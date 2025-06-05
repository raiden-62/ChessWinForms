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
            InitializeComponent(); //Определен в GameForm.Designer.cs, инициализирует все кнопки из конструктора
            InitializeBoard();

            if (!newGame) path = Path.GetDirectoryName(path); //Классы сериализации предполагают работу только с папкой

            if (isJSON) _serializer = new SerializerJSON(path);
            else _serializer = new SerializerXML(path);

            if (newGame)  _game = new Game();
            else  _game = _serializer.Deserialize();
            

            SyncBoard();
            this.FormClosing += ClosingGame; //Сохраняет игру и выходит из приложения при нажатии на крестик справа сверху
        }


        private void InitializeBoard()
        {
            _cells = new Button[8, 8]; //Массив кнопок, т.е. клеток

            
            //Размер панели
            GamePanel.Width = cellSize * 8; 
            GamePanel.Height = cellSize * 8; 

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button btn = new Button();
                    //Размер кнопки
                    btn.Width = cellSize;
                    btn.Height = cellSize;
                    //Позиция кнопки (по левому углу)
                    btn.Left = col * cellSize;
                    btn.Top = row * cellSize;

                    //Цвет
                    btn.BackColor = ((col + row) % 2 == 0) ? Color.Beige : Color.Brown;


                    btn.Tag = new Point(row, col); //Tag может хранить
                    btn.Click += CellClick;

                    btn.FlatStyle = FlatStyle.Flat; //Убирает 3D края у кнопки
                    btn.FlatAppearance.BorderSize = 0; //Убирает границы

                    GamePanel.Controls.Add(btn); //Добавляем кнопку на панель
                    _cells[row, col] = btn; //Сохраняем в массив
                }
            }
        }

        public Image GetPieceSprite(ChessPiece piece)
        {
            string pieceFolder = "PieceSprites"; //Название папки со спрайтами
            string fileName = piece.ToString() + ".png"; //piece.ToString() выдает например wB(white Bishop), также называются файлы со спрайтами
            return Image.FromFile(Path.Combine(pieceFolder, fileName)); //Image.FromFile() извлекает картинку
        }
        private void SyncBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    int flippedRow = 7 - row; //Перевернутая доска (черные сверху, а не снизу)
                    if (_game.Board[flippedRow, col] != null)
                    {
                        _cells[row, col].BackgroundImage = GetPieceSprite(_game.Board[flippedRow, col]); //Если клетка не пустая синхронизируем её спрайт
                    }
                    else
                    {
                        _cells[row, col].BackgroundImage = null; //Если пустая убираем картинку
                    }
                }
            }
        }

        private void DrawPossibleMoves(IFutureMove m, bool draw = true)
        {
            if (draw) //Отрисовываем возможные ходы
            {
                if (m == null || m.Moves == null) return;
                foreach (var move in m.Moves)
                {
                    _cells[7 - move.x, move.y].BackColor = Color.Green; //7-move.x = певеренутая доска
                }
            }
            else //Возвращаем нормальные цвета (черный, белый) всем клеткам
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
            var btn = sender as Button; //Приводим объект-отправитель к кнопке (т.к. это и есть кнопка)
            var point = (Point)btn.Tag; //Забираем свойство Tag
            int x = point.X; //row  Строка
            int y = point.Y; //col  Столбец
            int flippedRow = 7 - x; //Переворачиваем строку

            _game.Move(flippedRow, y); //Производим ход

            if (_game.Board[flippedRow, y] != null && _game.X1 != -1) DrawPossibleMoves(_game.Board[flippedRow, y] as IFutureMove); //Отрисовываем возможные ходы
            else if (_game.X1 == -1) DrawPossibleMoves(null, false); //Возвращаем обычный цвет клеткам

            SyncBoard(); //Синхронизация спрайтов с реализацией

            if (_game != 0)
            {
                string message = "";
                if (_game == -1)
                {
                    message = "Победа черных.";
                }
                else if (_game == 1)
                {
                    message = "Победа белых.";
                }
                else if (_game == 2)
                {
                    message = "Ничья.";
                }
                else if (_game == 3)
                {
                    if (_game.ColorPlayer == 1)
                    {
                        message = "Шах белому королю!";
                    }
                    else
                    {
                        message = "Шах черному королю!";
                    }
                }
                MessageBox.Show(message); //Всплывающее окно
            }
            if (_game == 1 || _game == -1)
            {
                this.Close(); //Игра окончена -> закрыть окно
            }
        }

        private void ClosingGame(object sender, EventArgs e)
        {
            _serializer.Serialize(_game); //Сохранение игры
            Application.Exit(); //Полностью закрывает приложение
        }


    }
}
