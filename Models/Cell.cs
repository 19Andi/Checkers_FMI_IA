using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Checkers.Models
{
    public enum CellColor
    {
        Black,
        White
    }
    public class Cell : BaseNotification
    {
        private int row;
        private int column;
        private CellColor color;
        private string texture;
        private Piece piece;
        private string hintSymbol;

        public Cell(int Row, int Column, CellColor Color, Piece Piece)
        {
            this.Row = Row;
            this.Column = Column;
            this.Color = Color;
            this.Piece = Piece;
            if (Color == CellColor.Black)
            {
                texture = "/Checkers;component/Resources/BlackCell.png";
            }
            else
            {
                texture = "/Checkers;component/Resources/WhiteCell.png";
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
                NotifyPropertyChanged();
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
                NotifyPropertyChanged();
            }
        }

        public CellColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore]
        public string Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                NotifyPropertyChanged();
            }
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore]
        public string HintSymbol
        {
            get
            {
                return hintSymbol;
            }
            set
            {
                hintSymbol = value;
                NotifyPropertyChanged();
            }
        }
    }
}
