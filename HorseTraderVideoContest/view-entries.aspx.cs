using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class view_entries : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();

            if(!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            //Allow to vote only if deadline has not passed
            if (ValidateDeadlineDate(contest.VotingDeadline))
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                int contestEntryId = judge.VoteContestEntryID != null ? (int)judge.VoteContestEntryID : 0;
                repContestEntries.DataSource = ContestEntryBLL.List(int.Parse(ConfigurationManager.AppSettings["ContestID"]), contestEntryId);
                repContestEntries.DataBind();

                LoadNotificationSettingsModal(contest.VotingDeadline, judge.FirstName);
            }
            else
            {
                //Load message notifying of voting deadline
                primaryHeader.InnerHtml = "&nbsp;";
                secondaryHeader.Attributes.Add("style", "display:none;");
                infoMessage.InnerHtml = "Thank you for signing up as a judge. Unfortunately, the voting deadline for this contest has passed. Look out for future contests you can participate in. Thanks again!";
                infoMessage.Attributes.Add("style", "display:block");
                //Kill user session and delete cookie to prevent user from voting from another page
                KillSession();
            }
        }

        private void CheckSession()
        {
            //Check if session is valid
            if (Session["ContestJudge"] != null)
                return;

            if (LoadUserFromCookie())
                return;

            Response.RedirectPermanent("index.aspx");
        }

        private void KillSession()
        {
            Session["ContestJudge"] = null;

            if (Request.Cookies["userInfoHTVC"] != null)
            {
                HttpCookie userCookie = new HttpCookie("userInfoHTVC");
                userCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(userCookie);
            }
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

        private void LoadNotificationSettingsModal(DateTime votingDeadline, string firstName)
        {
            notificationSettingsText.InnerHtml = string.Format(
                "You can change your vote, {0}, right up to the deadline of 11:59 pm on {1}!" +
                "<br><br>" +
                "Would you like to receive a \"judge's alert\" whenever a new entry " +
                "is accepted into the contest, or would you prefer to receive a weekly " +
                "summary of new entries?" +
                "<br><br>" +
                "Please let us know!"
                , firstName, votingDeadline.ToString("dddd, MMM d"));
        }
        #endregion

        #region Event Handlers
        protected void ibtnVote_Click(object sender, ImageClickEventArgs e)
        {
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

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "confirmation", "$(function () {$('#notificationSettingsModal').modal({ show: true, keyboard: false, backdrop: 'static' });});", true);
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

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            if (Session["ContestJudge"] != null)
            {
                ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
                SetNotificationSettings(judge.Email, judge.ContestJudgeID);
            }
            
            //Show thank you message
            Response.RedirectPermanent("thank-you.aspx");
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

        private void SetNotificationSettings(string email, int contestJudgeId)
        {
            ContestJudgeBLL.EmailUnsubscribe(email, contestJudgeId, GetSubscriptionValue());
        }

        private int GetSubscriptionValue()
        {
            int subscriptionValue = 1;

            if (rbReceiveAlerts.Checked)
                subscriptionValue = 1;
            else if (rbWeeklyAlerts.Checked)
                subscriptionValue = 2;
            else if (rbNoAlerts.Checked)
                subscriptionValue = 3;

            return subscriptionValue;
        }

        private bool ValidateDeadlineDate(DateTime deadlineDate)
        {
            if (DateTime.Now < deadlineDate)
                return true;

            return false;
        }
        #endregion
    }
}