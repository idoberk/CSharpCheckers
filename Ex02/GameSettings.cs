using System;

namespace Ex02
{
    public class GameSettings
    {
        private int m_BoardSize;
        private Player m_Player1;
        private Player m_Player2;

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public GameSettings(int i_BoardSize, string i_Player1) //, Player i_Player2)
        {
            m_BoardSize = i_BoardSize;
            m_Player1 = new Player(i_Player1);
            // m_Player2 = i_Player2;
        }
    }
}
