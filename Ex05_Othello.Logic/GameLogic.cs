using System;
using System.Collections.Generic;

namespace Ex05_Othello.Logic
{
    public delegate void TurnChangedOrGameEnded(string i_Sender);
    public delegate void TurnHaveBeenSwapped();
    public class GameLogic
    {
        private readonly Board r_GameBoard;
        private readonly Players r_PlayersInGame;
        private string m_CurrentPlayerMakeMove;
        private char m_CurrentSign;
        private readonly GameScores r_GameScore;
        private Dictionary<int, List<Cell>> m_ListOfValidMoves;
        public event CellChangedStateEvent BoardHaveBeenChanged;
        public event TurnChangedOrGameEnded OnGameEnd;
        public event TurnChangedOrGameEnded ChangeTurnAction;
        public event TurnHaveBeenSwapped OnTurnHaveBeenSwapped;

        public GameLogic(Board.eBoardSize i_eBoard, Players i_PlayersInGame)
        {
            r_PlayersInGame = i_PlayersInGame;
            m_CurrentPlayerMakeMove = r_PlayersInGame.FirstPlayer;
            r_GameBoard = new Board(i_eBoard);
            r_GameScore = new GameScores();
        }

        public void MakeMove(Cell i_PlayerMove)
        {
            submitMove(i_PlayerMove);
        }

        public void SwitchSides()
        {
            if (r_PlayersInGame.IsComputer)
            {

                playerVrsusComputer();
            }
            else
            {
                playerVrsusPlayer();
            }
        }

        private void playerVrsusPlayer()
        {
            string opponentName = r_PlayersInGame.GetOpponentName(m_CurrentPlayerMakeMove);
            char opponentSign = r_PlayersInGame.GetOpponentSign(m_CurrentSign);
            if (findIfHaveLegalMoves(opponentName, opponentSign))
            {
                changeTurns(opponentName);
                m_CurrentPlayerMakeMove = opponentName;
                m_CurrentSign = opponentSign;
                showAllValidMoves();
            }
            else
            {
                bool noMovesLeft = findIfHaveLegalMoves(m_CurrentPlayerMakeMove, m_CurrentSign);
                if (!noMovesLeft)
                {
                    OnendGame();
                }
                if (OnTurnHaveBeenSwapped != null && noMovesLeft)
                {
                    OnTurnHaveBeenSwapped.Invoke();
                    showAllValidMoves();
                }

            }
        }

        protected virtual void OnendGame()
        {
            string message = r_GameScore.MakeReport(r_GameBoard);
            if (OnGameEnd != null)
            {
                OnGameEnd.Invoke(message);
            }
        }

        public void RestartGame()
        {
            r_GameBoard.Reset();
            r_GameBoard.Init(r_PlayersInGame);
            m_ListOfValidMoves = getAllLegalMoves();
            showAllValidMoves();
        }

        private void playerVrsusComputer()
        {
            string opponentName = r_PlayersInGame.GetOpponentName(m_CurrentPlayerMakeMove);
            char opponentSign = r_PlayersInGame.GetOpponentSign(m_CurrentSign);
            if (findIfHaveLegalMoves(opponentName, opponentSign))
            {
                changeTurns(opponentName);
                m_CurrentPlayerMakeMove = opponentName;
                m_CurrentSign = opponentSign;
                computerMakeMove();
                playerVrsusComputerPlayerMove();
            }
            else
            {
                if (!findIfHaveLegalMoves(m_CurrentPlayerMakeMove, m_CurrentSign))
                {
                    OnendGame();
                }
                else
                {
                    if (OnTurnHaveBeenSwapped != null)
                    {
                        OnTurnHaveBeenSwapped.Invoke();
                    }
                }
            }
        }

        private void playerVrsusComputerPlayerMove()
        {
            string opponentName = r_PlayersInGame.GetOpponentName(m_CurrentPlayerMakeMove);
            char opponentSign = r_PlayersInGame.GetOpponentSign(m_CurrentSign);
            ChangeTurnAction(m_CurrentPlayerMakeMove);
            if (findIfHaveLegalMoves(opponentName, opponentSign))
            {
                ChangeTurnAction(opponentName);
                m_CurrentPlayerMakeMove = opponentName;
                m_CurrentSign = opponentSign;
                showAllValidMoves();
            }
            else
            {
                m_CurrentPlayerMakeMove = r_PlayersInGame.FirstPlayer;
                m_CurrentSign = (char)eCellStatus.Black;
                OnendGame();
            }
        }

        private void computerMakeMove()
        {
            Dictionary<int, List<Cell>> allComputerLegelMoves = new Dictionary<int, List<Cell>>();
            int indexOfMove = -1;
            for (int rows = 0; rows < r_GameBoard.Size; rows++)
            {
                for (int cols = 0; cols < r_GameBoard.Size; cols++)
                {
                    Cell currentPostion = new Cell(rows, cols, m_CurrentSign, m_CurrentPlayerMakeMove);
                    bool LegelMove = isMoveLegal(currentPostion, out List<Cell> currentMoveLegel);
                    if (LegelMove)
                    {
                        indexOfMove++;
                        currentMoveLegel.Add(currentPostion);
                        allComputerLegelMoves.Add(indexOfMove, currentMoveLegel);
                    }
                }
            }
            if (allComputerLegelMoves.Count > 0)
            {
                makeRandomMove(allComputerLegelMoves);
            }
        }

