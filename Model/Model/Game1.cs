using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public partial class Game
    {
        public ChessPiece[,] Board { get; }
        public int ColorPlayer { get; private set; }
        public int X1 { get; private set; } = -1;
        public int Y1 { get; private set; } = -1;
        public Game(ChessPiece[,] board, int colorplayer)
        {
            Board = board;
            ColorPlayer = colorplayer;
        }
        public Game()
        {
            Board = new ChessPiece[8, 8];
            Board[0, 0] = new Rook(0, 0, 1); Board[7, 0] = new Rook(7, 0, -1);
            Board[0, 1] = new Knight(0, 1, 1); Board[7, 1] = new Knight(7, 1, -1);
            Board[0, 2] = new Bishop(0, 2, 1); Board[7, 2] = new Bishop(7, 2, -1);
            Board[0, 3] = new Queen(0, 3, 1); Board[7, 3] = new Queen(7, 3, -1);
            Board[0, 4] = new King(0, 4, 1); Board[7, 4] = new King(7, 4, -1);
            Board[0, 5] = new Bishop(0, 5, 1); Board[7, 5] = new Bishop(7, 5, -1);
            Board[0, 6] = new Knight(0, 6, 1); Board[7, 6] = new Knight(7, 6,- 1);
            Board[0, 7] = new Rook(0, 7, 1); Board[7, 7] = new Rook(7, 7, -1);
            for (int i = 0; i < 8; i++)
            {
                Board[1, i] = new Pawn(1, i, 1);
                Board[6, i] = new Pawn(6, i, -1);
            }
            ColorPlayer = 1;
        }
        public int Move(int x,int y)
        {
            if ((X1 == -1 || Y1 == -1))
            {
                if ((Board[x, y] != null && Board[x, y].Color == ColorPlayer))
                {
                    X1 = x; Y1 = y;
                    Board[X1, Y1].MoveGenerator(Board); //Ходы должны генерироваться при первом клике 
                }
                return 0;
            }
            //System.Diagnostics.Debug.WriteLine(x);
            //System.Diagnostics.Debug.WriteLine(y);
            Board[X1, Y1].Move(x, y,Board);
            if (Board[X1,Y1] == null)
            {
                ColorPlayer = -ColorPlayer;
                X1 = -1; Y1 = -1;
                return CheckGame();
            }
            X1 = -1; Y1 = -1;
            return 0;
        }
        
    }
}
