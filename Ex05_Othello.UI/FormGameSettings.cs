using Ex05_Othello.Logic;
using System;
using System.Windows.Forms;

namespace Ex05_Othello.UI
{
    public partial class FormGameSettings : Form
    {
        private Board.eBoardSize m_BoardSize = Board.eBoardSize.size6x6;
        private readonly Players r_CurrentPlayers;
        private FormOthelloGame m_FormOthelloGame;

        public FormGameSettings()
        {
            InitializeComponent();
            r_CurrentPlayers = new Players();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonPlayAgianstComputer_Click(object sender, EventArgs e)
        {
            r_CurrentPlayers.IsComputer = true;
            startNewGame();
        }

        private void buttonPlayAgianstFriend_Click(object sender, EventArgs e)
        {
            r_CurrentPlayers.IsComputer = false;
            startNewGame();
        }

        private void startNewGame()
        {
            r_CurrentPlayers.FirstPlayer = "Black";
            r_CurrentPlayers.SecondPlayer = "White";
            Hide();
            m_FormOthelloGame = new FormOthelloGame(m_BoardSize, r_CurrentPlayers);
            _ = m_FormOthelloGame.ShowDialog();
            Close();
        }

        private void buttonChangeBoardSize_Click(object sender, EventArgs e)
        {
            int minBoarderSize = (int)Board.eBoardSize.size6x6;
            int maxBoarderSize = (int)Board.eBoardSize.size12x12;
            m_BoardSize += ((int)m_BoardSize == maxBoarderSize) ? -(maxBoarderSize - minBoarderSize) : 2;
            string incOrDecrese = ((int)m_BoardSize == maxBoarderSize) ? "decrease" : "increase";
            buttonChangeBoardSize.Text = string.Format("Board size: {0}x{0} (click to {1})", (int)m_BoardSize, incOrDecrese);
        }
    }
}
