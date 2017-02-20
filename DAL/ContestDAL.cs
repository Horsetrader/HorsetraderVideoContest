using System.Data;
using System.Configuration;

namespace DAL
{
    public class ContestDAL : BaseDAL
    {
        public static DataSet GetByContestID(int contestId)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorsetraderConnectionString"].ConnectionString);
            sql.AddParameter("@ContestID", contestId);

            DataSet dsResult = sql.Execute("sp_contest_GetContestByID", CommandType.StoredProcedure);
            return dsResult;
        }
    }
}
