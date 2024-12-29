using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace Ex02
{

    // TODO: Try to optimize the code (PrintGameBoard).
    public class GameUI
    {
        private static StringBuilder s_Board;
        private GameBoard board;

        public GameUI(GameBoard board)
        {
            this.board = board;
            s_Board = new StringBuilder();
        }

        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public enum ePlayerType
        {
            Empty = ' ',
            OPlayer = 'O',
            OPlayerKing = 'U',
            XPlayer = 'X',
            XPlayerKing = 'K'
        }

        public void PrintGameBoard(int i_BoardSize)
        {
            printSpaces();
            printColLabel(i_BoardSize);
            printSpaces();
            s_Board.AppendLine();
            printRowSeparators(i_BoardSize);
            printRowLabel(i_BoardSize);

            Console.Write(s_Board.ToString());
        }

        private void printInnerBoard(int i_BoardSize, int i)
        {
            int boardSquareSize = 3;

            for (int j = 0;  j < i_BoardSize; j++)
            {
                int getPieceType = board.GetPieceAtPosition(i, j);
                if (getPieceType == 0)
                {
                    s_Board.Append(new string(' ', boardSquareSize)).Append('|');
                }
                else
                {
                    s_Board.Append(' ').Append(getPieceType).Append(' ').Append('|');
                }
            }
            s_Board.AppendLine();
        }
        
        private  void printRowLabel(int i_BoardSize)
        {
            char rowLabel = 'A';

            for (int i = 0; i < i_BoardSize; i++)
            {
                s_Board.Append(rowLabel).Append("|");
                printInnerBoard(i_BoardSize, i);
                rowLabel++;
                printRowSeparators(i_BoardSize);
            }
        }

        private static void printRowSeparators(int i_BoardSize) 
        {
            s_Board.Append(' ').Append(new string('=', 4 * i_BoardSize + 1)).AppendLine();
        }


        private static void printSpaces()
        {
            s_Board.Append(new string(' ', 3));
        }

        private static void printColLabel(int i_num)
        {
            char colPosition = 'a';

            for (int i = 0; i < i_num; i++)
            {
                s_Board.Append(colPosition);
                colPosition++;
                printSpaces();
            }
        }

        public void ClearScreen()
        {
            Screen.Clear();
        }
    }
}
