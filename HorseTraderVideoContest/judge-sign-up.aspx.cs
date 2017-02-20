using System;
using System.Configuration;
using BLL;

namespace HorsetraderVideoContest
{
    public partial class judge_sign_up : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPage();
        }

        private void LoadPage()
        {
            ContestBLL contest = new ContestBLL(int.Parse(ConfigurationManager.AppSettings["ContestID"]));

            //
            //if (ValidateDeadlineDate(contest.VotingDeadline))
            //{
            //    disclaimerText.InnerHtml = contest.JudgeGuidelines;
            //    LoadUserFromCookie();
            //}
            //else
            //{
            //    //Load message saying contest is over
            //}
            
            disclaimerText.InnerHtml = contest.JudgeGuidelines;
            LoadUserFromCookie();
        }

        private bool ValidateDeadlineDate(DateTime deadlineDate)
        {
            if(DateTime.Now < deadlineDate)
                return true;

            return false;
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

                if (judge.ContestJudgeID > 0)
                {
                    Session["ContestJudge"] = judge;
                    Response.RedirectPermanent("list-entries.aspx");
                }
            }
        }
        #endregion

        #region Event Handlers
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            int contestId = int.Parse(ConfigurationManager.AppSettings["ContestID"]);

            if (!EmailExistsInDatabase(contestId))
            {
                if (!ScreenNameExistsInDatabase(contestId))
                {
                    //Save judge to db
                    ContestJudgeBLL judge = new ContestJudgeBLL();
                    judge.ContestID = contestId;
                    judge.CustomerID = null;
                    judge.FirstName = tbxFirstName.Text;
                    judge.LastName = tbxLastName.Text;
                    judge.Email = tbxEmail.Text;
                    judge.Zipcode = tbxZipcode.Text;
                    judge.ScreenName = tbxScreenName.Text;
                    judge.Password = tbxPassword.Text;
                    judge = judge.Save();
                    //Save in session and cookie
                    Session["ContestJudge"] = judge;
                    saveCookie();
                    //Redirect to voting page
                    Response.RedirectPermanent("view-entries.aspx");
                }
                else
                {
                    errorMessage.Attributes.Add("style", "display:block");
                    errorMessage.InnerHtml = "This screen name is already registered. Please enter another one.";
                }
            }
            else
            {
                errorMessage.Attributes.Add("style", "display:block");
                errorMessage.InnerHtml = "This email is already registered. Please enter another one.";
            }
        }
        #endregion

        #region Methods
        private void saveCookie()
        {
            Response.Cookies["userInfoHTVC"]["username"] = tbxEmail.Text;
            Response.Cookies["userInfoHTVC"]["password"] = tbxPassword.Text;
            Response.Cookies["userInfoHTVC"].Expires = DateTime.Now.AddDays(15);
        }

        private bool EmailExistsInDatabase(int contestId)
        {
            return ContestJudgeBLL.EmailExists(tbxEmail.Text, contestId);
        }

        private bool ScreenNameExistsInDatabase(int contestId)
        {
            return ContestJudgeBLL.ScreenNameExists(tbxScreenName.Text, contestId);
        }
        #endregion
    }
}