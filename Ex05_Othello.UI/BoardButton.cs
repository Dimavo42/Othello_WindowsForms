using Ex05_Othello.Logic;
using Ex05_Othello.UI.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othello.UI
{
    public class BoardButton : Button
    {
        private eCellStatus m_ButtonStatus;

        public BoardButton(Cell i_Location, eCellStatus i_CurrentStatus)
        {
            ChangeButtonStatus = i_CurrentStatus;
            CurrentLocation = i_Location;
        }

        public eCellStatus ChangeButtonStatus
        {
            get { return m_ButtonStatus; }
            set { changeButtonColor(value); }
        }

        public Cell CurrentLocation { get; set; }

        private void changeButtonColor(eCellStatus i_currentColor)
        {
            m_ButtonStatus = i_currentColor;
            switch (m_ButtonStatus)
            {
                case eCellStatus.Free:
                    {
                        Enabled = true;
                        BackgroundImage = null;
                        BackColor = Color.Green;
                    }
                    break;
                case eCellStatus.Black:
                    {
                        Enabled = false;
                        BackgroundImage = Resources.CoinRed;
                        BackgroundImageLayout = ImageLayout.Stretch;
                        BackColor = Color.White;
                    }
                    break;
                case eCellStatus.White:
                    {
                        Enabled = false;
                        BackgroundImage = Resources.CoinYellow;
                        BackgroundImageLayout = ImageLayout.Stretch;
                        BackColor = Color.White;
                    }
                    break;
                case eCellStatus.Blocked:
                    {
                        Enabled = false;
                        BackgroundImage = null;
                        BackColor = Color.White;
                    }
                    break;
            }
        }

    }
}
