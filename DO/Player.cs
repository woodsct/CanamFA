using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanamLiveFA.DO
{
    public class Player
    {
        private int m_Id;
        private string m_playerName;
        private Enums.Team m_currentTeam;
        private bool m_majors;
        private bool m_signed;
        private DateTime m_bidTime;
        private bool m_qualified;

        public void PopulatePlayer(int id, string name, Enums.Team currentTeam, bool majors, bool signed, DateTime bidTime, bool qualified)
        {
            m_Id = id;
            m_playerName = name;
            m_currentTeam = currentTeam;
            m_majors = majors;
            m_signed = signed;
            m_bidTime = bidTime;
            m_qualified = qualified;

        }

        public int Id
        { 
            get 
            {
                return m_Id;
            }
        }

        public string PlayerName
        { 
            get 
            {
                return m_playerName;
            }
        }

        public Enums.Team CurrentTeam
        {
            get
            {
                return m_currentTeam;
            }
        }

        public double HighestBid
        { get; set; }

        public double HomeTeamBid
        { get; set; }

        public bool Majors
        {
            get
            {
                return m_majors;
            }
        }

        public bool HomeTeamRightToMatch
        { get; set; }

        public bool Signed
        {
            get
            {
                return m_signed;
            }
        }

        public DateTime BidTime
        {
            get
            {
                return m_bidTime;
            }
        }

        public bool Qualified
        {
            get
            {
                return m_qualified;
            }
        }
    }
}