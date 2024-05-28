using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace Checkers.Services
{
    public class GameLogic : BaseNotification
    {
        private ObservableCollection<ObservableCollection<Cell>> board = new ObservableCollection<ObservableCollection<Cell>>();
        private PlayerTurn turn = new PlayerTurn();
        private int wWins = 0;
        private int bWins = 0;

        private MultipleJump multipleJump = new MultipleJump();
        private static bool extraMove = false;
        private static bool extraPath = false;
        private static int collectedBlackPieces = 0;
        private static int collectedWhitePieces = 0;
        public static Cell CurrentCell { get; set; }
        private static Dictionary<Cell, Cell> currentNeighbours = new Dictionary<Cell, Cell>();

        public GameLogic(ObservableCollection<ObservableCollection<Cell>> board, MultipleJump multipleJump, PlayerTurn turn)
        {
            this.board = board;
            this.turn = turn;
            this.multipleJump = multipleJump;
        }

        private void SwitchTurns()
        {
            if (turn.Turn == PieceColor.White)
            {
                turn.Turn = PieceColor.Black;
            }
            else
            {
                turn.Turn = PieceColor.White;
            }
        }
        private void FindNeighbours(Cell cell)
        {
            var checkNeighbours = new List<Tuple<int, int>>();

            setNeighbours(cell, checkNeighbours);

            foreach (Tuple<int, int> neighbour in checkNeighbours)
            {
                if (onTable(cell.Row + neighbour.Item1, cell.Column + neighbour.Item2))
                {
                    if (board[cell.Row + neighbour.Item1][cell.Column + neighbour.Item2].Piece == null)
                    {
                        if (!ExtraMove)
                        {
                            currentNeighbours.Add(board[cell.Row + neighbour.Item1][cell.Column + neighbour.Item2], null);
                        }
                    }
                    else if (onTable(cell.Row + neighbour.Item1 * 2, cell.Column + neighbour.Item2 * 2) &&
                        board[cell.Row + neighbour.Item1][cell.Column + neighbour.Item2].Piece.Color != cell.Piece.Color &&
                        board[cell.Row + neighbour.Item1 * 2][cell.Column + neighbour.Item2 * 2].Piece == null)
                    {
                        currentNeighbours.Add(board[cell.Row + neighbour.Item1 * 2][cell.Column + neighbour.Item2 * 2], board[cell.Row + neighbour.Item1][cell.Column + neighbour.Item2]);
                        ExtraPath = true;
                    }
                }
            }
        }
        private void DisplayMoves(Cell cell)
        {
            if (CurrentCell != cell)
            {
                if (CurrentCell != null)
                {
                    board[CurrentCell.Row][CurrentCell.Column].Texture = "/Checkers;component/Resources/BlackCell.png";

                    foreach (Cell selectedSquare in currentNeighbours.Keys)
                    {
                        selectedSquare.HintSymbol = null;
                    }
                    currentNeighbours.Clear();
                }

                FindNeighbours(cell);

                if(extraMove && !multipleJump.ButtonChecked)
                {
                    currentNeighbours.Clear();
                    ExtraMove = false;
                    SwitchTurns();
                }
                else
                {
                    if (ExtraMove && !ExtraPath)
                    {
                        ExtraMove = false;
                        SwitchTurns();
                    }
                    else
                    {

                        foreach (Cell neighbour in currentNeighbours.Keys)
                        {
                            neighbour.HintSymbol = "/Checkers;component/Resources/HintCell.png";
                        }

                        CurrentCell = cell;
                        ExtraPath = false;
                    }
                }
            }
            else
            {
                board[cell.Row][cell.Column].Texture = "/Checkers;component/Resources/BlackCell.png";

                foreach (Cell selectedSquare in currentNeighbours.Keys)
                {
                    selectedSquare.HintSymbol = null;
                }
                currentNeighbours.Clear();
                CurrentCell = null;
            }
        }
        public void ClickPiece(Cell cell)
        {
            if ((turn.Turn == PieceColor.White && cell.Piece.Color == PieceColor.White ||
                turn.Turn == PieceColor.Black && cell.Piece.Color == PieceColor.Black) &&
                !ExtraMove)
            {
                cell.Texture = "/Checkers;component/Resources/HintCell.png";
                DisplayMoves(cell);
            }
        }
        public void MovePiece(Cell cell)
        {
            cell.Piece = CurrentCell.Piece;
            cell.Piece.Cell = cell;

            if (currentNeighbours[cell] != null)
            {
                currentNeighbours[cell].Piece = null;
                ExtraMove = true;
            }
            else
            {
                ExtraMove = false;
                SwitchTurns();
            }

            board[CurrentCell.Row][CurrentCell.Column].Texture = "/Checkers;component/Resources/BlackCell.png";

            foreach (Cell selectedCell in currentNeighbours.Keys)
            {
                selectedCell.HintSymbol = null;
            }
            currentNeighbours.Clear();
            CurrentCell.Piece = null;
            CurrentCell = null;

            if (cell.Piece.Type == PieceType.Standard)
            {
                if (cell.Row == board.Count - 1 && cell.Piece.Color == PieceColor.Black) 
                {
                    cell.Piece.Type = PieceType.King;
                    cell.Piece.Texture = "/Checkers;component/Resources/blackKing.png";
                }
                else if (cell.Row == 0 && cell.Piece.Color == PieceColor.White)
                {
                    cell.Piece.Type = PieceType.King;
                    cell.Piece.Texture = "/Checkers;component/Resources/whiteKing.png";
                }
            }

            if (ExtraMove)
            {
                if (turn.Turn == PieceColor.Black)
                {
                    CollectedWhitePieces++;
                }
                if (turn.Turn == PieceColor.White)
                {
                    CollectedBlackPieces++;
                }
                DisplayMoves(cell);
            }

            if (CollectedBlackPieces == 12 || CollectedWhitePieces == 12)
            {
                GameOver();
            }
        }

        public static bool ExtraMove
        {
            get
            {
                return extraMove;
            }
            set
            {
                extraMove = value;
            }
        }
        public static bool ExtraPath
        {
            get
            {
                return extraPath;
            }
            set
            {
                extraPath = value;
            }
        }
        public static int CollectedWhitePieces
        {
            get { return collectedWhitePieces; }
            set { collectedWhitePieces = value; }
        }
        public static int CollectedBlackPieces
        {
            get { return collectedBlackPieces; }
            set { collectedBlackPieces = value; }
        }
        public PlayerTurn Turn
        {
            set
            {
                Turn = value;
                NotifyPropertyChanged();
            }
        }
        public MultipleJump MultipleJump
        {
            set
            {
                multipleJump = value;
                NotifyPropertyChanged();
            }
        }

        public void GameOver()
        {
            readScore(ref bWins, ref wWins);
            if (CollectedBlackPieces == 12)
            {
                writeScore(bWins, ++wWins);
                MessageBox.Show("Game over! White player won!", "Checkers");
            }
            if (CollectedWhitePieces == 12)
            {
                writeScore(++bWins, wWins);
                MessageBox.Show("Game over! Black player won!", "Checkers");
            }
            CollectedBlackPieces = 0;
            CollectedWhitePieces = 0;
            NewGameBoard();
        }
        public static void setNeighbours(Cell cell, List<Tuple<int, int>> neighboursToCheck)
        {
            if (cell.Piece.Type == PieceType.King)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
            else if (cell.Piece.Color == PieceColor.White)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
            }
            else
            {
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
        }
        public static bool onTable(int row, int column)
        {
            return row >= 0 && column >= 0 && row < 8 && column < 8;
        }
        public void NewGameBoard()
        {
            for (int index1 = 0; index1 < 8; index1++)
            {
                for (int index2 = 0; index2 < 8; index2++)
                {
                    if ((index1 + index2) % 2 == 0)
                    {
                        board[index1][index2].Piece = null;
                    }
                    else
                        if (index1 < 3)
                    {
                        board[index1][index2].Piece = new Piece(PieceColor.Black);
                        board[index1][index2].Piece.Cell = board[index1][index2];
                        //pieces
                    }
                    else
                        if (index1 > 4)
                    {
                        board[index1][index2].Piece = new Piece(PieceColor.White);
                        board[index1][index2].Piece.Cell = board[index1][index2];
                        //pieces
                    }
                    else
                    {
                        board[index1][index2].Piece = null;
                    }
                }
            }

            foreach (var cell in currentNeighbours.Keys)
            {
                cell.HintSymbol = null;
            }

            if (CurrentCell != null)
            {
                CurrentCell.Texture = "/Checkers;component/Resources/BlackCell.png";
            }

            currentNeighbours.Clear();
            CurrentCell = null;
            ExtraMove = false;
            ExtraPath = false;
            CollectedWhitePieces = 0;
            CollectedBlackPieces = 0;
            turn.Turn = PieceColor.White;
        }
        public void SaveGameBoard()
        {
            GameState savedGame = new GameState(board, turn, multipleJump, collectedBlackPieces, collectedWhitePieces);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (.json)|.json|All files (.)|.";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                string jsonData = JsonSerializer.Serialize(savedGame, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);
            }

        }
        public void OpenGameBoard()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (.json)|.json|All files (.)|.";
            GameState savedGame = new GameState();

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string jsonData = File.ReadAllText(filePath);
                savedGame = JsonSerializer.Deserialize<GameState>(jsonData);
            }
            for (int index1 = 0; index1 < 8; index1++)
                for (int index2 = 0; index2 < 8; index2++)
                    if (savedGame.Board[index1][index2].Piece != null)
                        board[index1][index2].Piece = new Piece(savedGame.Board[index1][index2].Piece.Color, savedGame.Board[index1][index2].Piece.Type);
                    else
                        board[index1][index2].Piece = null;
            multipleJump.ButtonChecked = savedGame.MultipleJump.ButtonChecked;
            turn.Turn = savedGame.Turn.Turn;
            collectedWhitePieces = savedGame.CollectedWhitePieces;
            collectedBlackPieces = savedGame.CollectedBlackPieces;

        }
        public static void writeScore(int b, int w)
        {
            string PATH = "C:/Faculte/SEM_4/MVP/Checkers/Resources/winners.txt";
            using (var writer = new StreamWriter(PATH))
            {
                writer.WriteLine(b + "," + w);
            }
        }
        public static void readScore(ref int b, ref int w)
        {
            string PATH = "C:/Faculte/SEM_4/MVP/Checkers/Resources/winners.txt";
            using (var reader = new StreamReader(PATH))
            {
                string delimiter = ",";
                string line = reader.ReadLine();
                var splitted = line.Split(delimiter[0]);
                b = int.Parse(splitted[0]);
                w = int.Parse(splitted[1]);
            }
        }
        public void printScore()
        {
            readScore(ref bWins,ref wWins);
            MessageBox.Show($"White player won {wWins} time(s). \nBlack player won {bWins} time(s).", "Game statistics");
        }

    }
}
