using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CanamLiveFA.DAL;

namespace CanamLiveFA.BLL
{
    public class User
    {
        public static bool LoginUser(string userName, string password)
        {
            DO.User userObj = DAL.User.GetUserByUserName(userName);
            if (userObj != new DO.User() && userObj.Password == password)
            {
                BLL.CommonFunctions.SetSessionValue("User", userObj);
                return true;
            }
            else
                return false;
        }
    }
}