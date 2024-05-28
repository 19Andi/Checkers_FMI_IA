using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class PlayerTurn : BaseNotification
    {
        private PieceColor turn;

        public PlayerTurn(PieceColor turn = PieceColor.White)
        {
            this.turn = turn;
        }

        public PieceColor Turn
        {
            get
            {
                return turn;
            }
            set
            {
                turn = value;
                NotifyPropertyChanged();
            }
        }
    }
}
