using System;
using System.Data;
using DAL;

namespace BLL
{
    public class ContestBLL
    {
        #region Properties
        public int ContestID { get; set; }
        public int? MainContactCustomerID { get; set; }
        public string Association { get; set; }
        public int? PubNumber { get; set; }
        public string Name { get; set; }
        public string BannerFileName { get; set; }
        public string SecondaryBannerFileName { get; set; }
        public string DisclaimerText { get; set; }
        public string JudgeGuidelines { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime VotingDeadline { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public string FirstPrize { get; set; }
        public int? WinnerContestEntryID { get; set; }
        public string ConfirmationText { get; set; }
        public string NotificationEmailSubject { get; set; }
        public string NotificationEmailBody { get; set; }
        public string ConfirmationEmailSubject { get; set; }
        public string ConfirmationEmailBody { get; set; }
        public ContestType Type { get; set; }
        #endregion

        #region Constructors
        public ContestBLL()
        { ;}

        public ContestBLL(int contestId)
        {
            loadObject(ContestDAL.GetByContestID(contestId));
        }
        #endregion

        #region Enum
        public enum ContestType
        {
            Undefined = 0,
            Video = 1,
            Raffle = 2,
            Photo = 3
        }
        #endregion

        #region Private Methods
        private void loadObject(DataSet dsContest)
        {
            if (dsContest.Tables.Count > 0)
            {
                if (dsContest.Tables[0].Rows.Count > 0)
                {
                    DataRow drContest = dsContest.Tables[0].Rows[0];
                    ContestID = string.IsNullOrEmpty(drContest["ContestID"].ToString()) ? -1 : int.Parse(drContest["ContestID"].ToString());
                    MainContactCustomerID = string.IsNullOrEmpty(drContest["MainContactCustomerID"].ToString()) ? -1 : int.Parse(drContest["MainContactCustomerID"].ToString());
                    Association = string.IsNullOrEmpty(drContest["Association"].ToString()) ? string.Empty : drContest["Association"].ToString();
                    PubNumber = string.IsNullOrEmpty(drContest["PubNumber"].ToString()) ? -1 : int.Parse(drContest["PubNumber"].ToString());
                    Name = string.IsNullOrEmpty(drContest["Name"].ToString()) ? string.Empty : drContest["Name"].ToString();
                    BannerFileName = string.IsNullOrEmpty(drContest["BannerFileName"].ToString()) ? string.Empty : drContest["BannerFileName"].ToString();
                    SecondaryBannerFileName = string.IsNullOrEmpty(drContest["SecondaryBannerFileName"].ToString()) ? string.Empty : drContest["SecondaryBannerFileName"].ToString();
                    DisclaimerText = string.IsNullOrEmpty(drContest["DisclaimerText"].ToString()) ? string.Empty : drContest["DisclaimerText"].ToString();
                    StartDate = string.IsNullOrEmpty(drContest["StartDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContest["StartDate"].ToString());
                    DeadlineDate = string.IsNullOrEmpty(drContest["DeadlineDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContest["DeadlineDate"].ToString());
                    VotingDeadline = string.IsNullOrEmpty(drContest["VotingDeadline"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContest["VotingDeadline"].ToString());
                    AnnouncementDate = string.IsNullOrEmpty(drContest["AnnouncementDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(drContest["AnnouncementDate"].ToString());
                    FirstPrize = string.IsNullOrEmpty(drContest["FirstPrize"].ToString()) ? string.Empty : drContest["FirstPrize"].ToString();
                    WinnerContestEntryID = string.IsNullOrEmpty(drContest["WinnerContestEntryID"].ToString()) ? -1 : int.Parse(drContest["WinnerContestEntryID"].ToString());
                    ConfirmationText = string.IsNullOrEmpty(drContest["ConfirmationText"].ToString()) ? string.Empty : drContest["ConfirmationText"].ToString();ConfirmationText = string.IsNullOrEmpty(drContest["ConfirmationText"].ToString()) ? string.Empty : drContest["ConfirmationText"].ToString();
                    NotificationEmailSubject = string.IsNullOrEmpty(drContest["NotificationEmailSubject"].ToString()) ? string.Empty : drContest["NotificationEmailSubject"].ToString();
                    NotificationEmailBody = string.IsNullOrEmpty(drContest["NotificationEmailBody"].ToString()) ? string.Empty : drContest["NotificationEmailBody"].ToString();
                    ConfirmationEmailSubject = string.IsNullOrEmpty(drContest["ConfirmationEmailSubject"].ToString()) ? string.Empty : drContest["ConfirmationEmailSubject"].ToString();
                    ConfirmationEmailBody = string.IsNullOrEmpty(drContest["ConfirmationEmailBody"].ToString()) ? string.Empty : drContest["ConfirmationEmailBody"].ToString();
                    JudgeGuidelines = string.IsNullOrEmpty(drContest["JudgeGuidelines"].ToString()) ? string.Empty : drContest["JudgeGuidelines"].ToString();
                    Type = string.IsNullOrEmpty(drContest["ContestTypeID"].ToString()) ? ContestType.Undefined : (ContestType)int.Parse(drContest["ContestTypeID"].ToString());
                }
            }
        }
        #endregion
    }
}
