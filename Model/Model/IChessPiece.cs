﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal interface IChessPiece
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public void Move(int x,int y, ChessPiece[,] board);
    }
}