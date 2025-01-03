using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace Ex02
{

    // TODO: Try to optimize the code (PrintGameBoard).
    // TODO: Consider refactoring the code to print without a StringBuilder.
    // TODO: Refactor PlayerMove
    public class ConsoleUI
    {
        private readonly Game r_Game;

        public ConsoleUI(Game i_Game)
        {
            r_Game = i_Game;
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
            // StringBuilder consoleBoard = new StringBuilder();
            // ClearScreen(consoleBoard);
            ClearScreen();
            // displayColumnHeaders(consoleBoard);
            displayColumnHeaders();
            // displayRows(board);
            // displayCurrentPlayerTurn(consoleBoard);
            displayCurrentPlayerTurn();
            // Console.Write(consoleBoard.ToString());
            // Console.Write(PlayerMove(consoleBoard));
            // PlayerMove();
            //Console.Write(PlayerMove());
        }

        private void displayCurrentPlayerTurn() //StringBuilder i_ConsoleBoard)
        {
            string currentPlayer = r_Game.GetCurrentPlayer();
            char currentPlayerPiece = r_Game.GetCurrentPlayerPiece();

            Console.Write($"{currentPlayer}'s turn ({currentPlayerPiece}): ");
            // i_ConsoleBoard.Append($"{currentPlayer}'s turn ({currentPlayerPiece}): ");
        }

        private void displayPreviousMove(ref MovePiece io_PlayerPiece) //, StringBuilder i_ConsoleBoard)
        {
            char currentPlayerPiece = r_Game.GetCurrentPlayerPiece();

            // displayCurrentPlayerTurn(i_ConsoleBoard);
            displayCurrentPlayerTurn();
        }

        private void displayInnerCells(int i_BoardSize, int i) //, StringBuilder i_ConsoleBoard)
        {
            int boardSquareSize = 3;

            for (int j = 0; j < i_BoardSize; j++)
            {
                char getPieceType = r_Game.GetPieceAtPosition(new PiecePosition(i, j));

                if (getPieceType == '\0')
                {
                    string spaces = new string(' ', boardSquareSize);
                    Console.Write(spaces);
                    // i_ConsoleBoard.Append(new string(' ', boardSquareSize));
                }
                else
                {
                    Console.Write($" {getPieceType} ");
                    //i_ConsoleBoard.Append(' ').Append(getPieceType).Append(' ');
                }
                Console.Write('|');
                //i_ConsoleBoard.Append('|');
            }
            Console.WriteLine();
            //i_ConsoleBoard.AppendLine();
        }

        private void displayRows()//StringBuilder i_ConsoleBoard)
        {
            int boardSize = r_Game.Board.GetBoardSize;
            
            for (int i = 0; i < boardSize; i++)
            {
                displayRowSeparators(boardSize);
                //displayRowSeparators(boardSize, i_ConsoleBoard);
                Console.Write($"{(char)('A' + i)}|");
                //i_ConsoleBoard.Append($"{(char)('A' + i)}").Append("|");
                // displayInnerCells(boardSize, i, i_ConsoleBoard);
                displayInnerCells(boardSize, i);
            }

            displayRowSeparators(boardSize);
            Console.WriteLine();
            //i_ConsoleBoard.AppendLine();
        }

        private void displayRowSeparators(int i_BoardSize) //, StringBuilder i_ConsoleBoard)
        {
            string separators = new string('=', 4 * i_BoardSize + 1);
            Console.WriteLine($" {separators}");
            // i_ConsoleBoard.Append(' ').Append(new string('=', 4 * i_BoardSize + 1)).AppendLine();
        }

        private void printSpaces() //StringBuilder i_ConsoleBoard)
        {
            string spaces = new string(' ', 3);
            Console.Write(spaces);
            // i_ConsoleBoard.Append(new string(' ', 3));
        }

        private void displayColumnHeaders() //StringBuilder i_ConsoleBoard)
        {
            printSpaces();
            //printSpaces(i_ConsoleBoard);
            for (int i = 0; i < r_Game.Board.GetBoardSize; i++)
            {
                Console.Write($"{(char)('a' + i)}");
                // i_ConsoleBoard.Append($"{(char)('a' + i)}");
                printSpaces();
                //printSpaces(i_ConsoleBoard);
            }
            printSpaces();
            //printSpaces(i_ConsoleBoard);
            Console.WriteLine();
            // i_ConsoleBoard.AppendLine();
            displayRows();
            //displayRows(i_ConsoleBoard);
        }

        public void PlayerMove() //StringBuilder i_ConsoleBoard)
        {
            bool isTurnFinished = false;

            while (!isTurnFinished)
            {
                string playerMoveInput = Console.ReadLine();
                MovePiece moveAttempt = null;

                if (IsValidTurnFormat(playerMoveInput, ref moveAttempt))
                {
                    if (r_Game.IsMoveExecuted(moveAttempt))
                    {
                        if (!r_Game.HasCaptureMoves)
                        {
                            isTurnFinished = true;
                        }
                        DisplayGameBoard();
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again in the following format: (e.g., 'Fg>Eh'): ");
                }
            }
        }

        //public void PlayerMove() //StringBuilder i_ConsoleBoard)
        //{
        //    r_Game.GetValidMoves();
        //    bool moveExecuted = false;

        //    while (!moveExecuted)
        //    {
        //        string playerMoveInput = Console.ReadLine();
        //        MovePiece moveAttempt = null;
        //        if (!IsValidTurnFormat(playerMoveInput, ref moveAttempt))
        //        {
        //            Console.WriteLine("Invalid input. Please try again in the following format: (e.g., 'Fg>Eh'): ");
        //            continue;
        //        }


        //        bool isCaptureMoveAvailable = moveAttempt.IsMoveInList(moveAttempt, r_Game.CaptureMoves);
        //        bool isRegularMoveAvailable = moveAttempt.IsMoveInList(moveAttempt, r_Game.RegularMoves);
        //        int availableCaptureMoves = r_Game.CaptureMoves.Count;

        //        if (availableCaptureMoves > 0)
        //        {
        //            if (isCaptureMoveAvailable)
        //            {
        //                r_Game.MakeMove(moveAttempt, isCaptureMoveAvailable);
        //                moveExecuted = true;
        //            }
        //            // Console.WriteLine("Invalid move.");
        //            // isFormatValid = false;
        //            // continue;
        //        }
        //        else if (isRegularMoveAvailable)
        //        {
        //            r_Game.MakeMove(moveAttempt, isCaptureMoveAvailable);
        //            moveExecuted = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid move.");
        //        }
        //        // isMoveValid = true;
        //    }
        //}

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

        public void ClearScreen()//StringBuilder i_ConsoleBoard)
        {
            //i_ConsoleBoard.Clear();
            Screen.Clear();
        }
    }
}
