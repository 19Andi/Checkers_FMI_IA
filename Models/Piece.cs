using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public enum PieceColor
    {
        Black,
        White
    }
    public enum PieceType
    {
        Standard,
        King
    }
    public class Piece : BaseNotification
    {
        private PieceColor color;
        private PieceType type;
        private string texture;
        private Cell cell;

        public Piece() { }
        public Piece(PieceColor color)
        {
            this.color = color;
            this.type = PieceType.Standard;
            if (color == PieceColor.Black)
            {
                texture = "/Checkers;component/Resources/blackPiece.png";
            }
            else
            {
                texture = "/Checkers;component/Resources/whitePiece.png";
            }
        }

        public Piece(PieceColor Color, PieceType Type)
        {
            this.Color = Color;
            this.Type = Type;
            if (Color == PieceColor.Black)
            {
                texture = "/Checkers;component/Resources/blackPiece.png";
            }
            else
            {
                texture = "/Checkers;component/Resources/whitePiece.png";
            }
            if (Type == PieceType.King && Color == PieceColor.Black)
            {
                texture = "/Checkers;component/Resources/blackKing.png";
            }
            if (Type == PieceType.King && Color == PieceColor.White)
            {
                texture = "/Checkers;component/Resources/whiteKing.png";
            }
        }

        public PieceColor Color
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

        public PieceType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
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
        [JsonIgnore]
        public Cell Cell
        {
            get
            {
                return cell;
            }
            set
            {
                cell = value;
                NotifyPropertyChanged();
            }
        }
    }
}
