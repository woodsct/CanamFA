using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;

namespace CanamLiveFA.DAL
{
    public class Bids: BaseDal
    {
        public static double BidFeesPayable(int userId)
        {
            double fees = 0;
            Hashtable inParameters = new Hashtable();
            inParameters.Add("userId", userId);

            SqlDataReader dataReader = SelectData("GetBidFeesPayable", inParameters, null);
            try
            {
                while (dataReader.Read())
                {
                    fees += GetDoubleValue(dataReader["Bid"]);
                }
            }
            catch
            { }

            return fees * .01;
        }

        public static double BidFeesReceivable(int userId)
        {
            double fees = 0;
            Hashtable inParameters = new Hashtable();
            inParameters.Add("userId", userId);

            SqlDataReader dataReader = SelectData("GetBidFeesReceivable", inParameters, null);
            try
            {
                while (dataReader.Read())
                {
                    fees += GetDoubleValue(dataReader["Bid"]);
                }
            }
            catch
            { }

            return fees * .01;
        }

        public static void UpdateBidFees(DO.User biddingUser, Enums.Team CurrentTeam)
        {
            double bidFees = DAL.Bids.BidFeesPayable(biddingUser.Id);
            DAL.User.UpdateUserBidFees(biddingUser.Id, bidFees, true);

            DO.User homeUser = DAL.User.GetUserByTeam(CurrentTeam);
            bidFees = DAL.Bids.BidFeesReceivable(biddingUser.Id);
            DAL.User.UpdateUserBidFees(homeUser.Id, bidFees, false);
        }
    }
}