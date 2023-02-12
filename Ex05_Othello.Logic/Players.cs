namespace Ex05_Othello.Logic
{
    public class Players
    {
        public string FirstPlayer { get; set; }
        public string SecondPlayer { get; set; }
        public bool IsComputer { get; set; }

        public char GetOpponentSign(char i_CurrentColor)
        {
            return (i_CurrentColor == (char)eCellStatus.Black) ?
            (char)eCellStatus.White :
                (char)eCellStatus.Black;
        }

        public string GetOpponentName(string i_CurrentName)
        {
            return (i_CurrentName == FirstPlayer) ? SecondPlayer : FirstPlayer;
        }
    }
}

    