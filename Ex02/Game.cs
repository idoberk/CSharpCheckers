using System;
using System.Collections.Generic;
using static Ex02.Player;

namespace Ex02
{
    public class Game
    {
        // TODO: Check if the piece is king
        private GameBoard m_GameBoard;
        private int m_BoardSize;
        private ePlayerNumber m_CurrentPlayer;
        private ePlayerNumber m_NextPlayer;
        private string m_Player1Name;
        private string m_Player2Name;
        private List<MovePiece> m_RegularMoves;
        private List<MovePiece> m_CaptureMoves;
        private PiecePosition m_LastMovePosition;

        public GameBoard Board
        {
            get { return m_GameBoard; }
          
            private set
            {
                m_GameBoard = value;
            }
        }

        public ePlayerNumber CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
            private set
            {
                m_CurrentPlayer = value;
            }

        }

        public ePlayerNumber NextPlayer
        {
            get
            {
                return m_NextPlayer;
            }
            private set
            {
                m_NextPlayer = value;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }
            private set
            {
                m_Player1Name = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }
            private set
            {
                m_Player2Name = value;
            }
        }

        public List<MovePiece> RegularMoves
        {
            get
            {
                return m_RegularMoves;
            }
            private set
            {
                m_RegularMoves = value;
            }
        }

        public List<MovePiece> CaptureMoves
        {
            get
            {
                return m_CaptureMoves;
            }
            private set
            {
                m_CaptureMoves = value;
            }
        }

        public bool HasCaptureMoves
        {
            get { return CaptureMoves.Count > 0; }
        }

        public Game(GameSettings i_Settings)
        {
            m_BoardSize = i_Settings.Board.GetBoardSize;
            Board = i_Settings.Board;
            Player1Name = i_Settings.Player1.Name;
            Player2Name = i_Settings.Player2.Name;
            CurrentPlayer = ePlayerNumber.Player1;
            RegularMoves = new List<MovePiece>();
            CaptureMoves = new List<MovePiece>();
        }

