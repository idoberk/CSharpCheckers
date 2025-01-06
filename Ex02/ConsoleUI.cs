using System;
using Ex02.ConsoleUtils;

namespace Ex02
{
    public class ConsoleUI
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome!");
        }

        public static string GetPlayerName()
        {
            bool isValidName = false;
            string playerName = string.Empty;

            Console.WriteLine("Enter Player name (must be shorter than 20 characters and cannot contain spaces): ");
            while (!isValidName)
            {
                playerName = Console.ReadLine();
                bool nameIsValid = Player.IsPlayerNameValid(playerName);

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

        public static string GetPlayerInput()
        {
            return Console.ReadLine();
        }

        public static int GetPlayerType()
        {
            bool isValidChoice = false;
            string userInputChoice = string.Empty;
            int typeIsValid = 0;

            Console.WriteLine("Do you want to play against another player or against a computer (1 / 2):");
            Console.WriteLine("1. Human");
            Console.WriteLine("2. Computer");

            while (!isValidChoice)
            {
                userInputChoice = Console.ReadLine();
                typeIsValid = IsPlayerTypeValid(userInputChoice);

                if (typeIsValid != 0)
                {
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid type input, please try again.");
                }
            }

            return typeIsValid;
        }

        public static int IsPlayerTypeValid(string i_UserChoice)
        {
            int userChoice = 0;

            if (int.TryParse(i_UserChoice, out int playerType))
            {
                if (playerType == 1)
                {
                    userChoice = (int)Player.ePlayerType.Human;
                }
                else if (playerType == 2)
                {
                    userChoice = (int)Player.ePlayerType.Computer;
                }
            }

            return userChoice;
        }

        public static int GetBoardSize()
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
                boardSize = GameBoard.SetBoardSize(userInputGameSize);

                if (boardSize != 0)
                {
                    isValidSize = true;
                }
                else
                {
                    Console.WriteLine("Invalid board size, please try again.");
                }
            }

            return boardSize;
        }

        public static void DisplayGame(GameBoard i_GameBoard)
        {
            ClearScreen();
            DisplayGameBoard(i_GameBoard);

            //if (!i_IsGameOver)
            //{
            //    displayPreviousPlayerMove(i_CurrentPlayer, i_NextPlayer, i_CaptureMoves);
            //    displayCurrentPlayerTurn(i_CurrentPlayer);
            //}
        }

        public static void ClearScreen()
        {
            Screen.Clear();
        }

        public static void DisplayCurrentPlayerTurn(Player i_CurrentPlayer)
        {
            if (i_CurrentPlayer.IsComputer())
            {
                Console.WriteLine($"{i_CurrentPlayer.Name}'s turn press 'enter' to see it's move: ");
            }
            else
            {
                Console.Write($"{i_CurrentPlayer.Name}'s turn ({i_CurrentPlayer.PlayerPiece}): ");
            }
        }

        public static void DisplayPreviousPlayerMove(Player i_CurrentPlayer)
        {
            Console.WriteLine($"{i_CurrentPlayer.Name}'s move was ({i_CurrentPlayer.PlayerPiece}): {i_CurrentPlayer.LastMove}");
        }

        public static void DisplayGameBoard(GameBoard i_GameBoard)
        {
            printSpaces();
            displayColumnHeaders(i_GameBoard);
            printSpaces();
            displayRows(i_GameBoard);
        }

        private static void displayColumnHeaders(GameBoard i_GameBoard)
        {
            for (int i = 0; i < i_GameBoard.GetBoardSize; i++)
            {
                Console.Write($"{(char)('a' + i)}");
                printSpaces();
            }
        }

        private static void printSpaces()
        {
            string spaces = new string(' ', 3);

            Console.Write(spaces);
        }

        private static void displayRows(GameBoard i_GameBoard)
        {
            int boardSize = i_GameBoard.GetBoardSize;

            Console.WriteLine();
            for (int i = 0; i < boardSize; i++)
            {
                displayRowSeparators(boardSize);
                Console.Write($"{(char)('A' + i)}|");
                displayInnerCells(boardSize, i, i_GameBoard);
            }

            displayRowSeparators(boardSize);
            Console.WriteLine();
        }

        private static void displayRowSeparators(int i_BoardSize)
        {
            string separators = new string('=', 4 * i_BoardSize + 1);

            Console.WriteLine($" {separators}");
        }

        private static void displayInnerCells(int i_BoardSize, int i, GameBoard i_GameBoard)
        {
            int boardSquareSize = 3;

            for (int j = 0; j < i_BoardSize; j++)
            {
                char getPieceType = i_GameBoard.GetPieceAtPosition(new PiecePosition(i, j));

                if (getPieceType == '\0')
                {
                    string spaces = new string(' ', boardSquareSize);
                    Console.Write(spaces);
                }
                else
                {
                    Console.Write($" {getPieceType} ");
                }
                Console.Write('|');
            }
            Console.WriteLine();
        }

        public static bool IsValidTurnFormat(string i_UserTurnInput, out MovePiece o_playerPiece)
        {
            bool isTurnFormatValid = !i_UserTurnInput.Contains(">") || !string.IsNullOrWhiteSpace(i_UserTurnInput);
            string[] userInput = null;
            int fromRow = 0;
            int fromCol = 0;
            int toRow = 0;
            int toCol = 0;

            if (isTurnFormatValid)
            {
                userInput = i_UserTurnInput.Split('>');
                if (userInput.Length != 2 || userInput[0].Length != 2 || userInput[1].Length != 2 ||
                    !char.IsUpper(userInput[0][0]) || !char.IsUpper(userInput[1][0]) ||
                    !char.IsLower(userInput[0][1]) || !char.IsLower(userInput[1][1]))
                {
                    isTurnFormatValid = false;
                }
                else
                {
                    fromRow = userInput[0][0] - 'A';
                    fromCol = userInput[0][1] - 'a';
                    toRow = userInput[1][0] - 'A';
                    toCol = userInput[1][1] - 'a';
                }
            }

            PiecePosition fromInput = new PiecePosition(fromRow, fromCol);
            PiecePosition toInput = new PiecePosition(toRow, toCol);
            o_playerPiece = new MovePiece(fromInput, toInput);

            return isTurnFormatValid;
        }

        public static void DisplayInvalidMoveMessage()
        {
            Console.WriteLine("Invalid move. Please try again.");
        }

        public static bool IsAnotherGame()
        {
            DisplayAnotherGameMessage();

            bool isAnotherGame = false;
            string userInput = GetPlayerInput();

            while (!userInput.Equals("Y") && !userInput.Equals("y") && !userInput.Equals("N") && !userInput.Equals("n"))
            {
                DisplayInvalidInputMessage();
                userInput = GetPlayerInput();
            }

            if (userInput.Equals("Y") || userInput.Equals("y"))
            {
                isAnotherGame = true;
            }

            return isAnotherGame;
        }

        public static void DisplayInvalidFormatMessage()
        {
            Console.WriteLine("Invalid input. Please try again in the following format: (e.g., 'Fg>Eh'): ");
        }

        public static void DisplayPlayerScores(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine($"{i_Player1.Name}'s score = {i_Player1.Score}");
            Console.WriteLine($"{i_Player2.Name}'s score = {i_Player2.Score}");
        }

        public static void DisplayInvalidEnterKeyMessage()
        {
            Console.WriteLine("Invalid input. Please press enter");
        }

        public static void DisplayAnotherGameMessage()
        {
            Console.WriteLine("Would you like to play another game? (Y / N)");
        }

        public static void DisplayInvalidInputMessage()
        {
            Console.WriteLine("Invalid input. Please try again.");
        }

        public static void DisplayWinnerMessage(Player i_WinningPlayer)
        {
            Console.WriteLine($"Congratulations! {i_WinningPlayer.Name} won!");
        }
    }
}