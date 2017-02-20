using System.Data;
using System.Configuration;

namespace DAL
{
    public class ContestJudgeDAL : BaseDAL
    {
        public static DataSet SaveContestJudge(int contestId, int? customerId, string firstName, string lastName,
            string email, string zipcode, string screenname, string password)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestID", contestId);
            sql.AddParameter("@CustomerID", customerId);
            sql.AddParameter("@FirstName", firstName);
            sql.AddParameter("@LastName", lastName);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@Zipcode", zipcode);
            sql.AddParameter("@ScreenName", screenname);
            sql.AddParameter("@Password", password);

            return sql.Execute("sp_contest_InsertContestJudge", CommandType.StoredProcedure);
        }

        public static void UpdateContestJudge(int contestJudgeId, int contestId, string firstName, string lastName,
            string email, string zipcode, int? voteContestEntryId, string screenname, string password)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestJudgeID", contestJudgeId);
            sql.AddParameter("@ContestID", contestId);
            sql.AddParameter("@FirstName", firstName);
            sql.AddParameter("@LastName", lastName);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@Zipcode", zipcode);
            sql.AddParameter("@VoteContestEntryID", voteContestEntryId);
            sql.AddParameter("@ScreenName", screenname);
            sql.AddParameter("@Password", password);

            sql.ExecuteNonQuery("sp_contest_UpdateContestJudge", CommandType.StoredProcedure);
        }

        public static DataSet EmailExists(string email, int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@ContestID", contestId);

            return sql.Execute("sp_ContestJudge_EmailExists", CommandType.StoredProcedure);
        }

        public static DataSet ScreenNameExists(string screenname, int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ScreenName", screenname);
            sql.AddParameter("@ContestID", contestId);

            return sql.Execute("sp_ContestJudge_ScreenNameExists", CommandType.StoredProcedure);
        }

        public static DataSet Login(string email, int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@ContestID", contestId);

            return sql.Execute("sp_ContestJudge_Login", CommandType.StoredProcedure);
        }

        public static DataSet EmailUnsubscribe(string email, int contestJudgeId, int optIn)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@ContestJudgeID", contestJudgeId);
            sql.AddParameter("@OptIn", optIn);

            return sql.Execute("sp_ContestJudge_EmailUnsubscribe", CommandType.StoredProcedure);
        }
    }
}
