using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace Ex02
{

    // TODO: Try to optimize the code (PrintGameBoard).
    // TODO: Consider refactoring the code to print without a StringBuilder.
    public class ConsoleUI
    {
        private readonly StringBuilder r_Board;
        private readonly Game r_Game;

        public ConsoleUI(Game i_Game)
        {
            r_Game = i_Game;
            r_Board = new StringBuilder();
        }

        public GameSettings GetGameSettings()
        {
            Console.WriteLine("Welcome!");
            string player1Name = getName();
            int boardSize = getBoardSize();
            int gameMode = getPlayerType();
            string player2Name = gameMode == (int)GameSettings.eGameMode.PlayerVsPlayer ? getName() : "Computer";
            
            return new GameSettings(gameMode, boardSize, player1Name, player2Name);
        }

        private int getPlayerType()
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
                typeIsValid = Player.IsPlayerTypeValid(userInputChoice);

                // TODO: Change to a method if possible
                if (typeIsValid != 0)
                {
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }

            return typeIsValid;
        }

        private string getName()
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

        private int getBoardSize()
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

        public void DisplayGameBoard()
        {
            // ClearScreen();
            displayColumnHeaders();
            displayRows();
            displayCurrentPlayerTurn();
            Console.Write(r_Board.ToString());
            Console.Write(PlayerMove());
        }

        private void displayCurrentPlayerTurn()
        {
            string currentPlayer = r_Game.GetCurrentPlayer();
            char currentPlayerPiece = r_Game.GetCurrentPlayerPiece();

            r_Board.Append($"{currentPlayer}'s turn ({currentPlayerPiece}): ");
        }

        private void displayPreviousMove(ref MovePiece io_PlayerPiece)
        {
            char currentPlayerPiece = r_Game.GetCurrentPlayerPiece();

            displayCurrentPlayerTurn();
        }

        private void displayInnerCells(int i_BoardSize, int i)
        {
            int boardSquareSize = 3;

            for (int j = 0; j < i_BoardSize; j++)
            {
                char getPieceType = r_Game.GetPieceAtPosition(new PiecePosition(i, j));

                if (getPieceType == '\0')
                {
                    r_Board.Append(new string(' ', boardSquareSize));
                }
                else
                {
                    r_Board.Append(' ').Append(getPieceType).Append(' ');
                }

                r_Board.Append('|');
            }

            r_Board.AppendLine();
        }

        private void displayRows()
        {
            int boardSize = r_Game.Board.GetBoardSize;
            
            for (int i = 0; i < boardSize; i++)
            {
                displayRowSeparators(boardSize);
                r_Board.Append($"{(char)('A' + i)}").Append("|");
                displayInnerCells(boardSize, i);
            }

            displayRowSeparators(boardSize);
            r_Board.AppendLine();
        }

        private void displayRowSeparators(int i_BoardSize)
        {
            r_Board.Append(' ').Append(new string('=', 4 * i_BoardSize + 1)).AppendLine();
        }

        private void printSpaces()
        {
            r_Board.Append(new string(' ', 3));
        }

        private void displayColumnHeaders()
        {

            printSpaces();
            for (int i = 0; i < r_Game.Board.GetBoardSize; i++)
            {
                r_Board.Append($"{(char)('a' + i)}");
                printSpaces();
            }
            printSpaces();
            r_Board.AppendLine();
        }

        public MovePiece PlayerMove()
        {
            string playerMove = string.Empty;
            MovePiece playerPiece = null;
            bool isFormatValid = false;
            bool isMoveValid = false;

            while (!isFormatValid && !isMoveValid)
            {
                playerMove = Console.ReadLine();
                if (!IsValidTurnFormat(playerMove, ref playerPiece))
                {
                    Console.WriteLine("Invalid input. Please try again in the following format: (e.g., 'Fg>Eh'): ");
                    continue;
                }

                isFormatValid = true;
                if (!r_Game.IsMakeMove(playerPiece.FromPosition, playerPiece.ToPosition))
                { 
                  Console.WriteLine("Invalid move.");
                  isFormatValid = false;
                  continue;
                }
                isMoveValid = true;
            }

            ClearScreen();
            // displayPreviousMove(ref playerPiece);
            return null;
        }

        public bool IsValidTurnFormat(string i_UserTurnInput, ref MovePiece io_playerPiece)
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
                if (userInput.Length != 2 || userInput[0].Length != 2 || userInput[1].Length != 2)
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
            io_playerPiece = new MovePiece(fromInput, toInput);

            return isTurnFormatValid;
        }

        public void ClearScreen()
        {
            r_Board.Clear();
            Screen.Clear();
        }
    }
}
