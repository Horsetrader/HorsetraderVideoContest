using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class view_comments : System.Web.UI.Page
    {
        protected string id;
        protected string pageUrl;
        protected string pageThumbnail;
        protected string pageTitle;
        protected string pageDescription;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();

            if (!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            if (Request["id"] != null)
            {
                int contestEntryId = 0;
                int.TryParse(Request["id"], out contestEntryId);

                ContestEntryBLL entry = new ContestEntryBLL(contestEntryId);
                LoadEntry(entry);
                SetVoteButton(contestEntryId);

                if (entry.ContestEntryID > 0)
                    return;
            }
            
            Response.RedirectPermanent("index.aspx");
        }

        private void LoadEntry(ContestEntryBLL entry)
        {
            videoFrame.Attributes.Add("src", entry.EmbedVideoURL + "?rel=0");
            frontrunnerTitle.InnerHtml = entry.Title;
            frontrunnerContestant.InnerHtml = string.Format("Submitted by {0} on {1}", entry.FirstName, entry.CreatedDate.ToShortDateString());
            frontrunnerDescription.InnerHtml = entry.Description;
            frontrunnerVotes.InnerHtml = string.Format("{0} votes", entry.PeoplesChoiceScore);
            LoadComments(entry.ContestEntryID);
            LoadFacebookMetadata(entry);
        }

        private void LoadComments(int contestEntryId)
        {
            repComments.DataSource = ContestCommentBLL.ListByContestEntryID(contestEntryId);
            repComments.DataBind();
        }

        private void LoadFacebookMetadata(ContestEntryBLL entry)
        {
            id = entry.ContestEntryID.ToString();
            pageUrl = Request.Url.ToString();
            pageTitle = entry.Title.Replace("\"", "'");
            pageDescription = entry.Description.Replace("\"","'");
            pageThumbnail = GetYoutubeThumbnailUrl(entry.VideoURL);
        }

        private bool CheckSession()
        {
            //Check if session is valid
            if (Session["ContestJudge"] != null || LoadUserFromCookie())
            {
                signInPostComment.Visible = false;
                postComment.Visible = true;
                return true;
            }
            else
                postComment.Visible = false;

            return false;
        }

        private bool LoadUserFromCookie()
        {
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
        protected void repComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (repComments.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblNoComments");
                    lblFooter.Visible = true;
                    lblFooter.Text = "There are currently no comments for this video.";
                }
            }
        }

        protected void btnSaveComment_Click(object sender, EventArgs e)
        {
            if (Session["ContestJudge"] != null && Request["id"] != null && tbxJudgeComment.Text != string.Empty)
            {
                int contestEntryId = 0;
                int.TryParse(Request["id"], out contestEntryId);

                if (contestEntryId > 0)
                {
                    ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                    ContestCommentBLL comment = new ContestCommentBLL();
                    comment.Comment = tbxJudgeComment.Text;
                    comment.ContestEntryID = contestEntryId;
                    comment.ContestJudgeID = judge.ContestJudgeID;
                    comment.Save();

                    tbxJudgeComment.Text = string.Empty;

                    LoadPage();
                }
            }
        }

        protected void ibtnVote_Click(object sender, ImageClickEventArgs e)
        {
            //Get ContestEntryID that is being voted for
            ImageButton ibtnVote = (ImageButton)sender;
            int contestEntryId = int.Parse(ibtnVote.CommandArgument);

            if (CheckSession())
            {
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

                //Reload page
                LoadPage();
            }
            else
                Response.RedirectPermanent("judge-login.aspx?from=view-comments.aspx?id=" + contestEntryId);
        }
        #endregion

        #region Methods
        protected string GetYoutubeThumbnailUrl(string youtubeUrl)
        {
            string youtubeId = GetYoutubeID(youtubeUrl);
            if (!string.IsNullOrEmpty(youtubeId))
            {
                return string.Format("http://img.youtube.com/vi/{0}/0.jpg", youtubeId);
            }

            return "";
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

        protected void SetVoteButton(int contestEntryId)
        {
            ibtnVote.ImageUrl = "~/assets/img/unchecked.png";
            ibtnVote.CommandArgument = contestEntryId.ToString();

            if (Session["ContestJudge"] != null)
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                if (judge.VoteContestEntryID.Equals(contestEntryId))
                    ibtnVote.ImageUrl = "~/assets/img/checked.png";
            }
        }

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
        #endregion

        
    }
}