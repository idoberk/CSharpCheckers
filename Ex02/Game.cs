using System;

namespace Ex02
{
    public class Game
    {
        private GameBoard m_GameBoard;
        private int m_BoardSize;
        private Player.ePlayerNumber m_CurrentPlayer;
        private Player.ePlayerNumber m_NextPlayer;
        private string m_Player1Name;
        private string m_Player2Name;

        public GameBoard Board
        {
            get
            {
                return m_GameBoard;
            }
            private set
            {
                m_GameBoard = value;
            }
        }

        public Player.ePlayerNumber CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            private set { m_CurrentPlayer = value; }

        }

        public Player.ePlayerNumber NextPlayer
        {
            get { return m_NextPlayer; }
            private set { m_NextPlayer = value; }
        }

        public string Player1Name
        {
            get { return m_Player1Name; }
            private set { m_Player1Name = value; }
        }

        public string Player2Name
        {
            get { return m_Player2Name; }
            private set { m_Player2Name = value; }
        }

        public Game(GameSettings i_Settings)
        {
            m_BoardSize = i_Settings.Board.GetBoardSize;
            Board = i_Settings.Board;
            Player1Name = i_Settings.Player1.Name;
            Player2Name = i_Settings.Player2.Name;
            CurrentPlayer = Player.ePlayerNumber.Player1;
        }

        public char GetPieceAtPosition(PiecePosition i_PiecePosition)
        {
            return m_GameBoard.GetPieceAtPosition(i_PiecePosition);
        }

        public string GetCurrentPlayer()
        {
            return CurrentPlayer == Player.ePlayerNumber.Player1 ? Player1Name : Player2Name;
        }

        public char GetCurrentPlayerPiece()
        {
            char currentPlayerPiece = CurrentPlayer == Player.ePlayerNumber.Player1
                                          ? (char)Player.ePlayerPieceType.OPlayer
                                          : (char)Player.ePlayerPieceType.XPlayer;

            return currentPlayerPiece;
        }

        private void switchTurn()
        {
            CurrentPlayer = CurrentPlayer == Player.ePlayerNumber.Player1
                                ? Player.ePlayerNumber.Player2
                                : Player.ePlayerNumber.Player1;
        }

        private bool isPlayerPiece(PiecePosition i_PiecePosition)
        {
            char charAtPosition = GetPieceAtPosition(i_PiecePosition);
            bool isPlayerPiece = charAtPosition == GetCurrentPlayerPiece();

            return isPlayerPiece;

        }

        private bool isValidDirection(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            char currentPlayerPiece = GetCurrentPlayerPiece();
            int playerDirection = currentPlayerPiece == (char)Player.ePlayerPieceType.OPlayer ? 1 : -1;
            bool isValidDirection = Math.Sign(i_ToPosition.Row - i_FromPosition.Row) == playerDirection;
            
            return isValidDirection;
        }

        private bool isMoveDiagonal(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            int rowDifference = Math.Abs(i_ToPosition.Row - i_FromPosition.Row);
            int colDifference = Math.Abs(i_ToPosition.Col - i_FromPosition.Col);
            bool isDiagonal = rowDifference == colDifference;

            return isDiagonal;
        }

        public bool IsCaptureAvailable(MovePiece i_MovePiece)
        {
            bool isCapturingMove = false;

            if (isMoveDiagonal(i_MovePiece.FromPosition, i_MovePiece.ToPosition))
            {
                int rowDiff = Math.Abs(i_MovePiece.ToPosition.Row - i_MovePiece.FromPosition.Row);

                if (rowDiff == 2)
                {
                    int middleRow = (i_MovePiece.FromPosition.Row + i_MovePiece.ToPosition.Row) / 2;
                    int middleCol = (i_MovePiece.FromPosition.Col + i_MovePiece.ToPosition.Col) / 2;
                    char capturedPiece = m_GameBoard.GetPieceAtPosition(new PiecePosition(middleRow, middleCol));

                    if (capturedPiece != (char)Player.ePlayerPieceType.Empty && capturedPiece != GetCurrentPlayerPiece())
                    {
                        isCapturingMove = true;
                    }
                }
            }

            return isCapturingMove;
        }

        public bool IsMakeMove(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            MovePiece moveInput = new MovePiece(i_FromPosition, i_ToPosition);
            bool isMoveValid = true;
            bool isCaptureMove = false;

            //bool isBoundariesValid = m_GameBoard.IsMoveInBoundaries(i_FromPosition, i_ToPosition);
            //bool isValidFromPosition = isPlayerPiece(i_FromPosition);
            //bool isValidMoveDirection = isValidDirection(i_FromPosition, i_ToPosition);
            //bool isDiagonal = isMoveDiagonal(i_FromPosition, i_ToPosition);
            //bool isValidDistance = m_GameBoard.IsValidMoveDistance(i_FromPosition, i_ToPosition);

            if (!isPlayerPiece(i_FromPosition))
            {
                isMoveValid = false;
            }

            if (isMoveValid && !isValidDirection(i_FromPosition, i_ToPosition))
            {
                isMoveValid = false;
            }

            if (isMoveValid && !isMoveDiagonal(i_FromPosition, i_ToPosition))
            {
                isMoveValid = false;
            }

            if (isMoveValid && !m_GameBoard.IsValidMove(moveInput))
            {
                isMoveValid = false;
            }

            if (isMoveValid)
            {
                isCaptureMove = IsCaptureAvailable(moveInput);
                if (isCaptureMove)
                {
                    m_GameBoard.CapturePiece(moveInput);
                }
                else
                {
                    switchTurn();
                }
                m_GameBoard.MovePlayerPiece(moveInput);
            }

            return isMoveValid;
            //if (isBoundariesValid && isValidFromPosition && isValidMoveDirection && isDiagonal && isValidDistance)
            //{
            //    m_GameBoard.MovePlayerPiece(moveInput);
            //    switchTurn();
            //}

            // return isBoundariesValid && isValidFromPosition && isValidMoveDirection && isDiagonal && isValidDistance;
        }

        //public bool IsCaptureAvailableRight(PiecePosition i_ToPosition)
        //{
        //    bool isCaptureAvailable = false;
        //    PiecePosition nextPosition = null;

        //    if (CurrentPlayer == Player.ePlayerNumber.Player1)
        //    {
        //        nextPosition = new PiecePosition(i_ToPosition.Row + 2, i_ToPosition.Col + 2);
        //        if (m_GameBoard.IsMoveInBoundaries(nextPosition,null) )
        //        {
        //            if (IsPositionAvailable(nextPositio))
        //            {
        //                is GetPieceAtPosition(i_ToPosition.Row + 1, i_ToPosition.Col + 1) == other player piece
        //            };
        //        }


        //        isCaptureAvailable = true;

        //        //GetPieceAtPosition(nextPosition);
        //    }
        //    else
        //    {
        //        nextPosition = new PiecePosition(i_ToPosition.Row - 2, i_ToPosition.Col - 2);
        //        IsPositionAvailable(nextPosition);
        //        isCaptureAvailable = true;
        //    }
        //    return
        //}
    }
}
