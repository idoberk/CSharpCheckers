using System;

namespace Ex02
{
    public class GameSettings
    {
        private GameBoard m_GameBoard;
        private int m_GameMode;
        private Player m_Player1;
        private Player m_Player2;

        public enum eGameMode
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer = 2
        }

        public GameBoard Board
        {
            get { return m_GameBoard; }
            private set { m_GameBoard = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public static GameSettings CreateNewGame()
        {
            ConsoleUI ui = new ConsoleUI(null);
            return ui.GetGameSettings();

        }

        public GameSettings(int i_GameMode, int i_BoardSize, string i_Player1, string i_Player2)
        {
            m_GameMode = i_GameMode;
            m_GameBoard = new GameBoard(i_BoardSize);
            m_Player1 = new Player(i_Player1);
            m_Player2 = new Player(i_Player2);
        }
    }
}
