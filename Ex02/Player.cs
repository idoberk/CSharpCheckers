using System;


namespace Ex02
{
    public class Player
    {
        private static readonly int sr_MaxPlayerNameLength = 20;
        private string m_PlayerName;

        public string Name
        {
            get { return m_PlayerName; }
        }

        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
        }

        public enum ePlayerPieceType
        {
            Empty = ' ',
            OPlayer = 'O',
            OPlayerKing = 'U',
            XPlayer = 'X',
            XPlayerKing = 'K'
        }

        
        public static bool IsPlayerNameValid(string i_PlayerName)
        {
            bool validPlayerName = i_PlayerName.Length <= sr_MaxPlayerNameLength && !(i_PlayerName.Contains(" ")) && i_PlayerName != string.Empty;
            return validPlayerName;
        }
    }
}
