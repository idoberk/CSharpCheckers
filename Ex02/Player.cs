using System;

namespace Ex02
{
    public class Player
    {
        private static readonly int sr_MaxPlayerNameLength = 20;
        private string m_PlayerName;
        private ePlayerNumber m_PlayerNumber;
        private ePlayerPieceType m_PlayerPiece;

        public string Name
        {
            get { return m_PlayerName; }
        }

        public ePlayerPieceType PieceType
        {
            get
            {
                return m_PlayerPiece;
            }
            private set
            {
                m_PlayerPiece = value;
            }
        }

        public ePlayerNumber PlayerNumber
        {
            get { return m_PlayerNumber; }
        }

        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
            PieceType = m_PlayerNumber == ePlayerNumber.Player1 ? ePlayerPieceType.OPlayer : ePlayerPieceType.XPlayer;
        }

        public void PlayerKingPiece()
        {
            PieceType = PieceType == ePlayerPieceType.OPlayer
                            ? ePlayerPieceType.OPlayerKing
                            : ePlayerPieceType.XPlayerKing;
        }

        public enum ePlayerPieceType
        {
            Empty = ' ',
            OPlayer = 'O',
            OPlayerKing = 'U',
            XPlayer = 'X',
            XPlayerKing = 'K'
        }

        public enum ePlayerType
        {
            Human = 1,
            Computer = 2
        }

        public enum ePlayerNumber
        {
            Player1 = 1,
            Player2 = 2
        }

        public static bool IsPlayerNameValid(string i_PlayerName)
        {
            bool validPlayerName = i_PlayerName.Length <= sr_MaxPlayerNameLength && !(i_PlayerName.Contains(" "))
                                   && i_PlayerName != string.Empty;

            return validPlayerName;
        }

        public static bool IsPieceKing(char i_Piece)
        {
            bool isKing = (i_Piece == (char)ePlayerPieceType.OPlayerKing || i_Piece == (char)ePlayerPieceType.XPlayerKing);

            return isKing;
        }

        public static int IsPlayerTypeValid(string i_UserChoice)
        {
            int userChoice = 0;

            if (int.TryParse(i_UserChoice, out int playerType))
            {
                if (playerType == 1)
                {
                    userChoice = (int)ePlayerType.Human;
                }
                else if (playerType == 2)
                {
                    userChoice = (int)ePlayerType.Computer;
                }
            }

            return userChoice;
        }

        public bool IsPieceBelongToCurrentPlayer(char i_PlayerPiece)
        {
            bool isPieceOwner = i_PlayerPiece == (char)PieceType;

            return isPieceOwner;
        }

        public char GetPiece()
        {
            return (char)PieceType;
        }
    }
        
}