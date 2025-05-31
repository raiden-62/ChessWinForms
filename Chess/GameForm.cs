using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Model;

namespace Chess
{
    public partial class GameForm : Form
    {
        private Button[,] _cells;
        private const int cellSize = 45;
        private Game _game;

        private int[] chosenCell = new int[2] { -1, -1 }; //row, col

        public GameForm(bool newGame = true)
        {
            _cells = new Button[8, 8];
            InitializeComponent();
            InitializeBoard();

            if (newGame)
            {
                _game = new Game();
            }
            else
            {
                //fill from serialized game
            }
            SyncBoard();
        }


        private void InitializeBoard()
        {
            GamePanel.Controls.Clear();
            GamePanel.Width = cellSize * 8;
            GamePanel.Height = cellSize * 8;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button btn = new Button();
                    btn.Width = cellSize;
                    btn.Height = cellSize;
                    btn.Left = col * cellSize;
                    btn.Top = row * cellSize;

                    btn.BackColor = ((col + row) % 2 == 0) ? Color.Beige : Color.Brown;

                    btn.Tag = new Point(row, col); //for the click handler
                    btn.Click += CellClick;

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;

                    GamePanel.Controls.Add(btn); //adds to the panel
                    _cells[row, col] = btn;



                }
            }
        }

        public Image GetPieceSprite(ChessPiece piece)
        {
            string pieceFolder = "PieceSprites";
            string fileName = Game.GetPieceName(piece) + ".png";
            return Image.FromFile(Path.Combine(pieceFolder, fileName));
        }
        private void SyncBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (_game.Board[row, col] != null)
                    {
                        _cells[row, col].BackgroundImage = GetPieceSprite(_game.Board[row, col]);
                    }
                }
            }
        }

        private void DrawPossibleMoves(IFutureMove m, bool draw = true)
        {
            if (draw) //Draw green possible cells
            {
                if (m.Moves == null) return;
                foreach (var move in m.Moves)
                {
                    _cells[move.y, move.x].BackColor = Color.Green;
                }
            }
            else //Restore normal colors
            {
                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        _cells[row, col].BackColor = ((col + row) % 2 == 0) ? Color.Beige : Color.Brown;
                    }
                }
            }
        }
        private void FillBoardDefault()
        {
            string[,] defaultLayout = new string[8, 8]
            {
                {"bR", "bN", "bB", "bQ", "bK", "bB", "bN", "bR"},
                {"bP", "bP", "bP", "bP", "bP", "bP", "bP", "bP"},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", ""},
                {"wP", "wP", "wP", "wP", "wP", "wP", "wP", "wP"},
                {"wR", "wN", "wB", "wQ", "wK", "wB", "wN", "wR"}
            };



            string pieceFolder = "PieceSprites";
            string extension = "png";

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (defaultLayout[row, col] == String.Empty) continue;

                    string fileName = Path.Combine(pieceFolder, defaultLayout[row, col] + "." + extension);
                    _cells[row, col].BackgroundImage = Image.FromFile(fileName);
                }
            }
        }

        private void CellClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var point = (Point)btn.Tag;
            int x = point.X; //row
            int y = point.Y; //col

            _game.Move(y, x);

            if (_game.Board[y, x] != null) DrawPossibleMoves(_game.Board[y, x] as IFutureMove);
            SyncBoard();
        }
    }
}
