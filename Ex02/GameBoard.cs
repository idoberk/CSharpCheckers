using System;
using System.Collections.Generic;

namespace Ex02
{
    public class GameBoard
    {
        private char[,] m_Board;
        private readonly int r_BoardSize;

        private enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public GameBoard(int i_GameBoardSize)
        {
            r_BoardSize = i_GameBoardSize;
            m_Board = new char[r_BoardSize, r_BoardSize];

            initializeBoard();
        }

        public int GetBoardSize
        {
            get { return r_BoardSize; }
        }

        public char GetPieceAtPosition(PiecePosition i_PiecePosition)
        {
            return m_Board[i_PiecePosition.Row, i_PiecePosition.Col];
        }

        private void initializeBoard()
        {
            int numRows = (r_BoardSize - 2) / 2;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.OPlayer;
                    }
                    else
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                    }
                }
            }

            for (int i = numRows; i <= numRows + 1; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                }
            }

            for (int i = r_BoardSize - numRows; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.XPlayer;
                    }
                    else
                    {
                        m_Board[i, j] = (char)Player.ePlayerPieceType.Empty;
                    }
                }
            }
        }

        public static int IsBoardSizeValid(string i_BoardSizeInput)
        {
            int userChoice = 0;

            if (int.TryParse(i_BoardSizeInput, out int boardSize))
            {
                if (boardSize == 1)
                {
                    userChoice = (int)eBoardSize.Small;
                }
                else if (boardSize == 2)
                {
                    userChoice = (int)eBoardSize.Medium;
                }
                else if (boardSize == 3)
                {
                    userChoice = (int)eBoardSize.Large;
                }
            }

            return userChoice;
        }

        public void MovePlayerPiece(MovePiece i_MovePiece)
        {
            m_Board[i_MovePiece.ToPosition.Row, i_MovePiece.ToPosition.Col] = m_Board[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col];
            m_Board[i_MovePiece.FromPosition.Row, i_MovePiece.FromPosition.Col] = (char)Player.ePlayerPieceType.Empty;

        }

        public List<MovePiece> GetValidMoves(PiecePosition i_PiecePosition, Player i_CurrentPlayer)
        {
            List<MovePiece> validMoves = new List<MovePiece>();
            char currentPlayerPiece = (char)i_CurrentPlayer.PieceType;

            if (currentPlayerPiece == ' ' || currentPlayerPiece != GetPieceAtPosition(i_PiecePosition))
            {
                validMoves = null;
            }

            return validMoves;
        }

        public void CapturePiece(MovePiece i_MovePiece)
        {
            int middleRow = (i_MovePiece.FromPosition.Row + i_MovePiece.ToPosition.Row) / 2;
            int middleCol = (i_MovePiece.FromPosition.Col + i_MovePiece.ToPosition.Col) / 2;

            m_Board[middleRow, middleCol] = (char)Player.ePlayerPieceType.Empty;
        }

        public bool IsValidMove(MovePiece i_MovePiece)
        {
            bool isValid = true;

            if (!IsMoveInBoundaries(i_MovePiece.FromPosition, i_MovePiece.ToPosition))
            {
                isValid = false;
            }

            if (isValid && GetPieceAtPosition(i_MovePiece.ToPosition) != (char)Player.ePlayerPieceType.Empty)
            {
                isValid = false;
            }

            if (isValid && !IsValidMoveDistance(i_MovePiece.FromPosition, i_MovePiece.ToPosition))
            {
                isValid = false;
            }

            if (isValid && !IsPositionAvailable(i_MovePiece.ToPosition))
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsMoveInBoundaries(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {

            bool isFromRowMoveValid = i_FromPosition.Row >= 0 && i_FromPosition.Row < GetBoardSize;
            bool isFromColMoveValid = i_FromPosition.Col >= 0 && i_FromPosition.Col < GetBoardSize;
            bool isToRowMoveValid = i_ToPosition.Row >= 0 && i_ToPosition.Row < GetBoardSize;
            bool isToColMoveValid = i_ToPosition.Col >= 0 && i_ToPosition.Col < GetBoardSize;

            return isFromRowMoveValid && isFromColMoveValid && isToRowMoveValid && isToColMoveValid;
        }

        public bool IsValidMoveDistance(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            int moveDistance = Math.Abs(i_ToPosition.Row - i_FromPosition.Row);
            bool isValidDistance = moveDistance == 1;

            return isValidDistance;
        }

        public bool IsPositionAvailable(PiecePosition i_ToPosition)
        {
            return GetPieceAtPosition(i_ToPosition) == (char)Player.ePlayerPieceType.Empty;
        }

    }
}