        public bool IsMoveExecuted(MovePiece i_AttemptedMove)
        {
            //if(!i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_CaptureMoves) )
            //{
            //    GetValidMoves();
            //}
            // if (m_LastMovePosition != null && !i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_CaptureMoves))

            //if (m_LastMovePosition == null)
            //{
            //    GetValidMoves();
            //}

            GetValidMoves();

            if (m_LastMovePosition != null)
            {
                 m_CaptureMoves.Clear();
                 m_RegularMoves.Clear();

                 char lastMovedPiece = GetPieceAtPosition(m_LastMovePosition);

                 addMovesToList(m_LastMovePosition, lastMovedPiece, i_IsCapture: true);
            }

            bool isMoveExecuted = false;
          
            if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_CaptureMoves))
            {
                MakeMove(i_AttemptedMove, i_IsCapture : true);
                isMoveExecuted = true;
                //m_LastMovePosition = i_AttemptedMove.FromPosition;
            }

            else if (i_AttemptedMove.IsMoveInList(i_AttemptedMove, m_RegularMoves))
            {
                MakeMove(i_AttemptedMove, i_IsCapture: false);
                isMoveExecuted = true;
            }

            return isMoveExecuted;
        }

        public char GetPieceAtPosition(PiecePosition i_PiecePosition)
        {
            return m_GameBoard.GetPieceAtPosition(i_PiecePosition);
        }

        public string GetCurrentPlayer()
        {
            return CurrentPlayer == ePlayerNumber.Player1 ? Player1Name : Player2Name;
        }

        public char GetCurrentPlayerPiece()
        {
            char currentPlayerPiece = CurrentPlayer == ePlayerNumber.Player1
                                          ? (char)ePlayerPieceType.OPlayer
                                          : (char)ePlayerPieceType.XPlayer;

            return currentPlayerPiece;
        }

        private void switchTurn()
        {
            CurrentPlayer = CurrentPlayer == ePlayerNumber.Player1
                                ? ePlayerNumber.Player2
                                : ePlayerNumber.Player1;
        }

        public void GetValidMoves()
        {
            List<PiecePosition> currentPlayerPieces = m_GameBoard.GetPiecesPositionsList(m_CurrentPlayer);
            bool isCapture = true;
            
            m_RegularMoves.Clear();
            m_CaptureMoves.Clear();

            getPossibleMoves(currentPlayerPieces, isCapture);

            if (m_CaptureMoves.Count == 0)
            {
                isCapture = false;
                getPossibleMoves(currentPlayerPieces, isCapture);
            }
        }

        private void getPossibleMoves(List<PiecePosition> i_CurrentPlayerPieces, bool i_IsCapture)
        {
            foreach (PiecePosition piecePosition in i_CurrentPlayerPieces)
            {
                char piece = GetPieceAtPosition(piecePosition);

                addMovesToList(piecePosition, piece, i_IsCapture);
            }
        }

        private void addMovesToList(PiecePosition i_CurrentPiecePosition, char i_CurrentPiece, bool i_IsCapture)
        {
            int[] moveDirection = getPieceDirection(i_CurrentPiece);
            int distance = i_IsCapture ? 2 : 1;

            foreach (int rowDirection in moveDirection)
            {
                foreach (int colDirection in new[] { -distance, distance })
                {
                        PiecePosition newPosition = new PiecePosition(
                        i_CurrentPiecePosition.Row + rowDirection * distance,
                        i_CurrentPiecePosition.Col + colDirection);
                    MovePiece possibleMove = new MovePiece(i_CurrentPiecePosition, newPosition);

                    if (IsMakeMove(possibleMove.FromPosition, possibleMove.ToPosition))
                    {
                        bool isCaptureAvailable = IsCaptureAvailable(possibleMove);

                        if (i_IsCapture && isCaptureAvailable)
                        {
                            m_CaptureMoves.Add(possibleMove);
                        }
                        else 
                        {
                            m_RegularMoves.Add(possibleMove);
                        }
                    }
                }
            }
        }

        private int[] getPieceDirection(char i_Piece)
        {
            int[] pieceDirection = null;

            if (IsPieceKing(i_Piece))
            {
                pieceDirection = new[] { -1, 1 };
            }
            else if (i_Piece == (char)ePlayerPieceType.OPlayer)
            {
                pieceDirection = new[] { 1 };
            }
            else
            {
                pieceDirection = new[] { -1 };
            }

            return pieceDirection;
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
            int[] playerDirection = getPieceDirection(currentPlayerPiece);
            bool isValidDirection = Math.Sign(i_ToPosition.Row - i_FromPosition.Row) == playerDirection[0];

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

                    if (capturedPiece != (char)ePlayerPieceType.Empty
                        && capturedPiece != GetCurrentPlayerPiece())
                    {
                        isCapturingMove = true;
                    }
                }
            }

            return isCapturingMove;
        }

        public bool IsValidMoveDistance(PiecePosition i_FromPosition, PiecePosition i_ToPosition, bool i_IsCaptureMove)
        {
            bool isValidDistance = false;
            int distance = i_IsCaptureMove ? 2 : Math.Abs(i_ToPosition.Row - i_FromPosition.Row);

            if ((distance == 2 && i_IsCaptureMove) || distance == 1)
            {
                isValidDistance = true;
            }

            return isValidDistance;
        }

        public bool IsMakeMove(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            MovePiece moveInput = new MovePiece(i_FromPosition, i_ToPosition);
            bool isMoveValid = isPlayerPiece(i_FromPosition);

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
                bool isCaptureMove = IsCaptureAvailable(moveInput);
                bool isValidDistance = IsValidMoveDistance(i_FromPosition, i_ToPosition, isCaptureMove);

                if (!isValidDistance) 
                { 
                    isMoveValid = false; 
                }
            }

            return isMoveValid;
        }

        //public void MakeMove(MovePiece i_PlayerMove)
        //{
        //    // bool isCaptureMove = m_CaptureMoves.Count == 0;// IsCaptureAvailable(i_PlayerMove);

        //    if (isCaptureMove)
        //    {
        //        m_GameBoard.CapturePiece(i_PlayerMove);
        //    }
        //    else
        //    {
        //        switchTurn();
        //    }

        //    m_GameBoard.MovePlayerPiece(i_PlayerMove);
        //}

        public void MakeMove(MovePiece i_PlayerMove, bool i_IsCapture)
        {
            if (i_IsCapture) 
            {
                m_GameBoard.CapturePiece(i_PlayerMove);
                m_GameBoard.MovePlayerPiece(i_PlayerMove);

                m_LastMovePosition = i_PlayerMove.ToPosition;

                m_CaptureMoves.Clear();

                addMovesToList(i_PlayerMove.ToPosition, GetPieceAtPosition(m_LastMovePosition), i_IsCapture);
                //addMovesToList(i_PlayerMove.ToPosition, GetPieceAtPosition(i_PlayerMove.ToPosition), i_IsCapture);

                if (m_CaptureMoves.Count == 0)
                {
                    m_LastMovePosition = null;
                    switchTurn();
                }
            }
            else
            {
                m_GameBoard.MovePlayerPiece(i_PlayerMove);
                m_LastMovePosition = null;
                switchTurn();
            }

            if (i_PlayerMove.ToPosition.Row == m_BoardSize - 1)
            {
                m_GameBoard.MakeKing(i_PlayerMove.ToPosition);
            }
            // TODO: Refactor to prevent using MovePlayerPiece twice.
        }
    }
}