        private void makeRandomMove(Dictionary<int, List<Cell>> i_AllComputerLegelMoves)
        {
            Random random = new Random();
            int numberOfRandomMove = random.Next(0, i_AllComputerLegelMoves.Count);
            r_GameBoard.UpdateCellsValidMoves(i_AllComputerLegelMoves[numberOfRandomMove]);
        }

        protected virtual void changeTurns(string i_Turn)
        {
            if(ChangeTurnAction != null)
            {
                ChangeTurnAction.Invoke(i_Turn);
            }
        }

        private bool findIfHaveLegalMoves(string i_PlayerName, char i_Sign)
        {
            bool anyLegalMove = false;
            for (int rows = 0; rows < r_GameBoard.Size; rows++)
            {
                for (int cols = 0; cols < r_GameBoard.Size; cols++)
                {
                    Cell currentPostion = new Cell(rows, cols, i_Sign, i_PlayerName);
                    anyLegalMove = isMoveLegal(currentPostion, out _);
                    if (anyLegalMove)
                    {
                        break;
                    }
                }
                if (anyLegalMove)
                {
                    break;
                }
            }
            return anyLegalMove;
        }

        private void submitMove(Cell i_PlayerMove)
        {
            int index;
            List<Cell> cellList = new List<Cell>();
            foreach (KeyValuePair<int, List<Cell>> keyValuePair in m_ListOfValidMoves)
            {
                int numberOfItems = keyValuePair.Value.Count;
                Cell currentCellToCheck = keyValuePair.Value[numberOfItems - 1];
                if (i_PlayerMove.Column == currentCellToCheck.Column && i_PlayerMove.Row == currentCellToCheck.Row)
                {
                    index = keyValuePair.Key;
                    r_GameBoard.UpdateCellsValidMoves(m_ListOfValidMoves[index]);
                }
                else
                {
                    cellList.Add(currentCellToCheck);
                }
            }
            r_GameBoard.RestCellListBlock(cellList);
        }

        protected virtual void OnChangedCell(Cell cell)
        {
            if (BoardHaveBeenChanged!= null)
            {
                BoardHaveBeenChanged.Invoke(cell);
            }
        }

        public void InitBoardButtonsActions()
        {

            for (int row = 0; row < r_GameBoard.Size; row++)
            {
                for (int col = 0; col < r_GameBoard.Size; col++)
                {
                    r_GameBoard.GameBoard[row, col].CellChanged += OnChangedCell;
                }
            }
            r_GameBoard.Init(r_PlayersInGame);
            m_CurrentSign = (char)eCellStatus.Black;
            m_ListOfValidMoves = getAllLegalMoves();
            showAllValidMoves();
        }

        private void showAllValidMoves()
        {
            m_ListOfValidMoves = getAllLegalMoves();
            List<Cell> list = new List<Cell>();
            foreach (KeyValuePair<int, List<Cell>> keyValuePair in m_ListOfValidMoves)
            {
                int numberOfItems = keyValuePair.Value.Count;
                list.Add(keyValuePair.Value[numberOfItems - 1]);
            }
            r_GameBoard.ResetCellListToFree(list);
        }

        private Dictionary<int, List<Cell>> getAllLegalMoves()
        {
            Dictionary<int, List<Cell>> allLegelMoves = new Dictionary<int, List<Cell>>();
            int indexOfMove = -1;
            for (int rows = 0; rows < r_GameBoard.Size; rows++)
            {
                for (int cols = 0; cols < r_GameBoard.Size; cols++)
                {
                    Cell currentPostion = new Cell(rows, cols, m_CurrentSign, m_CurrentPlayerMakeMove);
                    bool LegelMove = isMoveLegal(currentPostion, out List<Cell> currentMoveLegel);
                    if (LegelMove)
                    {
                        indexOfMove++;
                        currentMoveLegel.Add(currentPostion);
                        allLegelMoves.Add(indexOfMove, currentMoveLegel);
                    }
                }
            }
            return allLegelMoves;
        }

        private bool isMoveLegal(Cell i_CurrentPostion, out List<Cell> i_NeededCellsToFlip)
        {

            bool ifCellHasName = r_GameBoard.GameBoard[i_CurrentPostion.Row, i_CurrentPostion.Column].Name != null;
            i_NeededCellsToFlip = ifCellHasName ? null : giveDirctionsToOpponentPostion(i_CurrentPostion);
            return !ifCellHasName && i_NeededCellsToFlip.Count > 0;
        }

        private List<Cell> giveDirctionsToOpponentPostion(Cell i_CurrentPostion)
        {
            List<Cell> checkedCells = new List<Cell>();
            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        continue;
                    }
                    checkedCells.AddRange(checkOpponentPostion(i_CurrentPostion, row, col));
                }
            }
            return checkedCells;
        }

        private List<Cell> checkOpponentPostion(Cell i_CurrentPostion, int i_Horizontal, int i_Vertical)
        {
            List<Cell> opponentPostion = new List<Cell>();
            int row = i_CurrentPostion.Row + i_Horizontal;
            int col = i_CurrentPostion.Column + i_Vertical;
            char oppentSign = r_PlayersInGame.GetOpponentSign(m_CurrentSign);
            while (r_GameBoard.IsInsideBoard(row, col) && r_GameBoard.GameBoard[row, col].Name != null)
            {

                if (r_GameBoard.GameBoard[row, col].Sign == oppentSign)
                {
                    opponentPostion.Add(new Cell(row, col, m_CurrentSign, m_CurrentPlayerMakeMove));
                    row += i_Horizontal;
                    col += i_Vertical;
                }
                else
                {
                    return opponentPostion;
                }
            }
            return new List<Cell>();
        }

    }
}
