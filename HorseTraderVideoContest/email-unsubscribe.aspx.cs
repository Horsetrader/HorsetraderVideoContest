using System;
using System.Configuration;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class email_unsubscribe : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event Handlers
        protected void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxEmail.Text))
                SetNotificationSettings();
        }
        #endregion

        #region Methods
        private void SetNotificationSettings()
        {
            if (ContestJudgeBLL.EmailUnsubscribe(tbxEmail.Text, GetContestJudgeID(), GetSubscriptionValue()))
            {
                unsubscribeBox.InnerHtml = "Thank you. Your subscription has been updated.";
                SendEmailNotification();
            }
            else
            {
                errorMessage.InnerHtml = "This email is not registered in our database. Please try again.";
                errorMessage.Attributes.Add("style", "display:block");
            }
        }

        private int GetContestJudgeID()
        {
            int contestJudgeId = 0;

            if (Request["id"] != null)
                int.TryParse(Request["id"], out contestJudgeId);

            return contestJudgeId;

        }

        private int GetSubscriptionValue()
        {
            int subscriptionValue = 1;

            if (rbReceiveAlerts.Checked)
                subscriptionValue = 1;
            else if (rbWeeklyAlerts.Checked)
                subscriptionValue = 2;
            else if (rbUnsubscribe.Checked)
                subscriptionValue = 0;

            return subscriptionValue;
        }

        private void SendEmailNotification()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            string emailFrom = ConfigurationManager.AppSettings["EmailAddressUnsubscribe"];
            string emailTo = ConfigurationManager.AppSettings["EmailAddressUnsubscribe"];
            string subject = "UNSUBSCRIBE:  Please remove from Contest " + contest.Name;
            string body = "User with email: " + tbxEmail.Text + "has unsubscribed from email notifications";
            EmailHandler.Send(emailFrom, emailTo, string.Empty, subject, body);
        }
        #endregion
    }
}