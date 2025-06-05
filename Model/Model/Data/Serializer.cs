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
                if (_folderPath == null) return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _filename+ Extension); //По умолчанию всегда рабочий стол
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
            if ((isJson && ext != ".json") || (!isJson && ext != ".xml")) return false; //соответствие расширения
            if (Path.GetFileNameWithoutExtension(filePath) != _filename) return false;// название = ChessGame

            Serializer serializer = isJson ? new SerializerJSON(Path.GetDirectoryName(filePath)) : new SerializerXML(Path.GetDirectoryName(filePath));
            try //Попытка десериализации
            {
                var game = serializer.Deserialize();
                if (game == null) return false;
                if (game == -1 || game == 1) return false; //Игра уже окончена
            }
            catch //Произошла ошибка
            {
                return false;
            }

            return true;
        }
    }
}
