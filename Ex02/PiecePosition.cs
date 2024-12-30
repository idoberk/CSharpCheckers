using System;

namespace Ex02
{
    public class PiecePosition
    {
        private int m_Row;
        private int m_Col;

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        public PiecePosition(int i_Row, int i_Col)
        {
            Row = i_Row;
            Col = i_Col;
        }
    }
}
