using System;

namespace Ex05_Othello.Logic
{
    public class GameScores
    {
        private int m_WhiteWins;
        private int m_BlackWins;

        public GameScores()
        {
            m_WhiteWins = 0;
            m_BlackWins = 0;
        }

        public string MakeReport(Board i_Board)
        {
            int Black = 0, White = 0;

            for (int rows = 0; rows < i_Board.Size; rows++)
            {
                for (int cols = 0; cols < i_Board.Size; cols++)
                {
                    if (i_Board.GameBoard[rows, cols].CellStatus == eCellStatus.Black)
                    {
                        Black++;
                    }
                    else if (i_Board.GameBoard[rows, cols].CellStatus == eCellStatus.White)
                    {
                        White++;
                    }
                }
            }
            eCellStatus winnerIs = decideWhoWon(Black, White);
            string message = winnerIs == eCellStatus.Black || winnerIs == eCellStatus.White
                ? string.Format("{0} Won!!({1}/{2}) ({3}/{4}){5}Would you like another round?", winnerIs, Black, White, m_BlackWins, m_WhiteWins, Environment.NewLine)
                : string.Format("Its tie score is:({0}/{1}) ({2}/{3}){4}Would you like another round?", Black, White, m_BlackWins, m_WhiteWins, Environment.NewLine);
            return message;
        }

        private eCellStatus decideWhoWon(int i_Black, int i_White)
        {
            eCellStatus status;
            if (i_Black > i_White)
            {
                m_BlackWins++;
                status = eCellStatus.Black;
            }
            else if (i_White > i_Black)
            {
                m_WhiteWins++;
                status = eCellStatus.White;
            }
            else
            {
                status = eCellStatus.Free;
            }
            return status;

        }

    }
}
