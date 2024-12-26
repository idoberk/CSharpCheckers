using System;
using Ex02.ConsoleUtils;
using static Ex02.GameUI;


namespace Ex02
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Would you like to play on a small (6x6), medium (8x8) or large (10x10) board: ");
            string userInputGameSize = Console.ReadLine();
            int.TryParse(userInputGameSize, out int boardSize);
            GameBoard gameBoard = new GameBoard(boardSize);
            GameUI board = new GameUI(gameBoard);
            if (isInputValid(boardSize))
            {
                board.PrintGameBoard(boardSize);
            }
        }


        private static bool isInputValid(int i_num)
        {
            bool isValid = false;

            while (!isValid)
            {
                if (Enum.IsDefined(typeof(eBoardSize), i_num))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }

            return isValid;
        }
    }

}
