using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Assets.Scripts
{
    class Persist
    {

        public static void save<T>(T state)
        {
            JsonSerializerSettings s = new JsonSerializerSettings();
            s.TypeNameHandling = TypeNameHandling.Auto;
            File.WriteAllText("~" + DateTime.Now.Ticks, JsonConvert.SerializeObject(state, Formatting.Indented, s));
        }

        public static T load<T>(string name, string path)
        {
            JsonSerializer deserializer = new JsonSerializer();
            deserializer.TypeNameHandling = TypeNameHandling.Auto;
            if (!File.Exists(path + "\\" + name))
                throw new FileNotFoundException("The given File does not Exist!");

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path + "\\" + name));
        }
    }
}
