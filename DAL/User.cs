using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanamLiveFA.DAL
{
    public class User : BaseDal
    {
        public static DO.User GetUserByUserName(string userName)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("userName", userName);
            SqlDataReader sqlReader = SelectData("GetUserByUserName", inParameters, null);
            DO.User userObj = new DO.User();
            try
            {
                while (sqlReader.Read())
                {
                    userObj = PopulateUser(sqlReader);
                }
            }
            finally
            {
                sqlReader.Close();
            }
            return userObj;
        }

        public static DO.User GetUserByTeam(Enums.Team team)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("team", (int)team);
            SqlDataReader sqlReader = SelectData("GetUserByTeam", inParameters, null);
            DO.User userObj = new DO.User();
            try
            {
                while (sqlReader.Read())
                {
                    userObj = PopulateUser(sqlReader);
                }
            }
            finally
            {
                sqlReader.Close();
            }
            return userObj;
        }

        public static void UpdateUserBidFees(int userId, double fees, bool isPayable)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("userId", userId);
            inParameters.Add("fees", fees);
            inParameters.Add("isPayable", isPayable);
            ExecuteProcedure("UpdateBidFees", inParameters, null);
        }

        private static DO.User PopulateUser(SqlDataReader sqlReader)
        {
            DO.User userObj = new DO.User();
            int userId = GetIntValue(sqlReader["UId"]);
            string userName = GetStringValue(sqlReader["UUserName"]);
            string password = GetStringValue(sqlReader["UPassword"]);
            Enums.Team team = (Enums.Team)Enum.Parse(typeof(Enums.Team), GetStringValue(sqlReader["UTeam"]));
            bool commissioner = GetBooleanValue(sqlReader["UCommissioner"]);
            int qualifyingFreeAgentAvailable = GetIntValue(sqlReader["UQualifyingFAsAvailable"]);
            userObj.PopulateUser(userId, userName, team, password, commissioner, qualifyingFreeAgentAvailable);
            return userObj;
        }
    }
}