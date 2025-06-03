using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class King : ChessPiece
    {
        public King(int x, int y, int color, bool firstmove = true) : base(x, y, color, firstmove) { }
        public override void MoveGenerator(ChessPiece[,] board)
        {
            base.MoveGenerator(board);
            for (int x = XCoordinate-1; x <=XCoordinate+1;x++)
            {
                for (int y = YCoordinate-1; y <= YCoordinate + 1; y++)
                    if (0 <= x && x <= 7 && 0 <= y && y <= 7 && board[x, y] == null && !IsBrokenField(board, x, y)) Add(x, y);
            }
            if (FirstMove)
            {
                if (board[XCoordinate, YCoordinate - 4] != null && board[XCoordinate, YCoordinate - 4].FirstMove && !IsBrokenField(board, XCoordinate, YCoordinate) &&
                    !IsBrokenField(board, XCoordinate, YCoordinate - 1) && board[XCoordinate, YCoordinate - 1] == null && !IsBrokenField(board, XCoordinate, YCoordinate - 2) &&
                    board[XCoordinate, YCoordinate - 2] == null && !IsBrokenField(board, XCoordinate, YCoordinate - 3) && board[XCoordinate, YCoordinate - 3] == null)
                    Add(XCoordinate, YCoordinate - 2);
                if (board[XCoordinate, YCoordinate + 3] != null && board[XCoordinate, YCoordinate + 3].FirstMove && !IsBrokenField(board, XCoordinate, YCoordinate) &&
                    !IsBrokenField(board, XCoordinate, YCoordinate + 1) && board[ XCoordinate, YCoordinate + 1] == null && !IsBrokenField(board, XCoordinate, YCoordinate + 2) &&
                    board[ XCoordinate, YCoordinate + 2] == null)
                    Add(XCoordinate, YCoordinate +2);
            }
        }
        public override void Move(int x, int y, ChessPiece[,] board)
        {
            if (Moves.Contains((x, y)))
            {
                if(YCoordinate-y == -2)
                {
                    board[x, 7].MoveGenerator(board);
                    board[x, 7].Move(x, y - 1,board);
                    base.Move(x, y, board);
                }
                else if (YCoordinate-y == 2)
                {
                    board[x, 0].MoveGenerator(board);
                    board[x, 0].Move(x, y + 1, board);
                    base.Move(x, y, board);
                }
                else base.Move(x, y, board);
            }
        }
        public bool IsBrokenField(ChessPiece[,] board,int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j<8; j++)
                {
                    if (board[i,j]!= null && board[i,j].Color != Color)
                    {
                        // Проверка нет ли рядом короля
                        if (board[i, j] is King)
                        {
                            if (Math.Abs(i - x) <= 1 && Math.Abs(j - y) <= 1) return true;
                            else continue;
                        }

                        board[i, j].MoveGenerator(board);
                        if (board[i, j].Moves.Contains((x, y)))
                            return true;
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return (Color == 1 ? "wK" : "bK");
        }
    }
}
