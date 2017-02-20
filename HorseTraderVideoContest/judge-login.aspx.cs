using System;
using System.Configuration;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class judge_login : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            LoadUserFromCookie();

            if (Request["from"] != null)
                btnLogin.Text = "Sign In";
        }

        private void LoadUserFromCookie()
        {
            if (Request.Cookies["userInfoHTVC"] != null)
            {
                //Load user cookie info
                string email = Server.HtmlEncode(Request.Cookies["userInfoHTVC"]["username"]);
                string password = Server.HtmlEncode(Request.Cookies["userInfoHTVC"]["password"]);
                int contestId = int.Parse(ConfigurationManager.AppSettings["ContestID"]);
                ContestJudgeBLL judge = new ContestJudgeBLL(email, password, contestId);

                if (judge.ContestID > 0)
                {
                    Session["ContestJudge"] = judge;
                    Response.RedirectPermanent("list-entries.aspx");
                }
            }
        }
        #endregion

        #region Event Handlers
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Validate voting deadline
            if (ValidateDeadlineDate())
            {
                //Validate email and password inputs
                if (!validateInputs())
                    return;

                //Create ContestJudge object from username and password
                int contestId = int.Parse(ConfigurationManager.AppSettings["ContestID"]);
                ContestJudgeBLL judge = new ContestJudgeBLL(tbxEmail.Text, tbxPassword.Text, contestId);
                if (judge.ContestJudgeID > 0)
                {

                    Session["ContestJudge"] = judge;
                    //saveCookie();

                    if (Request["from"] != null)
                        Response.RedirectPermanent(Request["from"]);

                    Response.RedirectPermanent("change-vote.aspx");
                }
                else
                {
                    errorMessage.Attributes.Add("style", "display:block");
                    errorMessage.InnerHtml = "The username or password you entered is incorrect. Please try again.";
                }
            }
        }
        #endregion

        #region Methods
        private bool validateInputs()
        {
            if (string.IsNullOrEmpty(tbxEmail.Text) ||
                string.IsNullOrEmpty(tbxPassword.Text))
            {
                errorMessage.Attributes.Add("style", "display:block");
                errorMessage.InnerHtml = "Please enter your username and password.";
                return false;
            }

            return true;
        }

        private bool ValidateDeadlineDate()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            if (DateTime.Now < contest.VotingDeadline)
                return true;


            infoMessage.InnerHtml = "Thank you for signing up as a judge. Unfortunately, the voting deadline for this contest has passed. Look out for future contests you can participate in. Thanks again!";
            infoMessage.Attributes.Add("style", "display:block");

            return false;
        }

        //private void saveCookie()
        //{
        //    //Save "Remember me" cookie
        //    if (cbxRememberMe.Checked)
        //    {
        //        Response.Cookies["userInfoHTVC"]["username"] = tbxEmail.Text;
        //        Response.Cookies["userInfoHTVC"]["password"] = tbxPassword.Text;
        //        Response.Cookies["userInfoHTVC"].Expires = DateTime.Now.AddDays(15);
        //    }
        //}
        #endregion
    }
}