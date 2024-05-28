using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class GameState : BaseNotification
    {
        private ObservableCollection<ObservableCollection<Cell>> board;
        private PlayerTurn turn;
        private MultipleJump multipleJump;
        private int collectedBlackPieces;
        private int collectedWhitePieces;

        public GameState(ObservableCollection<ObservableCollection<Cell>> board, PlayerTurn turn, MultipleJump multipleJump, int collectedBlackPieces, int collectedWhitePieces)
        {
            this.board = board;
            this.turn = turn;
            this.multipleJump = multipleJump;
            this.collectedBlackPieces = collectedBlackPieces;
            this.collectedWhitePieces = collectedWhitePieces;
        }
        public GameState() { }

        public ObservableCollection<ObservableCollection<Cell>> Board 
        { 
            get
            { 
                return board; 
            }
            set
            {
                board = value;
                NotifyPropertyChanged();
            }
        }

        public PlayerTurn Turn 
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

        public MultipleJump MultipleJump 
        {
            get
            {
                return multipleJump;
            }
            set
            {
                multipleJump = value;
                NotifyPropertyChanged();
            }
        }

        public int CollectedBlackPieces
        {
            get
            {
                return collectedBlackPieces;
            }
            set
            {
                collectedBlackPieces = value;
                NotifyPropertyChanged();
            }
        }

        public int CollectedWhitePieces
        {
            get
            {
                return collectedWhitePieces;
            }
            set
            {
                collectedWhitePieces = value;
                NotifyPropertyChanged();
            }
        }
    }
}
