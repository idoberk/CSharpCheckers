using System;

namespace Ex02
{
    public class GameBoard
    {
        public int[,] m_Board;
        private int m_BoardSize;

        public GameBoard(int i_GameBoardSize)
        {
            m_BoardSize = i_GameBoardSize;
            m_Board = new int[m_BoardSize, m_BoardSize];

            initializeBoard();
        }

        public int GetPieceAtPosition(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        private void initializeBoard()
        {
            int numRows = (m_BoardSize - 2) / 2;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = 1;
                    }
                }
            }

            for (int i = m_BoardSize - numRows; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = 2;
                    }
                }
            }
        }
    }
}
