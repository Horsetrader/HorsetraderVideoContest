using System;
using System.Data;
using DAL;

namespace BLL
{
    public class ContestEntryBLL
    {
        #region Properties
        public int ContestEntryID { get; set; }
        public int ContestID { get; set; }
        public int? CustomerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string VideoURL { get; set; }
        public string EmbedVideoURL { get; set; }
        public string Zipcode { get; set; }
        public int PeoplesChoiceScore { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Constructors
        public ContestEntryBLL()
        { ;}

        public ContestEntryBLL(int? contestEntryId)
        {
            if(contestEntryId != null && contestEntryId > 0)
                loadObject(ContestEntryDAL.GetByContestEntryID(contestEntryId));
        }

        public ContestEntryBLL(DataRow drContestEntry)
        {
            loadObject(drContestEntry);
        }
        #endregion

        #region Public Methods
        public void Save()
        {
            ContestEntryDAL.SaveContestEntry(this.ContestID, this.CustomerID, this.Title, this.Description,
                this.FirstName, this.LastName, this.Email, this.VideoURL, this.EmbedVideoURL, this.Zipcode);
        }

        public void Update()
        {
            ContestEntryDAL.UpdateContestEntry(this.ContestEntryID, this.ContestID, this.CustomerID, this.Title, this.Description,
                this.FirstName, this.LastName, this.Email, this.VideoURL, this.EmbedVideoURL, this.Zipcode, this.PeoplesChoiceScore);
        }

        public static DataTable List(int contestId, int contestEntryId)
        {
            DataSet ds = ContestEntryDAL.ListAllEntries(contestId, contestEntryId);
            DataTable dtEntries = new DataTable();

            if (ds.Tables.Count > 0)
                dtEntries = ds.Tables[0];

            return dtEntries;
        }

        public static DataTable ListLeadingEntries(int contestId)
        {
            DataSet ds = ContestEntryDAL.ListLeadingEntries(contestId);
            DataTable dtLeadingEntries = new DataTable();

            if (ds.Tables.Count > 0)
                dtLeadingEntries = ds.Tables[0];

            return dtLeadingEntries;
        }

        public static bool EmailExists(string email, int contestId)
        {
            DataSet dsEmail = ContestEntryDAL.EmailExists(email, contestId);
            if (dsEmail.Tables.Count > 0)
            {
                if (dsEmail.Tables[0].Rows.Count > 0)
                    return true;
            }

            return false;
        }
        #endregion

        #region Private Methods
        private void loadObject(DataSet dsContestEntry)
        {
            if (dsContestEntry.Tables.Count > 0)
            {
                if (dsContestEntry.Tables[0].Rows.Count > 0)
                {
                    DataRow drContestEntry = dsContestEntry.Tables[0].Rows[0];
                    ContestEntryID = string.IsNullOrEmpty(drContestEntry["ContestEntryID"].ToString()) ? -1 : int.Parse(drContestEntry["ContestEntryID"].ToString());
                    ContestID = string.IsNullOrEmpty(drContestEntry["ContestID"].ToString()) ? -1 : int.Parse(drContestEntry["ContestID"].ToString());
                    CustomerID = string.IsNullOrEmpty(drContestEntry["CustomerID"].ToString()) ? -1 : int.Parse(drContestEntry["CustomerID"].ToString());
                    Title = string.IsNullOrEmpty(drContestEntry["Title"].ToString()) ? string.Empty : drContestEntry["Title"].ToString();
                    Description = string.IsNullOrEmpty(drContestEntry["Description"].ToString()) ? string.Empty : drContestEntry["Description"].ToString();
                    FirstName = string.IsNullOrEmpty(drContestEntry["FirstName"].ToString()) ? string.Empty : drContestEntry["FirstName"].ToString();
                    LastName = string.IsNullOrEmpty(drContestEntry["LastName"].ToString()) ? string.Empty : drContestEntry["LastName"].ToString();
                    Email = string.IsNullOrEmpty(drContestEntry["Email"].ToString()) ? string.Empty : drContestEntry["Email"].ToString();
                    VideoURL = string.IsNullOrEmpty(drContestEntry["VideoURL"].ToString()) ? string.Empty : drContestEntry["VideoURL"].ToString();
                    EmbedVideoURL = string.IsNullOrEmpty(drContestEntry["EmbedVideoURL"].ToString()) ? string.Empty : drContestEntry["EmbedVideoURL"].ToString();
                    Zipcode = string.IsNullOrEmpty(drContestEntry["Zipcode"].ToString()) ? string.Empty : drContestEntry["Zipcode"].ToString();
                    PeoplesChoiceScore = string.IsNullOrEmpty(drContestEntry["PeoplesChoiceScore"].ToString()) ? -1 : int.Parse(drContestEntry["PeoplesChoiceScore"].ToString());
                    CreatedDate = string.IsNullOrEmpty(drContestEntry["CreatedDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContestEntry["CreatedDate"].ToString());
                }
            }
        }

        private void loadObject(DataRow drContestEntry)
        {
            ContestEntryID = string.IsNullOrEmpty(drContestEntry["ContestEntryID"].ToString()) ? -1 : int.Parse(drContestEntry["ContestEntryID"].ToString());
            ContestID = string.IsNullOrEmpty(drContestEntry["ContestID"].ToString()) ? -1 : int.Parse(drContestEntry["ContestID"].ToString());
            CustomerID = string.IsNullOrEmpty(drContestEntry["CustomerID"].ToString()) ? -1 : int.Parse(drContestEntry["CustomerID"].ToString());
            Title = string.IsNullOrEmpty(drContestEntry["Title"].ToString()) ? string.Empty : drContestEntry["Title"].ToString();
            Description = string.IsNullOrEmpty(drContestEntry["Description"].ToString()) ? string.Empty : drContestEntry["Description"].ToString();
            FirstName = string.IsNullOrEmpty(drContestEntry["FirstName"].ToString()) ? string.Empty : drContestEntry["FirstName"].ToString();
            LastName = string.IsNullOrEmpty(drContestEntry["LastName"].ToString()) ? string.Empty : drContestEntry["LastName"].ToString();
            Email = string.IsNullOrEmpty(drContestEntry["Email"].ToString()) ? string.Empty : drContestEntry["Email"].ToString();
            VideoURL = string.IsNullOrEmpty(drContestEntry["VideoURL"].ToString()) ? string.Empty : drContestEntry["VideoURL"].ToString();
            EmbedVideoURL = string.IsNullOrEmpty(drContestEntry["EmbedVideoURL"].ToString()) ? string.Empty : drContestEntry["EmbedVideoURL"].ToString();
            Zipcode = string.IsNullOrEmpty(drContestEntry["Zipcode"].ToString()) ? string.Empty : drContestEntry["Zipcode"].ToString();
            PeoplesChoiceScore = string.IsNullOrEmpty(drContestEntry["PeoplesChoiceScore"].ToString()) ? -1 : int.Parse(drContestEntry["PeoplesChoiceScore"].ToString());
            CreatedDate = string.IsNullOrEmpty(drContestEntry["CreatedDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContestEntry["CreatedDate"].ToString());
        }
        #endregion
    }
}
