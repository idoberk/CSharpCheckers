using System;

namespace Ex02
{
    public class GameBoard
    {
        private char[,] m_Board;
        private readonly int r_BoardSize;

        private enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public GameBoard(int i_GameBoardSize)
        {
            r_BoardSize = i_GameBoardSize;
            m_Board = new char[r_BoardSize, r_BoardSize];

            initializeBoard();
        }

        public char GetPieceAtPosition(PiecePosition i_PiecePosition)
        {
            return m_Board[i_PiecePosition.Row, i_PiecePosition.Col];
        }

        private void initializeBoard()
        {
            int numRows = (r_BoardSize - 2) / 2;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.OPlayer;
                    }
                    else
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                    }
                }
            }

            for (int i = numRows; i <= numRows + 1; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                }
            }

            for (int i = r_BoardSize - numRows; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.XPlayer;
                    }
                    else
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                    }
                }
            }
        }

        public static int IsBoardSizeValid(string i_BoardSizeInput)
        {
            int returnState = 0;

            if (int.TryParse(i_BoardSizeInput, out int boardSize))
            {
                if (boardSize == 1)
                {
                    returnState = (int)eBoardSize.Small;
                }
                else if (boardSize == 2)
                {
                    returnState = (int)eBoardSize.Medium;
                }
                else if (boardSize == 3)
                {
                    returnState = (int)eBoardSize.Large;
                }
            }

            return returnState;
        }
    }
}
