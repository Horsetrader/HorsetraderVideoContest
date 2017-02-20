using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class list_entries : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            //If judge is logged in, set the judge's selected entry
            //as the contestEntryId variable.
            int contestEntryId = 0;

            if (LoadUserFromCookie())
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                contestEntryId = (int)judge.VoteContestEntryID;
                //If user is logged in, hide button that redirect to register and login page
                voteButtons.Attributes.Add("style", "display:none");
            }

            repContestEntries.DataSource = ContestEntryBLL.List(int.Parse(ConfigurationManager.AppSettings["ContestID"]), contestEntryId);
            repContestEntries.DataBind();
        }

        private void CheckSession()
        {
            //Check if session is valid
            if (Session["ContestJudge"] != null)
                return;

            if (LoadUserFromCookie())
                return;

            Response.RedirectPermanent("judge-sign-up.aspx");
        }

        private bool LoadUserFromCookie()
        {
            if (Session["ContestJudge"] != null)
                return true;

            if (Request.Cookies["userInfoHTVC"] != null)
            {
                //Load user cookie info
                string email = Server.HtmlEncode(Request.Cookies["userInfoHTVC"]["username"]);
                string password = Server.HtmlEncode(Request.Cookies["userInfoHTVC"]["password"]);
                int contestId = int.Parse(ConfigurationManager.AppSettings["ContestID"]);
                ContestJudgeBLL judge = new ContestJudgeBLL(email, password, contestId);

                if (judge.ContestJudgeID > 0)
                {
                    Session["ContestJudge"] = judge;
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Event Handlers
        protected void ibtnVote_Click(object sender, ImageClickEventArgs e)
        {
            CheckSession();

            //Get ContestEntryID that is being voted for
            ImageButton ibtnVote = (ImageButton)sender;
            int contestEntryId = int.Parse(ibtnVote.CommandArgument);

            //Load judge object and check if he/she has already voted for this entry
            ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
            if (!judge.VoteContestEntryID.Equals(contestEntryId))
            {
                //If judge has voted previously, then subtract his/her vote from previous entry
                if (judge.VoteContestEntryID != null && judge.VoteContestEntryID > 0)
                    SubtractVote(judge.VoteContestEntryID);

                //Add judge's new vote to entry
                AddVote(contestEntryId);

                //Update judge's vote
                judge.VoteContestEntryID = contestEntryId;
                judge.Update();
                Session["ContestJudge"] = judge;
            }

            //Reload repeater
            LoadPage();
        }

        protected void repContestEntries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (repContestEntries.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblNoEntries");
                    lblFooter.Visible = true;
                    lblFooter.Text = "No entries have been submitted yet, but check back soon!";
                }
            }
        }

        protected string SetSelectedClass(int contestEntryId)
        {
            if (Session["ContestJudge"] != null)
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                if (judge.VoteContestEntryID.Equals(contestEntryId))
                    return "entry generic-container selected-entry";
            }

            return "entry generic-container";
        }

        protected string SetVoteButton(int contestEntryId)
        {
            if (Session["ContestJudge"] != null)
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                if (judge.VoteContestEntryID.Equals(contestEntryId))
                    return "~/assets/img/checked.png";
            }

            return "~/assets/img/unchecked.png";
        }

        protected string GetYoutubeThumbnailUrl(string youtubeUrl)
        {
            string youtubeId = GetYoutubeID(youtubeUrl);
            if (!string.IsNullOrEmpty(youtubeId))
            {
                return string.Format("http://img.youtube.com/vi/{0}/0.jpg", youtubeId);
            }

            return "";
        }

        protected string GetViewCommentsLink(string contestEntryId, string numberOfComments)
        {
            return string.Format("<a href='view-comments.aspx?id={0}'>View comments ({1})</a>", contestEntryId, numberOfComments);
        }
        #endregion

        #region Methods
        private void SubtractVote(int? contestEntryId)
        {
            ContestEntryBLL entry = new ContestEntryBLL(contestEntryId);
            entry.PeoplesChoiceScore--;
            entry.Update();
        }

        private void AddVote(int contestEntryId)
        {
            ContestEntryBLL entry = new ContestEntryBLL(contestEntryId);
            entry.PeoplesChoiceScore++;
            entry.Update();
        }

        private string GetYoutubeID(string youtubeUrl)
        {
            Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

            Match youtubeMatch = YoutubeVideoRegex.Match(youtubeUrl);
            string youtubeId = string.Empty;

            if (youtubeMatch.Success)
                youtubeId = youtubeMatch.Groups[1].Value;

            return youtubeId;
        }
        #endregion
    }
}