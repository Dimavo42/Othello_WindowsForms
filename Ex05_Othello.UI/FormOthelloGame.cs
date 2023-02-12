using Ex05_Othello.Logic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othello.UI
{
    public partial class FormOthelloGame : Form
    {
        private readonly BoardButton[,] r_boardButtons;
        private readonly Panel r_Panel = new Panel();
        private readonly GameLogic r_GameLogic;
        private readonly bool r_isComputer;
        private bool m_isGameRunning = false;

        public FormOthelloGame(Board.eBoardSize i_eBoardSize, Players i_CurrentPlayers)
        {
            InitializeComponent();
            r_boardButtons = new BoardButton[(int)i_eBoardSize, (int)i_eBoardSize];
            initButtons((int)i_eBoardSize);
            CenterToScreen();
            r_GameLogic = new GameLogic(i_eBoardSize, i_CurrentPlayers);
            r_GameLogic.BoardHaveBeenChanged += onUpdateBoard_Opreation;
            r_GameLogic.InitBoardButtonsActions();
            r_GameLogic.OnGameEnd += onGameEnd_Opreatuion;
            r_GameLogic.ChangeTurnAction += onChangeTurn_Opreation;
            r_GameLogic.OnTurnHaveBeenSwapped += onTurnHaveBeenSwapped_Opreation;
            m_isGameRunning = true;
            if (i_CurrentPlayers.IsComputer)
            {
                r_isComputer = true;
                timer1.Interval = 2000;
            }
            else
            {
                r_isComputer = false;
            }
        }

        private void initButtons(int i_BoardSize)
        {
            Size = new Size((i_BoardSize * 50) + 30, (i_BoardSize * 50) + 40);
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    r_boardButtons[i, j] = new BoardButton(new Cell(i, j), eCellStatus.Blocked)
                    {
                        Size = new Size(50, 50),
                        Location = new Point(i * 50, j * 50)
                    };
                    r_boardButtons[i, j].Click += button_Click;
                    r_Panel.Controls.Add(r_boardButtons[i, j]);
                }
            }
            r_Panel.Dock = DockStyle.Fill;
            Controls.Add(r_Panel);
            Text = string.Format("Othello - {0}'s Turn", eCellStatus.Black);
        }

        private void onUpdateBoard_Opreation(Cell i_NewLocation)
        {
            r_boardButtons[i_NewLocation.Row, i_NewLocation.Column].ChangeButtonStatus = i_NewLocation.CellStatus;

        }

        private void onGameEnd_Opreatuion(string message)
        {
            m_isGameRunning = false;
            DialogResult dialogResult = MessageBox.Show(message, "Othello", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                Close();
                return;
            }
            else
            {
                r_GameLogic.RestartGame();
                m_isGameRunning = true;
                Text = string.Format("Othello - {0}'s Turn", eCellStatus.Black);
            }
        }

        private void onChangeTurn_Opreation(string i_Name)
        {
            Text = string.Format("Othello - {0}'s Turn", i_Name);
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (m_isGameRunning)
            {
                BoardButton button = (BoardButton)sender;
                r_GameLogic.MakeMove(button.CurrentLocation);
                if (!r_isComputer)
                {
                    r_GameLogic.SwitchSides();
                }
                else
                {
                    Text = string.Format("Othello - {0}'s Turn", eCellStatus.White);
                    timer1.Interval = 2000;
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
        }

        private void FormOthelloGame_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_isGameRunning)
            {
                r_GameLogic.SwitchSides();
                timer1.Stop();
                timer1.Enabled = false;
            }
        }

        private void onTurnHaveBeenSwapped_Opreation()
        {
            m_isGameRunning = false;
            string message = "You dont have moves your turn have been swapped";
            DialogResult dialogResult = MessageBox.Show(message, "Othello", MessageBoxButtons.OK);
            if (dialogResult == DialogResult.OK)
            {
                m_isGameRunning = true;
            }
        }

    }
}
