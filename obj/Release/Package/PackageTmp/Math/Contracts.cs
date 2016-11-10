using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CanamLiveFA;
using CanamLiveFA.DO;

namespace CanamLiveFA
{
    public abstract class Contracts
    {
        //everything is assumed to be in thousands of dollars not millions for simplicity (etc 1 million  = 1000)
        public static void freeAgentValueCalc (Player playerObj, int numberOfYears, int contractAmount, bool noTrade, ref string errorStr, User userObj, bool timeCheck)
        {
            double totalContractValue;
            switch (numberOfYears) // cswitch/case to determine if the offer meets minimum requirements
            {
                case 1:
                    if (contractAmount < 40 && !playerObj.Majors)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    if (contractAmount < 100 && playerObj.Majors)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    break;
                case 2:
                    if (contractAmount < 500)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    break;
                case 3:
                    if (contractAmount < 600)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    break;
                case 4:
                    if (contractAmount < 700)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    break;
                case 5:
                    if (contractAmount < 800)
                        errorStr = "Error: Contract is lower then current yearly minimum";
                    break;
                default:
                    errorStr = "error code #1 has occured";
                    break;
            }
            if (string.IsNullOrWhiteSpace(errorStr))
            {
                if (noTrade) 
                    totalContractValue = (double)((numberOfYears + 1) * contractAmount * 1.1 );
                else 
                    totalContractValue = (numberOfYears + 1) * contractAmount;
                if ((totalContractValue >= (playerObj.HighestBid * 1.1) ) || !timeCheck) // for the final 5 minutes this check does not have to happen...
                {
                    if (playerObj.HomeTeamBid >= (0.5 * totalContractValue) )
                    {// if check to see if the home team has the right to match
                        playerObj.HomeTeamRightToMatch = true;
                    }
                    else playerObj.HomeTeamRightToMatch = false;
                    if (timeCheck || (totalContractValue >= (playerObj.HighestBid * 1.1) ) )
                    {
                        DAL.Player.InsertPlayerContract(playerObj.Id, totalContractValue, false, playerObj.HomeTeamRightToMatch, numberOfYears, noTrade, contractAmount, userObj.Team);
                    }
                }
                else errorStr = "Error: Contract is not 10%% greater then current high contract";
            }
        }
    }
}