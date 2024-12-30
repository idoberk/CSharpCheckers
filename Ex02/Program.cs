using System;
using Ex02.ConsoleUtils;
using static Ex02.ConsoleUI;


namespace Ex02
{
    public class Program
    {
        static void Main()
        {
            GameSettings settings = GetGameSettings();
            GameBoard gameBoard = new GameBoard(settings.BoardSize);
            ConsoleUI board = new ConsoleUI(gameBoard);
            board.PrintGameBoard(settings.BoardSize);

            Console.ReadLine();
        }
    }
}
