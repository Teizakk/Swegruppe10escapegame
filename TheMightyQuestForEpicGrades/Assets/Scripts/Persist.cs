using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts
{
    class Persist
    {

        public static void saveOld<T>(T state)
        {
            QuestionController q = QuestionController.GetInstance();
            q.
            JsonSerializerSettings s = new JsonSerializerSettings();
            s.TypeNameHandling = TypeNameHandling.Auto;
            File.WriteAllText("~" + DateTime.Now.Ticks, JsonConvert.SerializeObject(state, Formatting.Indented, s));
        }

        public static T loadOld<T>(string name, string path)
        {
            JsonSerializer deserializer = new JsonSerializer();
            deserializer.TypeNameHandling = TypeNameHandling.Auto;
            if (!File.Exists(path + "\\SavedStates\\" + name))
                throw new FileNotFoundException("The given File does not Exist!");

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path + "\\" + name));
        }

        public static string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static void save<T>(T state, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + "\\SavedStates\\" + fileName + ".dat", FileMode.OpenOrCreate);
            bf.Serialize(file, state);
            file.Close();
        }

        public static T load<T>(string fileName)
        {
            if (File.Exists(path + "\\SavedStates\\" + fileName + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path + "\\" + "SavedStates" + "\\" + fileName + ".dat", FileMode.Open);
                T state = (T)bf.Deserialize(file);
                return state;
            }
            return default(T);
        }
        public static List<string> savedStates()
        {
            if (Directory.Exists(path + "\\SavedStates"))
                return Directory.GetFiles(path + "\\SavedStates").ToList().Select(x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            else
                Directory.CreateDirectory(path + "\\SavedStates");
            return new List<string>();
        }

    }
}
