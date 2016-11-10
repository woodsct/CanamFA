using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace CanamLiveFA.DAL
{
    public class Bids: BaseDal
    {
        public static void RemoveBid(int playerId, int teamId)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("playerId", playerId);
            inParameters.Add("team", teamId);
            ExecuteProcedure("RemoveBid", inParameters, null);
        }

        public static DataTable GetFinalBids()
        {
            return SelectDataInDataTable("GetFinalBids", null, null);
        }
    }
}