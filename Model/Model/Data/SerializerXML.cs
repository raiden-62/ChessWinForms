using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    public class ChessPieceDTO
    {
        public string Type { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool FirstMove { get; set; }
        public int Color { get; set; }

        public ChessPieceDTO() { }
        public ChessPieceDTO(ChessPiece piece)
        {

            Type = piece.GetType().ToString();
            Row = piece.XCoordinate;
            Column = piece.YCoordinate;
            FirstMove = piece.FirstMove;
            Color = piece.Color;
        }

    }

    public class ChessGameDTO
    {
        public ChessPieceDTO[] Board2D { get; set; }
        public int ColorPlayer { get; set; }
        public int GameState {  get; set; }
        public ChessGameDTO() { }
        public ChessGameDTO(Game game)
        {
            ColorPlayer = game.ColorPlayer;
            GameState = game.PosGame;
            Board2D = new ChessPieceDTO[64]; //flattened board
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (game.Board[row, col] != null) Board2D[row * 8 + col] = new ChessPieceDTO(game.Board[row, col]);
                }
            }
        }

    }


    public class SerializerXML : Serializer
    {
        protected override string Extension => ".xml";

        public SerializerXML(string folderPath) : base(folderPath) { }


        public override void Serialize(Game game)
        {
            var dto = new ChessGameDTO(game);
            var serializer = new XmlSerializer(typeof(ChessGameDTO));

            using var writer = new StreamWriter(FullPath);
            serializer.Serialize(writer, dto);
        }

        public override Game Deserialize()
        {
            using var reader = new StreamReader(FullPath);
            var serializer = new XmlSerializer(typeof(ChessGameDTO));
            var dto = (ChessGameDTO)serializer.Deserialize(reader);

            ChessPiece[,] board = new ChessPiece[8, 8];
            for (int i = 0; i < 64; i++)
            {
                var dtoPiece = dto.Board2D[i];
                if (dtoPiece != null)
                {
                    ChessPiece piece;
                    string type = dtoPiece.Type;
                    if (type == typeof(Pawn).ToString()) piece = new Pawn(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color, dtoPiece.FirstMove);
                    else if (type == typeof(Knight).ToString()) piece = new Knight(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color);
                    else if (type == typeof(Bishop).ToString()) piece = new Bishop(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color);
                    else if (type == typeof(Rook).ToString()) piece = new Rook(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color, dtoPiece.FirstMove);
                    else if (type == typeof(King).ToString()) piece = new King(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color, dtoPiece.FirstMove);
                    else if (type == typeof(Queen).ToString()) piece = new Bishop(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color);
                    else piece = null;

                    board[i / 8, i % 8] = piece;
                }
            }

            return new Game(board, dto.ColorPlayer, dto.GameState);
        }


    }
}
