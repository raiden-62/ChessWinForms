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
            

            //shitty code incoming
            string folder = _folderPath != null ? _folderPath : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //shitty code over

            string filePath = Path.Combine(folder,_filename + ".json");
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(game, settings);
            File.WriteAllText(filePath, json);
        }
        public override Game Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}
