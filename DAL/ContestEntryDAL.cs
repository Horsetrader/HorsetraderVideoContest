using System.Data;
using System.Configuration;

namespace DAL
{
    public class ContestEntryDAL : BaseDAL
    {
        public static DataSet GetByContestEntryID(int? contestEntryId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestEntryID", contestEntryId);

            DataSet dsResult = sql.Execute("sp_ContestEntry_GetContestEntryByID", CommandType.StoredProcedure);
            return dsResult;
        }

        public static void SaveContestEntry(int contestId, int? customerId, string title, string description, string firstName,
            string lastName, string email, string videoUrl, string embedVideoUrl, string zipcode)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestID", contestId);
            sql.AddParameter("@CustomerID", customerId);
            sql.AddParameter("@Title", title);
            sql.AddParameter("@Description", description);
            sql.AddParameter("@FirstName", firstName);
            sql.AddParameter("@LastName", lastName);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@VideoUrl", videoUrl);
            sql.AddParameter("@EmbedVideoUrl", embedVideoUrl);
            sql.AddParameter("@Zipcode", zipcode);

            sql.ExecuteNonQuery("sp_ContestEntry_Insert", CommandType.StoredProcedure);
        }

        public static void UpdateContestEntry(int contestEntryId, int contestId, int? customerId, string title, string description, string firstName, 
            string lastName, string email, string videoUrl, string embedVideoUrl, string zipcode, int peoplesChoiceScore)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestEntryID", contestEntryId);
            sql.AddParameter("@ContestID", contestId);
            sql.AddParameter("@CustomerID", customerId);
            sql.AddParameter("@Title", title);
            sql.AddParameter("@Description", description);
            sql.AddParameter("@FirstName", firstName);
            sql.AddParameter("@LastName", lastName);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@VideoUrl", videoUrl);
            sql.AddParameter("@EmbedVideoUrl", embedVideoUrl);
            sql.AddParameter("@Zipcode", zipcode);
            sql.AddParameter("@PeoplesChoiceScore", peoplesChoiceScore);

            sql.ExecuteNonQuery("sp_ContestEntry_Update", CommandType.StoredProcedure);
        }

        public static DataSet ListAllEntries(int contestId, int contestEntryId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestID", contestId);
            sql.AddParameter("@ContestEntryID", contestEntryId);
            return sql.Execute("sp_ContestEntry_List", CommandType.StoredProcedure);
        }

        public static DataSet ListLeadingEntries(int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestID", contestId);
            return sql.Execute("sp_ContestEntry_ListLeadingEntries", CommandType.StoredProcedure);
        }

        public static DataSet EmailExists(string email, int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@Email", email);
            sql.AddParameter("@ContestID", contestId);

            return sql.Execute("sp_ContestEntry_EmailExists", CommandType.StoredProcedure);
        }
    }
}
