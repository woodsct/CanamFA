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
                    bool signed = GetBooleanValue(sqlReader["PSigned"]);
                    DateTime bidTime = GetDateValue(sqlReader["PBidTime"]);
                    bool qualified = GetBooleanValue(sqlReader["PQualifyingOffer"]);
                    playerObj.PopulatePlayer(playerId, playerName, currentTeam, majors, signed, bidTime, qualified);
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

        public static void InsertPlayerContract(int playerId, double ContractValue, int years, bool noTrade, double amount, Enums.Team team)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("playerId", playerId);
            inparameters.Add("ContractValue", ContractValue);
            inparameters.Add("Years", years);
            inparameters.Add("NoTrade", noTrade);
            inparameters.Add("AmountPerYear", amount);
            inparameters.Add("biddingTeam", (int)team);
            inparameters.Add("bidTime", DateTime.Now);
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

        public static DataTable GetUserBidsByPlayer(int playerId, int teamId)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("playerId", playerId);
            inparameters.Add("team", teamId);
            DataTable dtable = SelectDataInDataTable("GetUserBidsByPlayer", inparameters, null);
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
            dtable.Columns.Add("BiddingTeam");
            dtable.Columns.Add("PlayerLevel");
            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["Team"].ToString()).ToString();
                if (!string.IsNullOrWhiteSpace(drow["HighTeam"].ToString()))
                    drow["BiddingTeam"] = Enum.Parse(typeof(Enums.Team), drow["HighTeam"].ToString()).ToString();
                else
                    drow["BiddingTeam"] = string.Empty;
                if (bool.Parse(drow["Level"].ToString()))
                    drow["PlayerLevel"] = "Majors";
                else
                    drow["PlayerLevel"] = "Dev";
            }
            return dtable;
        }

        public static DataTable GetAllPlayers(bool signed)
        {
            Hashtable inparameters = new Hashtable();
            inparameters.Add("signed", signed);
            DataTable dtable = SelectDataInDataTable("GetAllPlayers", inparameters, null);
            dtable.Columns.Add("TeamName");
            dtable.Columns.Add("PlayerLevel");
            dtable.Columns.Add("BiddingTeam");
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
            dtable.Columns.Add("BiddingTeam");
            foreach (DataRow drow in dtable.Rows)
            {
                drow["TeamName"] = Enum.Parse(typeof(Enums.Team), drow["Team"].ToString()).ToString();
                if (!string.IsNullOrWhiteSpace(drow["HighTeam"].ToString()))
                    drow["BiddingTeam"] = Enum.Parse(typeof(Enums.Team), drow["HighTeam"].ToString()).ToString();
                else
                    drow["BiddingTeam"] = string.Empty;
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
                if (drow["SigningTeam"].ToString() != drow["Team"].ToString() && (bool)drow["QualifyingOffer"])
                    drow["Compensation"] = "Qualified Pick";
            }
            return dtable;
        }

        public static void UploadFreeAgents(string filePath, decimal qualifyingOffer)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("filePath", filePath);
            inParameters.Add("qualifyingOffer", qualifyingOffer);
            ExecuteProcedure("ImportFreeAgents", inParameters, null);
        }

        public static void SignHighPlayers()
        {
            ExecuteProcedure("SignPlayers", null, null);
        }

        public static void SignAllPlayers()
        {
            ExecuteProcedure("SignAllPlayers", null, null);
        }

        public static void SignExpiredPlayers(double hoursToExpiry)
        {
            int secondsToExpiry = (int)(hoursToExpiry * 3600);
            Hashtable inParameters = new Hashtable();
            inParameters.Add("secondsToExpiry", secondsToExpiry);
            ExecuteProcedure("SignExpiredPlayers", inParameters, null);
        }

        public static void match(int playerId)
        {
            Hashtable inParameters = new Hashtable();
            inParameters.Add("playerId", playerId);
            ExecuteProcedure("Match", inParameters, null);
        }

        public static void StartFreeAgency()
        {
            ExecuteProcedure("startFreeAgency", null, null);
        }
    }
}