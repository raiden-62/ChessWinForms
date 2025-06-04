using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SerializerJSON : Serializer
    {
        protected override string Extension => ".json";
        public SerializerJSON(string folderPath) : base(folderPath)
        {
        }

        public override void Serialize(Game game)
        {

            var dto = new ChessGameDTO(game);
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            File.WriteAllText(FullPath, json);

        }
        public override Game Deserialize()
        {
            string json = File.ReadAllText(FullPath);
            var dto = JsonConvert.DeserializeObject<ChessGameDTO>(json);

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
                    else if (type == typeof(Queen).ToString()) piece = new Queen(dtoPiece.Row, dtoPiece.Column, dtoPiece.Color);
                    else piece = null;

                    board[i / 8, i % 8] = piece;
                }
            }

            return new Game(board, dto.ColorPlayer, dto.GameState);
        }
    }
}
