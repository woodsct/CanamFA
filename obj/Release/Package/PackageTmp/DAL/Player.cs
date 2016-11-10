using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CanamLiveFA.DO;
using CanamLiveFA.Enums;

namespace CanamLiveFA.DAL
{
    public class Player : BaseDal
    {
        public static DO.Player GetPlayer(int playerId)
        {
            DO.Player playerObj = new DO.Player();
            Hashtable inparameters = new Hashtable();
            inparameters.Add("playerId", playerId);
            SqlDataReader sqlReader = SelectData("GetPlayer", inparameters, null);
            try
            {
                while (sqlReader.Read())
                {
                    playerId = GetIntValue(sqlReader["PId"]);
                    string playerName = GetStringValue(sqlReader["PPlayerName"]);
                    Enums.Team currentTeam = (Enums.Team)Enum.Parse(typeof(Enums.Team), GetStringValue(sqlReader["PCurrentTeam"]));
                    bool majors = GetBooleanValue(sqlReader["PMajors"]);
                    playerObj.PopulatePlayer(playerId, playerName, currentTeam, majors);
                    playerObj.HighestBid = GetDoubleValue(sqlReader["PHighestBid"]);
                    playerObj.HomeTeamBid = GetDoubleValue(sqlReader["PHomeTeamBid"]);
                    playerObj.HomeTeamRightToMatch = GetBooleanValue(sqlReader["PHomeTeamMatch"]);
                }
            }
            finally
            {
                sqlReader.Close();
            }

            return playerObj;
        }

        public static void InsertPlayerContract(int playerId, double ContractValue, bool isHomeTeam, bool rightToMatch, int years, bool noTrade, double amount, Enums.Team team)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("playerId", playerId);
            inparameters.Add("ContractValue", ContractValue);
            inparameters.Add("Years", years);
            inparameters.Add("NoTrade", noTrade);
            inparameters.Add("AmountPerYear", amount);
            inparameters.Add("biddingTeam", (int)team);
            inparameters.Add("HomeTeamRightToMatch", rightToMatch);
            if (isHomeTeam)
                inparameters.Add("HomeTeamBid", ContractValue);

            ExecuteProcedure("InsertPlayerContract", inparameters, null);
        }

        public static DataTable GetBidsByPlayer(int playerId)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("playerId", playerId);
            DataTable dtable = SelectDataInDataTable("GetBidsByPlayer", inparameters, null);
            dtable.Columns.Add("TeamName");
            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["Team"].ToString()).ToString();
            }
            return dtable;
        }

        public static DataTable GetAllUnsignedPlayers()
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("signed", 0);
            DataTable dtable = SelectDataInDataTable("GetAllPlayers", inparameters, null);
            dtable.Columns.Add("TeamName");
            dtable.Columns.Add("PlayerLevel");
            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["Team"].ToString()).ToString();
                if (bool.Parse(drow["Level"].ToString()))
                    drow["PlayerLevel"] = "Majors";
                else
                    drow["PlayerLevel"] = "Dev";
            }
            return dtable;
        }

        public static DataTable GetAllPlayersByTeam(DO.User userObj)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("teamId", (int)userObj.Team);
            DataTable dtable = SelectDataInDataTable("GetAllPlayersByTeam", inparameters, null);
            dtable.Columns.Add("TeamName");
            dtable.Columns.Add("PlayerLevel");

            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["Team"].ToString()).ToString();
                if (bool.Parse(drow["Level"].ToString()))
                    drow["PlayerLevel"] = "Majors";
                else
                    drow["PlayerLevel"] = "Dev";
            }
            return dtable;
        }

        public static DataTable GetAllSignedPlayers()
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("signed", 1);
            DataTable dtable = SelectDataInDataTable("GetAllPlayers", inparameters, null);
            dtable.Columns.Add("TeamName");
            dtable.Columns.Add("Compensation");
            dtable.Columns.Add("PlayerLevel");
            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["SigningTeam"].ToString()).ToString();
                if (bool.Parse(drow["Level"].ToString()))
                    drow["PlayerLevel"] = "Majors";
                else
                    drow["PlayerLevel"] = "Dev";
                double contractValue = double.Parse(drow["Bid"].ToString());
                if (drow["SigningTeam"] == drow["Team"])
                    drow["Compensation"] = "None";
                else if (contractValue > 10000)
                    drow["Compensation"] = "1st + " + contractValue * 0.1;
                else if (contractValue > 6000 && contractValue <= 10000)
                    drow["Compensation"] = "1st";
                else if (contractValue > 4000 && contractValue <= 6000)
                    drow["Compensation"] = "2nd";
                else if (contractValue > 3000 && contractValue <= 4000)
                    drow["Compensation"] = "3rd";
                else if (contractValue > 2000 && contractValue <= 30000)
                    drow["Compensation"] = "4th";
                else if (contractValue > 1000 && contractValue <= 20000)
                    drow["Compensation"] = "5th";
                else
                    drow["Compensation"] = "None";
            }
            return dtable;
        }

        public static void UploadFreeAgents(string filePath)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("filePath", filePath);
            ExecuteProcedure("ImportFreeAgents", inParameters, null);
        }

        public static void SignHighPlayers()
        {
            ExecuteProcedure("SignPlayers", null, null);
        }
    }
}