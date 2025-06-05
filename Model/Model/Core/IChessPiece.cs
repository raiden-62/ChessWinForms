using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal interface IChessPiece
    {
        public int XCoordinate { get; } //строка
        public int YCoordinate { get; } //столбец
        public void Move<T>(int x,int y, T[,] board) where T:ChessPiece; // метод хода
    }
}