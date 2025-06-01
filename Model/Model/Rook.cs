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
        public override void MoveGenerator(ChessPiece[,] board)
        {
            base.MoveGenerator(board);
            bool closemove = false;
            for (int x = XCoordinate+1; x< 8; x++)
            {
                if (closemove) break;
                System.Diagnostics.Debug.WriteLine(x);
                System.Diagnostics.Debug.WriteLine(YCoordinate);
                if (board[x, YCoordinate] == null) Add(x, YCoordinate);
                else
                {
                    closemove = true;
                    if (board[x, YCoordinate].Color != Color) Add(x, YCoordinate);
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
