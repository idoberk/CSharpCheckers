using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace Ex02
{

    // TODO: Try to optimize the code (PrintGameBoard).
    public class ConsoleUI
    {
        private static StringBuilder s_Board;
        private GameBoard board;

        public ConsoleUI(GameBoard board)
        {
            this.board = board;
            s_Board = new StringBuilder();
        }

        public static GameSettings GetGameSettings()
        {
            Console.WriteLine("Welcome!");
            string playerName = getName();
            int boardSize = getBoardSize();
            
            return new GameSettings(boardSize, playerName);
        }

        private static string getName()
        {
            bool isValidName = false;
            string playerName = string.Empty;

            Console.WriteLine("Enter Player name (must be shorter than 20 characters and cannot contain spaces): ");
            while (!isValidName)
            {
               playerName = Console.ReadLine();
               bool nameIsValid = Player.IsPlayerNameValid(playerName);

               // TODO: Change to a method if possible
               if (nameIsValid)
               {
                   isValidName = true;
               }
               else
               {
                   Console.WriteLine("Invalid name, please try again.");
               }

            }
            return playerName;
        }

        private static int getBoardSize()
        {
            bool isValidSize = false;
            string userInputGameSize = string.Empty;
            int boardSize = 0;

            Console.WriteLine("Select the board size (1 / 2 / 3):");
            Console.WriteLine("1. Small (6x6)");
            Console.WriteLine("2. Medium (8x8)");
            Console.WriteLine("3. Large (10x10)");

            while (!isValidSize)
            {
                userInputGameSize = Console.ReadLine();
                boardSize = GameBoard.IsBoardSizeValid(userInputGameSize);

                // TODO: Change to a method if possible
                if (boardSize != 0)
                {
                    isValidSize = true;
                }
                else
                {
                    Console.WriteLine("Invalid size, please try again.");
                }
            }

            return boardSize;
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

            for (int j = 0; j < i_BoardSize; j++)
            {
                char getPieceType = board.GetPieceAtPosition(new PiecePosition(i, j));

                if (getPieceType == '\0')
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

        private void printRowLabel(int i_BoardSize)
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
