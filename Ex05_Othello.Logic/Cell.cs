namespace Ex05_Othello.Logic
{


    public delegate void CellChangedStateEvent(Cell i_currentCell);
    public class Cell
    {
        public event CellChangedStateEvent CellChanged;
        private char m_CurrentChar;

        public Cell(int i_Row, int i_Column, char i_Sign = 'B')
        {
            Row = i_Row;
            Column = i_Column;
            m_CurrentChar = i_Sign;
            Name = null;
        }

        public Cell(int i_Row, int i_Column, char i_Sign, string i_Name)
        {
            Row = i_Row;
            Column = i_Column;
            Sign = i_Sign;
            Name = i_Name;
        }
        public string Name { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public eCellStatus CellStatus { get; set; }

        public char Sign
        {
            get
            {
                return m_CurrentChar;
            }
            set
            {
                m_CurrentChar = value;
                CellStatus = (eCellStatus)m_CurrentChar;
                if (CellChanged!=null)
                {
                    CellChanged.Invoke(this);
                }
            }
        }

    }
}
