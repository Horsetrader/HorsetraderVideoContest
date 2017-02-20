using System;
using System.Data;
using DAL;

namespace BLL
{
    public class ContestCommentBLL
    {
        #region Properties
        public string Comment { get; set; }
        public int ContestEntryID { get; set; }
        public int ContestJudgeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        #endregion

        #region Constructors
        public ContestCommentBLL()
        { ; }
        #endregion

        #region Public Methods
        public void Save()
        {
            ContestCommentDAL.SaveContestComment(this.Comment, this.ContestEntryID, this.ContestJudgeID);
        }
        #endregion

        #region Public Static Methods
        public static DataTable ListByContestEntryID(int contestEntryId)
        {
            DataSet dsComments = ContestCommentDAL.ListCommentsByContestEntryID(contestEntryId);
            DataTable dtComments = new DataTable();

            if (dsComments.Tables.Count > 0)
                    dtComments = dsComments.Tables[0];

            return dtComments;
        }
        #endregion
    }
}
