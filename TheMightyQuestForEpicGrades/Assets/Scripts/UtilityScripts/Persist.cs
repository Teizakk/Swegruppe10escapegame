using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Assets.Scripts.UtilityScripts {
    public class Persist {
        public static string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static void saveOld<T>(T state) {
            var s = new JsonSerializerSettings();
            s.TypeNameHandling = TypeNameHandling.Auto;
            File.WriteAllText("~" + DateTime.Now.Ticks, JsonConvert.SerializeObject(state, Formatting.Indented, s));
        }

        public static T loadOld<T>(string name, string path) {
            var deserializer = new JsonSerializer();
            deserializer.TypeNameHandling = TypeNameHandling.Auto;
            if (!File.Exists(path + "\\SavedStates\\" + name))
                throw new FileNotFoundException("The given File does not Exist!");

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path + "\\" + name));
        }

        public static void save<T>(T state, string fileName) {
            var bf = new BinaryFormatter();
            var file = File.Open(path + "\\SavedStates\\" + fileName + ".dat", FileMode.OpenOrCreate);
            bf.Serialize(file, state);
            file.Close();
        }

        public static T load<T>(string fileName) {
            if (File.Exists(path + "\\SavedStates\\" + fileName + ".dat")) {
                var bf = new BinaryFormatter();
                var file = File.Open(path + "\\" + "SavedStates" + "\\" + fileName + ".dat", FileMode.Open);
                var state = (T) bf.Deserialize(file);
                return state;
            }
            return default(T);
        }

        public static List<string> savedStates() {
            if (Directory.Exists(path + "\\SavedStates"))
                return Directory.GetFiles(path + "\\SavedStates").ToList().Select(
                                     x => {
                                         x = Path.GetFileNameWithoutExtension(x);
                                         return x;
                                     }).ToList();
            Directory.CreateDirectory(path + "\\SavedStates");
            return new List<string>();
        }
    }
}