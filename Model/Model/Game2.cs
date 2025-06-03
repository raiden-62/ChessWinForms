using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Game
    {
        private (int x, int y) PosKing()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y] is King && Board[x, y].Color == ColorPlayer)
                    {
                        return (x, y);
                    }
                }
            }
            return (-1,-1) ;
        }
        private bool IsSquareUnderAttack(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessPiece piece = Board[i, j];
                    if (piece != null && piece.Color != ColorPlayer)
                    {
                        piece.MoveGenerator(Board); 
                        if (piece.Moves.Contains((x, y)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsCheck()
        {
            (int kingX, int kingY) = PosKing();
            return IsSquareUnderAttack(kingX, kingY);
        }

        // Проверяет, есть ли у короля или других фигур возможные ходы
        private bool HasAnyValidMoves()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessPiece piece = Board[x, y];
                    if (piece != null && piece.Color == ColorPlayer)
                    {
                        piece.MoveGenerator(Board);
                        foreach (var move in piece.Moves)
                        {
                            ChessPiece originalPiece = Board[move.x, move.y];
                            int oldX = piece.XCoordinate, oldY = piece.YCoordinate;
                            Board[x, y] = null;
                            Board[move.x, move.y] = piece;
                            piece.VirtualMove(move.x, move.y);
                            bool stillInCheck = IsCheck();
                            Board[x, y] = piece;
                            Board[move.x, move.y] = originalPiece;
                            piece.VirtualMove(oldX, oldY);
                            if (!stillInCheck)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool IsCheckmate()
        {
            return IsCheck() && !HasAnyValidMoves();
        }

        private bool IsStalemate()
        {
            return !IsCheck() && !HasAnyValidMoves();
        }

        // Проверяет окончание игры (мат или пат) и шах
        public int CheckGameEnd()
        {
            if (IsCheckmate())
            {
                return -ColorPlayer; // Победа противника
            }
            else if (IsStalemate())
            {
                return 0; // Ничья
            }
            else if (IsCheck())
            {
                return 2; // Шах
            }
            return 3; // Игра продолжается
        }
    }
}
