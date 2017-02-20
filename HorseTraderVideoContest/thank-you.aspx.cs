using System;
using System.Configuration;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class thank_you : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
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
        protected void btnSaveComment_Click(object sender, EventArgs e)
        {
            ContestJudgeBLL judge = (ContestJudgeBLL)Session["ContestJudge"];
            //Save commment
            SaveComment(judge);
            //Display coupon
            ShowCoupon();
            //Email coupon
            //EmailCoupon(judge.Email);
        }
        #endregion

        #region Methods
        private void SaveComment(ContestJudgeBLL judge)
        {
            ContestCommentBLL comment = new ContestCommentBLL();
            comment.Comment = tbxJudgeComment.Text;
            comment.ContestEntryID = (int)judge.VoteContestEntryID;
            comment.ContestJudgeID = judge.ContestJudgeID;
            comment.Save();
        }

        private void ShowCoupon()
        {
            submitComment.Visible = false;
            printCoupon.InnerHtml = 
                "<div class='coupon'>" +
                    "<img src='assets/img/funny-horse-videos-voucher.jpg' height='210' width='575' />" +
                "</div>" +
                "<div class='print-coupon'>" +
                    "<button id='btnPrintCoupon' type='button' class='btn btn-success btn-large'>Print Coupon!</button>" +
                "</div>";
        }

        private void EmailCoupon(string toAddress)
        {
            string fromAddress = ConfigurationManager.AppSettings["EmailAddressFrom"];
            string subject = "Here's your prize for being a People's Choice judge";
            string body = "<p>Thank you! you will find your prize attached as a PDF file.<br><br>Your Horsetrader.com Team</p>";
            string attachment = "funny-horse-videos-voucher.pdf";

            EmailHandler.Send(fromAddress, toAddress, string.Empty, subject, body, attachment);
        }
        #endregion
    }
}