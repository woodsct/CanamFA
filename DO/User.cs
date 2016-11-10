using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanamLiveFA.DO
{
    public class User
    {
        private string m_UserName;
        private Enums.Team m_Team;
        private double m_BidFeesPayable;
        private double m_BidFeesReceivable;
        private int m_UserId;
        private string m_Password;
        private bool m_Commissioner;

        public User()
        {
        }

        public void PopulateUser(int UserId, string userName, Enums.Team team, string password, bool commissioner)
        {
            m_UserId = UserId;
            m_UserName = userName;
            m_Team = team;
            m_Password = password;
            m_Commissioner = commissioner;
        }

        public string UserName
        {
            get { return m_UserName; }
        }

        public int Id
        {
            get { return m_UserId; }
        }

        public Enums.Team Team
        {
            get { return m_Team; }
        }

        public double BidFeesPayable
        {
            get { return m_BidFeesPayable; }
        }

        public double BidFeesReceiveable
        {
            get { return m_BidFeesReceivable; }
        }

        public string Password
        {
            get { return m_Password; }
        }

        public bool Commissioner
        {
            get { return m_Commissioner; }
        }
    }
}