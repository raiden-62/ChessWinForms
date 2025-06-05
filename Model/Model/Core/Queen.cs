using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Queen : ChessPiece
    {
        public Queen(int x, int y, int color) : base(x, y, color) { }
        private bool PartMoveGenerator(int x, int y, bool closemove, ChessPiece[,] board)
        {
            if (board[x, y] == null) Add(x, y);//возможный ход на пустую клетку
            else// стоит фигура
            {
                closemove = true;//закрывает хода по направлению дальше
                if (board[x, y].Color != Color) Add(x, y);// можно ли фигуру забрать
            }
            return closemove;
        }
        public override void MoveGenerator<T>(T[,] board)
        {
            base.MoveGenerator(board);
            bool closemove = false; // напрвление не закрыто
            int x = XCoordinate + 1, y = YCoordinate + 1; // первый возможный ход по напрвлению 
            while (x < 8 & y < 8) // ограничение на край доски
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove,board);
                x++; y++;
            }
            x = XCoordinate + 1; y = YCoordinate - 1;
            closemove = false;
            while (x < 8 & y > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove,board);
                x++; y--;
            }
            x = XCoordinate - 1; y = YCoordinate + 1;
            closemove = false;
            while (y < 8 & x > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove,board);
                x--; y++;
            }
            x = XCoordinate - 1; y = YCoordinate - 1;
            closemove = false;
            while (x > -1 & y > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove, board);
                x--; y--;
            }
            closemove = false;// напрваление не закрыто
            for (x = XCoordinate + 1; x < 8; x++)//ходы по напрвление
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
            for (x = XCoordinate - 1; x > -1; x--)
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
            for ( y = YCoordinate + 1; y < 8; y++)
            {
                if (closemove) break;
                if (board[XCoordinate, y] == null) Add(XCoordinate, y);
                else
                {
                    closemove = true;
                    if (board[XCoordinate, y].Color != Color) Add(XCoordinate, y);
                }
            }
            closemove = false;
            for ( y = YCoordinate - 1; y > -1; y--)
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
            return (Color == 1 ? "wQ" : "bQ");
        }
    }
}
