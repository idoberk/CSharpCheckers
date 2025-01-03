namespace Ex02
{
    public class Program
    {
        public static void Main()
        {
            GameSettings settings = GameSettings.CreateNewGame();
            Game game = new Game(settings);
            ConsoleUI ui = new ConsoleUI(game);

            for (int i = 0; i < 100; i++)
            {   
                ui.DisplayGameBoard();
                
                //ui.PlayerMove();
                // ui.ClearScreen();
            }
        }
    }
}

