using Checkers.Commands;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    public class CellVM : BaseNotification
    {
        private GameLogic gl;
        private Cell normalCell;
        private ICommand clickPieceCommand;
        private ICommand movePieceCommand;

        public CellVM(Cell normalCell, GameLogic gl)
        {
            this.normalCell = normalCell;
            this.gl = gl;
        }
        public Cell NormalCell
        {
            get
            {
                return normalCell;
            }
            set
            {
                normalCell = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ClickPieceCommand
        {
            get
            {
                if (clickPieceCommand == null)
                {
                    clickPieceCommand = new RelayCommand<Cell>(gl.ClickPiece);
                }
                return clickPieceCommand;
            }
        }

        public ICommand MovePieceCommand
        {
            get
            {
                if (movePieceCommand == null)
                {
                    movePieceCommand = new RelayCommand<Cell>(gl.MovePiece);
                }
                return movePieceCommand;
            }
        }
    }
}
