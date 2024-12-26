using System;

namespace Ex02
{
    public class GameBoard
    {
        public int[,] m_Board;
        private int m_BoardSize;

        public GameBoard(int size)
        {
            m_BoardSize = size;
            m_Board = new int[m_BoardSize, m_BoardSize];
            InitializeBoard();
        }

        public int GetPieceAtPosition(int row, int col)
        {
            return m_Board[row, col];
        }

        private void InitializeBoard()
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

        //public int[,] BuildMat(int i_BoardSize)
        //{
        //    //m_Board = new int[i_BoardSize, i_BoardSize];
        //    int middleRow = i_BoardSize / 2 - 1;

        //    for (int i = 0; i < i_BoardSize; i += 2)
        //    {
        //        if (i > middleRow)
        //        {
        //            for (int j = 0; j < i_BoardSize; j++)
        //            {
        //                if (j % 2 == 0)
        //                {
        //                    printFirstPattern(ref i, 1);
        //                }
        //                else
        //                {
        //                    printSecondPattern(ref i, 1);
        //                }
        //            }
        //        }
        //        else if (i == middleRow || i == middleRow + 1)
        //        {
        //            for (int j = 0; j < i_BoardSize; j++)
        //            {
        //                m_Board[i, j] = 0;
        //            }
        //        }
        //        else
        //        {
        //            for (int j = 0; j < i_BoardSize; j++)
        //            {
        //                if (j % 2 == 0)
        //                {
        //                    printSecondPattern(ref i, 2);
        //                }
        //                else
        //                {
        //                    printFirstPattern(ref i, 2);
        //                }
        //            }
        //        }


        //    }
        //    return m_Board;
        //}

        //private void printFirstPattern(ref int k, int num)
        //{
        //    for (int i = 0; i < m_Board.Length; i += 2)
        //    {
        //        m_Board[k, i] = num;
        //    }

        //}


        //private void printSecondPattern(ref int k,int num)
        //{
        //    for (int i = 1; i < m_Board.Length; i += 2)
        //    {
        //        m_Board[k, i] = num;
        //    }
        //}
    }
}
