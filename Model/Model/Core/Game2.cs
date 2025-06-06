﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public partial class Game
    {
        public int PosGame { get; private set; } // позиция игры
        private (int x, int y) PosKing() // где находится король
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
        private bool IsSquareUnderAttack(int x, int y) // указанная клетка под атакой?
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

        private bool IsCheck() // шах?
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
        
        private bool IsCheckmate() // Мат?
        {
            return IsCheck() && !HasAnyValidMoves();
        }

        private bool IsStalemate() // Пат?
        {
            return !IsCheck() && !HasAnyValidMoves();
        }

        // Проверяет окончание игры (мат или пат) и шах
        private int CheckGame()
        {
            if (IsCheckmate())
            {
                return -ColorPlayer; // Победа противника
            }
            else if (IsStalemate() || IsStalemate0())
            {
                return 2; // Ничья
            }
            else if (IsCheck())
            {
                return 3; // Шах
            }
            return 0; // Игра продолжается
        }
        public static bool operator ==(Game game, int posgame) // проверка положения игры
        {
            if (game.PosGame == posgame) return true;
            return false;
        }
        public static bool operator !=(Game game, int posgame)
        {
            if (game.PosGame != posgame) return true;
            return false;
        }
    }
}
