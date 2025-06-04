using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Pawn :ChessPiece
    {
        public Pawn(int x, int y, int color, bool firstmove = true) : base(x, y, color,firstmove) { }

        public override void MoveGenerator<T>(T[,] board)
        {
            base.MoveGenerator(board);

            if (board[XCoordinate+Color, YCoordinate] == null)
            {
                Add(XCoordinate+Color, YCoordinate);
                if (FirstMove && board[XCoordinate+Color*2, YCoordinate] == null)
                    Add(XCoordinate+Color*2, YCoordinate);
            }
            if (YCoordinate != 7 && board[XCoordinate+Color, YCoordinate+1] != null && board[XCoordinate + Color, YCoordinate + 1].Color != Color)
                Add(XCoordinate + Color, YCoordinate +1);
            if (YCoordinate != 0 && board[XCoordinate + Color, YCoordinate - 1] != null && board[XCoordinate + Color, YCoordinate - 1].Color != Color)
                Add(XCoordinate +Color, YCoordinate - 1);
        }
        public override string ToString()
        {
            return (Color == 1 ? "wP" : "bP");
        }

    }
}
