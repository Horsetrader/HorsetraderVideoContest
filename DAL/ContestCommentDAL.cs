using System.Data;
using System.Configuration;

namespace DAL
{
    public class ContestCommentDAL :BaseDAL
    {
        public static void SaveContestComment(string comment, int contestEntryId, int contestJudgeId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@Comment", comment);
            sql.AddParameter("@ContestEntryID", contestEntryId);
            sql.AddParameter("@ContestJudgeID", contestJudgeId);

            sql.ExecuteNonQuery("sp_ContestComment_Insert", CommandType.StoredProcedure);
        }

        public static DataSet ListCommentsByContestEntryID(int contestEntryId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestEntryID", contestEntryId);

            return sql.Execute("sp_ContestComment_ListByContestEntryID", CommandType.StoredProcedure);
        }
    }
}
