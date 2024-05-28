using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    public class PlayerTurnVM : BaseNotification
    {
        private PlayerTurn playerTurn;

        public PlayerTurnVM(PlayerTurn playerTurn)
        {
            this.playerTurn = playerTurn;
        }

        public PlayerTurn PlayerTurn 
        {
            get 
            { 
                return playerTurn; 
            }
            set
            {
                this.playerTurn = value;
                NotifyPropertyChanged();
            }
        }
    }
}
