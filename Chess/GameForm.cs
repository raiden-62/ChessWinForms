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
        private Serializer _serializer;

        public GameForm( string path, bool isJSON, bool newGame = true)
        {
            InitializeComponent();
            InitializeBoard();

            if (!newGame) path = Path.GetDirectoryName(path); //Assumes we're always working with the same ChessGame filename

            if (isJSON) _serializer = new SerializerJSON(path);
            else _serializer = new SerializerXML(path);

            if (newGame)  _game = new Game();
            else  _game = _serializer.Deserialize();
            

            SyncBoard();
            this.FormClosing += ClosingGame;
        }


        private void InitializeBoard()
        {
            _cells = new Button[8, 8];

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
            string fileName = piece.ToString() + ".png";
            return Image.FromFile(Path.Combine(pieceFolder, fileName));
        }
        private void SyncBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    int flippedRow = 7 - row;
                    if (_game.Board[flippedRow, col] != null)
                    {
                        _cells[row, col].BackgroundImage = GetPieceSprite(_game.Board[flippedRow, col]);
                    }
                    else
                    {
                        _cells[row, col].BackgroundImage = null;
                    }
                }
            }
        }

        private void DrawPossibleMoves(IFutureMove m, bool draw = true)
        {
            if (draw) //Draw green possible cells
            {
                if (m == null || m.Moves == null) return;
                foreach (var move in m.Moves)
                {
                    _cells[7 - move.x, move.y].BackColor = Color.Green; //fipped row
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

        private void CellClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var point = (Point)btn.Tag;
            int x = point.X; //row
            int y = point.Y; //col
            int flippedRow = 7 - x;

            int gameState = _game.Move(flippedRow, y);

            if (_game.Board[flippedRow, y] != null && _game.X1 != -1) DrawPossibleMoves(_game.Board[flippedRow, y] as IFutureMove); //draw possible moves
            else if (_game.X1 == -1) DrawPossibleMoves(null, false); //erase possible moves 

            SyncBoard();

            if (gameState != 0)
            {
                string message = "";
                switch (gameState)
                {
                    case -1:
                        message = "Победа черных."; break;
                    case 1:
                        message = "Победа белых."; break;
                    case 2:
                        message = "Ничья."; break;
                    case 3:
                        if (_game.ColorPlayer == 1) message = "Шах белому королю!";
                        else message = "Шах черному королю!";
                        break;
                }
                MessageBox.Show(message);
            }
            if (gameState == 1 || gameState == -1)
            {
                this.Close();
            }
        }

        private void ClosingGame(object sender, EventArgs e)
        {
            _serializer.Serialize(_game);
        }


    }
}
