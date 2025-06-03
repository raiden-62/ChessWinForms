namespace Model
{
    public abstract class ChessPiece : IChessPiece, IFutureMove
    {
        public List<(int x, int y)> Moves { get; private set; }
        public int XCoordinate { get; private set; } //это как бы Y (строка)
        public int YCoordinate { get; private set; }// это как бы X(столбец)
        public int Color { get; } //-1: Чёрные, 1: Белые   
        public bool FirstMove { get; private set; }
        public virtual void Move(int x, int y, ChessPiece[,] board)
        {
            System.Diagnostics.Debug.WriteLine(x);
            System.Diagnostics.Debug.WriteLine(y);
            if (Moves.Contains((x, y)))
            {
                int oldx = XCoordinate, oldy = YCoordinate = YCoordinate;
                FirstMove = false;
                XCoordinate = x;
                YCoordinate = y;
                board[x, y] = this;
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
        public virtual void MoveGenerator(ChessPiece[,] Board) { Moves = []; }
        protected void Add(int x,int y)
        {
            Moves.Add((x, y));
        }
        internal void VirtualMove(int x,int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
    }
}
