using System;
using static Ex02.ConsoleUI;


namespace Ex02
{
    public class Program
    {
        static void Main()
        {
            GameSettings settings = GameSettings.CreateNewGame();
            Game game = new Game(settings);
            ConsoleUI ui = new ConsoleUI(game);
            //ui.DisplayGameBoard(settings.Board.GetBoardSize);
            for (int i = 0; i < 3; i++)
            {   
                ui.DisplayGameBoard();
                // ui.ClearScreen();
            }

            Console.ReadLine();
        }
    }
}
