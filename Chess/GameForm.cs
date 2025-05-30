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

namespace Chess
{
    public partial class GameForm : Form
    {
        private Button[,] _cells;
        private const int cellSize = 45;
        public GameForm(bool newGame = true)
        {
            _cells = new Button[8, 8];
            InitializeComponent();
            InitializeBoard();

            if (newGame)
            {
                FillBoardDefault();
            }
            else
            {
                //fill from serialized game
            }
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
                {"wR", "wN", "wB", "wQ", "wK", "wB", "wN", "wR"},
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

        }
    }
}
