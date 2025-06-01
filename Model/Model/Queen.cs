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
            int x = XCoordinate + 1, y = YCoordinate + 1;
            while (x < 8 & y < 8)
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
            while (x < 8 & y > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove,board);
                x--; y++;
            }
            x = XCoordinate - 1; y = YCoordinate - 1;
            closemove = false;
            while (x < 8 & y > -1)
            {
                if (closemove) break;
                closemove = PartMoveGenerator(x, y, closemove, board);
                x--; y--;
            }
            closemove = false;
            for (x = XCoordinate; x < 8; x++)
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
            for ( x = XCoordinate; x > -1; x--)
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
            for ( y = YCoordinate; y < 8; y++)
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
            for ( y = YCoordinate; y > -1; y--)
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
