using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Rook : ChessPiece
    {
        public Rook(int x, int y, int color, bool firstmove = true) : base(x, y, color, firstmove) { }
        public override void MoveGenerator<T>(T[,] board)
        {
            base.MoveGenerator(board);
            bool closemove = false; // напрваление не закрыто
            for (int x = XCoordinate+1; x< 8; x++)//ходы по напрвление
            {
                if (closemove) break;// выход если напрваление закрыто
                if (board[x, YCoordinate] == null) Add(x, YCoordinate);// возможный ход на пустую клетку
                else
                {
                    closemove = true;// закрытие направления
                    if (board[x, YCoordinate].Color != Color) Add(x, YCoordinate); // возможность забарть фигуру
                }
            }
            closemove = false;
            for (int x = XCoordinate-1; x >-1; x--)
            {
                if (closemove) break;
                if (board[x, YCoordinate] == null) Add(x, YCoordinate);
                else
                {
                    closemove = true;
                    if (board[x, YCoordinate].Color != Color) Add(x, YCoordinate);
                }
            }
            closemove = false;
            for (int y = YCoordinate+1; y < 8; y++)
            {
                if (closemove) break;
                if (board[XCoordinate,y] == null) Add(XCoordinate, y);
                else
                {
                    closemove = true;
                    if (board[XCoordinate, y].Color != Color) Add(XCoordinate,y);
                }
            }
            closemove = false;
            for (int y = YCoordinate-1; y >-1; y--)
            {
                if (closemove) break;
                if (board[XCoordinate, y] == null) Add(XCoordinate, y);
                else
                {
                    closemove = true;
                    if (board[XCoordinate, y].Color != Color) Add(XCoordinate, y);
                }
            }
        }
        public override string ToString()
        {
            return (Color == 1 ? "wR" : "bR");
        }
    }
}
