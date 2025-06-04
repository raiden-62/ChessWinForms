using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public abstract class Serializer
    {
        protected string _folderPath;
        protected const string _filename = "ChessGame";
        protected abstract string Extension { get;}
        protected string FullPath
        {
            get
            {
                if (_folderPath == null) return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _filename+ Extension);
                return Path.Combine(_folderPath, _filename + Extension);
            }
        }

        public Serializer(string folderPath)
        {
            _folderPath = folderPath;
        }

        public abstract void Serialize(Game game);
        public abstract Game Deserialize();

        public static bool IsFileValid(string filePath, bool isJson)
        {
            var ext = Path.GetExtension(filePath);
            if ((isJson && ext != ".json") || (!isJson && ext != ".xml")) return false;
            if (Path.GetFileNameWithoutExtension(filePath) != _filename) return false;

            Serializer serializer = isJson ? new SerializerJSON(Path.GetDirectoryName(filePath)) : new SerializerXML(Path.GetDirectoryName(filePath));
            try
            {
                var game = serializer.Deserialize();
                if (game == null) return false;
                //if (game.Move(0,0) == -1 || game.Move(0, 0) == 1) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
