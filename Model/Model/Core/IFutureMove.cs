using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IFutureMove
    {
        public List<(int x, int y)> Moves { get; } //возможные ходы
        public void MoveGenerator<T>(T[,] board) where T : ChessPiece; // генерация возможных ходов
    }
}
