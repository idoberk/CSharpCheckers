using System.Collections.Generic;

namespace Ex02
{
    public class MovePiece
    {
        private PiecePosition m_FromPosition;
        private PiecePosition m_ToPosition;

        
        public PiecePosition FromPosition
        {
            get { return m_FromPosition; }
            set { m_FromPosition = value; }
        }

        public PiecePosition ToPosition
        {
            get { return m_ToPosition; }
            set { m_ToPosition = value; }
        }

        public MovePiece(PiecePosition i_FromPosition, PiecePosition i_ToPosition)
        {
            m_FromPosition = i_FromPosition;
            m_ToPosition = i_ToPosition;
        }

        public bool IsMoveInList(MovePiece i_Move, List<MovePiece> i_MoveList)
        {
           bool isInList = false;

           foreach(MovePiece currentMove in i_MoveList)
           {
                if(IsEqualMove(currentMove, i_Move))
                {
                    isInList = true;
                    break;
                }
           }

           return isInList;
        }

        public static bool IsEqualMove(MovePiece i_Move1, MovePiece i_Move2)
        {
            return PiecePosition.IsEqualPosition(i_Move1.FromPosition, i_Move2.FromPosition) && PiecePosition.IsEqualPosition(i_Move1.ToPosition, i_Move2.ToPosition);
        }
    }
}
