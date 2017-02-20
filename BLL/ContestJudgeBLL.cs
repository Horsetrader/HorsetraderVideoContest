using System;
using System.Data;
using DAL;

namespace BLL
{
    public class ContestJudgeBLL
    {
        #region Properties
        public int ContestJudgeID { get; set; }
        public int ContestID { get; set; }
        public int? CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Zipcode { get; set; }
        public int? VoteContestEntryID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ScreenName { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public ContestJudgeBLL()
        { ;}

        public ContestJudgeBLL(DataSet dsContestJudge)
        {
            loadObject(dsContestJudge);
        }

        public ContestJudgeBLL(string email, string password, int contestId)
        {
            DataSet dsContestJudge = ContestJudgeDAL.Login(email, contestId);

            if (validateLogin(dsContestJudge, password))
                loadObject(dsContestJudge);
        }
        #endregion

        #region Public Methods
        public ContestJudgeBLL Save()
        {
            DataSet dsContestJudge = ContestJudgeDAL.SaveContestJudge(this.ContestID, this.CustomerID, this.FirstName, 
                this.LastName, this.Email, this.Zipcode, this.ScreenName, this.Password);

            return new ContestJudgeBLL(dsContestJudge);
        }

        public void Update()
        {
            ContestJudgeDAL.UpdateContestJudge(this.ContestJudgeID, this.ContestID, this.FirstName, this.LastName,
                this.Email, this.Zipcode, this.VoteContestEntryID, this.ScreenName, this.Password);
        }

        public static bool EmailExists(string email, int contestId)
        {
            DataSet dsEmail = ContestJudgeDAL.EmailExists(email, contestId);
            if (dsEmail.Tables.Count > 0)
            {
                if (dsEmail.Tables[0].Rows.Count > 0)
                    return true;
            }

            return false;
        }

        public static bool ScreenNameExists(string screenname, int contestId)
        {
            DataSet dsScreenName = ContestJudgeDAL.ScreenNameExists(screenname, contestId);
            if (dsScreenName.Tables.Count > 0)
            {
                if (dsScreenName.Tables[0].Rows.Count > 0)
                    return true;
            }

            return false;
        }

        public static bool EmailUnsubscribe(string email, int contestJudgeId, int optIn)
        {
            DataSet dsContestJudge = ContestJudgeDAL.EmailUnsubscribe(email, contestJudgeId, optIn);
            if (dsContestJudge.Tables.Count > 0)
            {
                if (dsContestJudge.Tables[0].Rows.Count > 0)
                    return true;
            }

            return false;
        }
        #endregion

        #region Private Methods
        private void loadObject(DataSet dsContestJudge)
        {
            if (dsContestJudge.Tables.Count > 0)
            {
                if (dsContestJudge.Tables[0].Rows.Count > 0)
                {
                    DataRow drContestJudge = dsContestJudge.Tables[0].Rows[0];
                    ContestJudgeID = string.IsNullOrEmpty(drContestJudge["ContestJudgeID"].ToString()) ? -1 : int.Parse(drContestJudge["ContestJudgeID"].ToString());
                    ContestID = string.IsNullOrEmpty(drContestJudge["ContestID"].ToString()) ? -1 : int.Parse(drContestJudge["ContestID"].ToString());
                    CustomerID = string.IsNullOrEmpty(drContestJudge["CustomerID"].ToString()) ? -1 : int.Parse(drContestJudge["CustomerID"].ToString());
                    FirstName = string.IsNullOrEmpty(drContestJudge["FirstName"].ToString()) ? string.Empty : drContestJudge["FirstName"].ToString();
                    LastName = string.IsNullOrEmpty(drContestJudge["LastName"].ToString()) ? string.Empty : drContestJudge["LastName"].ToString();
                    Email = string.IsNullOrEmpty(drContestJudge["Email"].ToString()) ? string.Empty : drContestJudge["Email"].ToString();
                    Zipcode = string.IsNullOrEmpty(drContestJudge["Zipcode"].ToString()) ? string.Empty : drContestJudge["Zipcode"].ToString();
                    VoteContestEntryID = string.IsNullOrEmpty(drContestJudge["VoteContestEntryID"].ToString()) ? -1 : int.Parse(drContestJudge["VoteContestEntryID"].ToString());
                    CreatedDate = string.IsNullOrEmpty(drContestJudge["CreatedDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContestJudge["CreatedDate"].ToString());
                    ScreenName = string.IsNullOrEmpty(drContestJudge["ScreenName"].ToString()) ? string.Empty : drContestJudge["ScreenName"].ToString();
                    Password = string.IsNullOrEmpty(drContestJudge["Password"].ToString()) ? string.Empty : drContestJudge["Password"].ToString();
                }
            }
        }

        private bool validateLogin(DataSet dsContestJudge, string inputPassword)
        {
            if (dsContestJudge.Tables.Count > 0)
            {
                if (dsContestJudge.Tables[0].Rows.Count > 0)
                {
                    string password = dsContestJudge.Tables[0].Rows[0]["password"].ToString();
                    return password.Equals(inputPassword);
                }
            }

            return false;
        }
        #endregion
    }
}
