using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Knight : ChessPiece
    {
        public Knight(int x, int y, int color) : base(x, y, color) { }
        public override void MoveGenerator(ChessPiece[,] board)
        {
            base.MoveGenerator(board);
            int x = XCoordinate + 3, y = YCoordinate + 1;
            if (-1<x && x<8 && -1 < y && y < 8 && board[x,y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate + 3;y = YCoordinate - 1;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate -3; y = YCoordinate - 1;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate - 3; y = YCoordinate + 1;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate + 1; y = YCoordinate + 3;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate + 1; y = YCoordinate - 3;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate - 1; y = YCoordinate - 3;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
            x = XCoordinate - 1; y = YCoordinate +3;
            if (-1 < x && x < 8 && -1 < y && y < 8 && board[x, y] != null && board[x, y].Color != Color) Add(x, y);
           
        }
        public override string ToString()
        {
            return (Color == 1 ? "wN" : "bN");
        }
    }
}
