using System.Collections.Generic;

namespace Ex05_Othello.Logic
{
    public class Board
    {
        public enum eBoardSize
        {
            size6x6 = 6,
            size8x8 = 8,
            size10x10 = 10,
            size12x12 = 12
        }

        private readonly eBoardSize r_Size;

        public Board(eBoardSize i_Size)
        {
            r_Size = i_Size;
            int rowColSize = (int)i_Size;
            GameBoard = new Cell[rowColSize, rowColSize];
            for (int row = 0; row < rowColSize; row++)
            {
                for (int col = 0; col < rowColSize; col++)
                {
                    GameBoard[row, col] = new Cell(row, col);
                }
            }

        }

        public void Init(Players i_Players)
        {
            int middleLocation = (Size / 2) - 1;
            GameBoard[middleLocation, middleLocation].Sign = (char)eCellStatus.Black;
            GameBoard[middleLocation, middleLocation].Name = i_Players.FirstPlayer;
            GameBoard[middleLocation + 1, middleLocation + 1].Sign = (char)eCellStatus.Black;
            GameBoard[middleLocation + 1, middleLocation + 1].Name = i_Players.FirstPlayer;
            GameBoard[middleLocation + 1, middleLocation].Sign = (char)eCellStatus.White;
            GameBoard[middleLocation + 1, middleLocation].Name = i_Players.SecondPlayer;
            GameBoard[middleLocation, middleLocation + 1].Sign = (char)eCellStatus.White;
            GameBoard[middleLocation, middleLocation + 1].Name = i_Players.SecondPlayer;
        }

        public Cell[,] GameBoard { get; }
        public int Size
        {
            get
            { return (int)r_Size; } 
        }

        public void UpdateCellsValidMoves(List<Cell> i_CellsToFlip)
        {
            foreach (Cell cell in i_CellsToFlip)
            {
                GameBoard[cell.Row, cell.Column].Sign = cell.Sign;
                GameBoard[cell.Row, cell.Column].Name = cell.Name;
            }
        }

        public bool IsInsideBoard(int i_Row, int i_Column)
        {
            return (i_Row < (int)r_Size) &&
                 (i_Row >= 0) &&
                 (i_Column < (int)r_Size) &&
                 (i_Column >= 0);
        }

        public void Reset()
        {
            foreach (Cell cell in GameBoard)
            {
                cell.Name = null;
                cell.Sign = 'B';
            }
        }

        public void ResetCellListToFree(List<Cell> i_List)
        {
            foreach (Cell cell in i_List)
            {
                GameBoard[cell.Row, cell.Column].Sign = ' ';
                GameBoard[cell.Row, cell.Column].Name = null;
            }
        }

        public void RestCellListBlock(List<Cell> i_List)
        {
            foreach (Cell cell in i_List)
            {
                GameBoard[cell.Row, cell.Column].Sign = 'B';
                GameBoard[cell.Row, cell.Column].Name = null;
            }
        }

    }
}
