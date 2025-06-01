using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Bishop : ChessPiece
    {
        public Bishop(int x, int y, int color) : base(x, y, color) { }
        private bool PartMoveGenerator(int x,int y, bool closemove, ChessPiece[,] board)
        {
            if (board[x, y] == null) Add(x, y);
            else
            {
                closemove = true;
                if (board[x, y].Color != Color) Add(x, y);
            }
            return closemove;
        }
        public override void MoveGenerator(ChessPiece[,] board)
        {
            base.MoveGenerator(board);
            bool closemove = false;
            int x = XCoordinate+1, y = YCoordinate+1;
            while (x<8 & y<8)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove,board);
                x++;y++;
            }
            x = XCoordinate + 1;y = YCoordinate -1;
            closemove = false;
            while (x < 8 & y > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove, board);
                x++; y--;
            }
            x = XCoordinate - 1; y = YCoordinate + 1;
            closemove = false;
            while (y < 8 & x > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove, board);
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
        }
    }
}
