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

        public void PopulatePlayer(int id, string name, Enums.Team currentTeam, bool majors)
        {
            m_Id = id;
            m_playerName = name;
            m_currentTeam = currentTeam;
            m_majors = majors;
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
    }
}