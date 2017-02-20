using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class index : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            //Load contest basic information
            LoadContestInfo();

            //Load top 5 contest entries
            DataTable dtLeadingEntries = ContestEntryBLL.ListLeadingEntries(int.Parse(ConfigurationManager.AppSettings["ContestID"]));
            if (dtLeadingEntries.Rows.Count > 0)
            {
                //Load frontrunner entry
                LoadFrontRunner(new ContestEntryBLL(dtLeadingEntries.Rows[0]));
                dtLeadingEntries.Rows[0].Delete();

                //Load repeater with remaining top 4 contest entries
                repContestEntries.DataSource = dtLeadingEntries;
                repContestEntries.DataBind();
            }
            else
                frontrunner.InnerHtml = "<p style='text-align:center;'>No entries have been submitted yet. Click <a href='submit-entry.aspx'>here</a> to be the first!</p>";
        }

        private void LoadContestInfo()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));
            deadlineDate.Value = contest.VotingDeadline.ToString();
            secondaryBanner.InnerHtml = string.IsNullOrEmpty(contest.SecondaryBannerFileName) ? string.Empty : string.Format("<img src='assets/img/{0}' />", contest.SecondaryBannerFileName);
        }

        private void LoadFrontRunner(ContestEntryBLL frontrunner)
        {
            imgFrontRunnerEntry.ImageUrl = GetYoutubeThumbnailUrl(frontrunner.VideoURL);
            //frontrunnerEntry.InnerHtml = string.Format("<img src='{0}' />", GetYoutubeThumbnailUrl(frontrunner.VideoURL));
            frontrunnerEntry.Attributes.Add("video-url", frontrunner.EmbedVideoURL);
            frontrunnerEntry.Attributes.Add("video-title", frontrunner.Title);
            frontrunnerTitle.InnerHtml = frontrunner.Title;
            frontrunnerContestant.InnerHtml = string.Format("Submitted by {0} on {1}", frontrunner.FirstName, frontrunner.CreatedDate.ToShortDateString());
            frontrunnerDescription.InnerHtml = frontrunner.Description;
            frontrunnerVotes.InnerHtml = string.Format("{0} votes", frontrunner.PeoplesChoiceScore);
            LoadComments(frontrunner.ContestEntryID);
        }

        private void LoadComments(int contestEntryId)
        {
            DataTable dtComments = ContestCommentBLL.ListByContestEntryID(contestEntryId);
            int commentLimit = 2;

            foreach (DataRow dr in dtComments.Rows)
            {
                if (commentLimit > 0)
                {
                    frontrunnerComments.InnerHtml +=
                        "<b>" + dr["ScreenName"] + " says:</b>" +
                        "<br>" +
                        dr["Comment"] +
                        "<br><br>";
                }

                commentLimit--;
            }

            if (dtComments.Rows.Count > 2)
            {
                frontrunnerViewComments.Attributes.Add("href", "view-comments.aspx?id=" + contestEntryId);
                frontrunnerViewComments.InnerHtml = string.Format("View all comments ({0})", dtComments.Rows.Count);
            }
        }
        #endregion

        #region Event Handlers
        protected void repContestEntries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (repContestEntries.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblNoEntries");
                    lblFooter.Visible = true;
                    lblFooter.Text = "No other entries have been submitted yet. Click <a href='submit-entry.aspx'>here</a> to add your own!";
                }
            }
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