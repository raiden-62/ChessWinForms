namespace Model
{
    public abstract class ChessPiece : IChessPiece, IFutureMove
    {
        public List<(int x, int y)> Moves { get; private set; }
        public int XCoordinate { get; private set; } //это как бы Y (строка)
        public int YCoordinate { get; private set; }// это как бы X(столбец)
        public int Color { get; } //-1: Чёрные, 1: Белые   
        public bool FirstMove { get; private set; } // это первый ход?
        public virtual void Move<T>(int x, int y, T[,] board)where T: ChessPiece
        {
            if (Moves.Contains((x, y))) // если это возможный ход
            {
                int oldx = XCoordinate, oldy = YCoordinate = YCoordinate;
                FirstMove = false;
                XCoordinate = x;
                YCoordinate = y;
                board[x, y] = this as T;
                board[oldx, oldy] = null;
            }
        }
        
        public ChessPiece(int x,int y,int color,bool firstmove=true)
        {
            FirstMove = firstmove;
            XCoordinate = x;
            YCoordinate = y;
            Color = color;
        }
        public virtual void MoveGenerator<T>(T[,] Board)where T:ChessPiece { Moves = []; } // обнуление возможных ходов
        protected void Add(int x,int y) // добавление в хода в возомжные
        {
            Moves.Add((x, y));
        }
        internal void VirtualMove(int x,int y) //ход для нужный для проверки положения при возможном ходе
        {
            XCoordinate = x;
            YCoordinate = y;
        }
    }
}
