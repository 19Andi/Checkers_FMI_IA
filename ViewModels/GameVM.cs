using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    public class GameVM
    {
        public ObservableCollection<ObservableCollection<CellVM>> Board { get; set; }
        public GameLogic Logic { get; set; }
        public ButtonControl Controller { get; set; }
        public MultipleJumpVM JumpVM { get; set; }
        public PlayerTurnVM PlayerTurnVM { get; set; }
        public GameVM() 
        {
            ObservableCollection<ObservableCollection<Cell>> board = createBoard();
            MultipleJump jump = new MultipleJump();
            PlayerTurn turn = new PlayerTurn() ;
            Logic = new GameLogic(board, jump, turn);
            Board = CellBoardToCellVMBoard(board);
            Controller = new ButtonControl(Logic);
            JumpVM = new MultipleJumpVM(jump);
            PlayerTurnVM = new PlayerTurnVM(turn);
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c, Logic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
        public ObservableCollection<ObservableCollection<Cell>> createBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
            const int boardSize = 8;

            for (int row = 0; row < boardSize; ++row)
            {
                board.Add(new ObservableCollection<Cell>());
                for (int column = 0; column < boardSize; ++column)
                {
                    if ((row + column) % 2 == 0)
                    {
                        board[row].Add(new Cell(row, column, CellColor.White, null));
                    }
                    else if (row > 4)
                    {
                        board[row].Add(new Cell(row, column, CellColor.Black, new Piece(PieceColor.White)));
                    }
                    else if (row < 3)
                    {
                        board[row].Add(new Cell(row, column, CellColor.Black, new Piece(PieceColor.Black)));
                    }
                    else
                    {
                        board[row].Add(new Cell(row, column, CellColor.Black, null));
                    }
                }
            }

            return board;
        }
    }
}
