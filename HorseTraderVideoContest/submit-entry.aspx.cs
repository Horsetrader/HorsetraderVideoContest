using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Configuration;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class submit_entry : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            //If submission deadline date has passed, show message
            //and disable form
            if (!ValidateDeadlineDate(contest.DeadlineDate))
            {
                DisableForm();
                infoMessage.InnerHtml = string.Format("The deadline for submissions has passed. You can still vote until 11:59 pm on {0} by <a href='judge-sign-up.aspx'>signing up</a> or change your vote by <a href='judge-login.aspx'>signing in</a>",
                    contest.VotingDeadline.ToString("dddd, MMM d"));
                infoMessage.Attributes.Add("style", "display:block");
            }

            disclaimerText.InnerHtml = contest.DisclaimerText;
            firstPrize.InnerHtml = string.Format("Submit your video to win {0}!", contest.FirstPrize);
            secondaryBanner.InnerHtml = string.IsNullOrEmpty(contest.SecondaryBannerFileName) ? string.Empty : string.Format("<img src='assets/img/{0}' />", contest.SecondaryBannerFileName);
            confirmationText.InnerHtml = contest.ConfirmationText;
        }

        private bool ValidateDeadlineDate(DateTime deadlineDate)
        {
            if (DateTime.Now < deadlineDate)
                return true;

            return false;
        }

        private void DisableForm()
        {
            tbxEmail.Enabled = tbxFirstName.Enabled = tbxLastName.Enabled =
            tbxTitle.Enabled = tbxDescription.Enabled = tbxVideoUrl.Enabled =
            tbxZipcode.Enabled = btnSubmitVideo.Enabled = false;
        }
        #endregion

        #region Event Handlers
        protected void btnSubmitVideo_Click(object sender, EventArgs e)
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            if (ValidateDeadlineDate(contest.DeadlineDate))
            {
                if (!EmailExistsInDatabase(contest.ContestID))
                {
                    //Add new ContestEntry to database
                    ContestEntryBLL entry = new ContestEntryBLL();
                    entry.ContestID = int.Parse(ConfigurationManager.AppSettings["ContestID"]);
                    entry.CustomerID = null;
                    entry.Title = tbxTitle.Text;
                    entry.Description = tbxDescription.Text;
                    entry.FirstName = tbxFirstName.Text;
                    entry.LastName = tbxLastName.Text;
                    entry.Email = tbxEmail.Text;
                    entry.VideoURL = tbxVideoUrl.Text;
                    entry.EmbedVideoURL = FormatYoutubeLink(tbxVideoUrl.Text);
                    entry.Zipcode = tbxZipcode.Text;
                    entry.Save();

                    //Send confirmation emails
                    SendNewEntryNotification(contest.NotificationEmailSubject, FormatNewEntryNotificationBody(contest.NotificationEmailBody));
                    SendNewEntryConfirmation(contest.ConfirmationEmailSubject, FormatNewEntryConfirmationBody(contest.ConfirmationEmailBody));

                    //Clear all inputs
                    ClearForm();

                    //Load confirmation modal
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "confirmation", "$(function () {$('#confirmationModal').modal('show');});", true);
                }
                else
                {
                    errorMessage.Attributes.Add("style", "display:block");
                    errorMessage.InnerHtml = "This email is already registered. Please enter another one.";
                }
            }
        }
        #endregion

        #region Methods
        private string FormatYoutubeLink(string youtubeUrl)
        {
            Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

            Match youtubeMatch = YoutubeVideoRegex.Match(youtubeUrl);
            string formattedUrl = string.Empty;

            if (youtubeMatch.Success)
                formattedUrl = string.Format("http://www.youtube.com/embed/{0}", youtubeMatch.Groups[1].Value);

            return formattedUrl;
        }

        private void ClearForm()
        {
            tbxEmail.Text = tbxFirstName.Text = tbxLastName.Text = 
                tbxTitle.Text = tbxDescription.Text = tbxVideoUrl.Text =
                tbxZipcode.Text = string.Empty;

            errorMessage.Attributes.Add("style", "display:none");
        }

        private string FormatNewEntryNotificationBody(string body)
        {
            body = body.Replace("<created date>", DateTime.Now.ToLongDateString());
            body = body.Replace("<first name>", tbxFirstName.Text);
            body = body.Replace("<last name>", tbxLastName.Text);
            body = body.Replace("<email>", tbxEmail.Text);
            return body;
        }

        private void SendNewEntryNotification(string subject, string body)
        {
            string toAddress = ConfigurationManager.AppSettings["EmailAddressTo"];
            string fromAddress = ConfigurationManager.AppSettings["EmailAddressFrom"];

            EmailHandler.Send(fromAddress, toAddress, string.Empty, subject, body);
        }

        private string FormatNewEntryConfirmationBody(string body)
        {
            body = body.Replace("<first name>", tbxFirstName.Text);
            body = body.Replace("<video title>", tbxTitle.Text);
            return body;
        }

        private void SendNewEntryConfirmation(string subject, string body)
        {
            string toAddress = tbxEmail.Text;
            string fromAddress = ConfigurationManager.AppSettings["EmailAddressFrom"];

            EmailHandler.Send(fromAddress, toAddress, string.Empty, subject, body);
        }

        private bool EmailExistsInDatabase(int contestId)
        {
            return ContestEntryBLL.EmailExists(tbxEmail.Text, contestId);
        }
        #endregion
    }
}