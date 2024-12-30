using System;

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
    }
}
