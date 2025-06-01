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
                return Path.Combine(_folderPath, _filename + Extension);
            }
        }

        public Serializer(string folderPath)
        {
            _folderPath = folderPath;
        }

        public abstract void Serialize(Game game);
        public abstract Game Deserialize();

    }
}
