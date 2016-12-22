using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    class Persist
    {
        public static String ExecuteablePath { get; set; }
        public static String FExt { get; set; }
        
        static Persist()
        {
            ExecuteablePath = Application.persistentDataPath;
            FExt = ".dat";
            AssureDirectoryAndFilesExists();
        }

        // Highscores/highscores
        // SavedStates
        private static void AssureDirectoryAndFilesExists()
        {
            String st = ExecuteablePath + "\\SavedStates";
            String hs = ExecuteablePath + "\\Highscores";
            if (!Directory.Exists(st))
                Directory.CreateDirectory(st);
            if (!Directory.Exists(hs))
                Directory.CreateDirectory(hs);            
        }
        public static void save<T>(T state, string fileName)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.OpenOrCreate))
                {
                    bf.Serialize(file, state);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static T load<T>(string fileName) where T : new()
        {
            try
            {
                if (File.Exists(ExecuteablePath + "\\" + fileName + ".dat"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.Open))
                    {
                        T state = (T)bf.Deserialize(file);
                        file.Close();
                        return state;
                    }

                }
                return default(T);
            }
            catch (Exception e)
            {
                return new T();
            }
        }
        public static List<string> savedStates()
        {
            if (Directory.Exists(ExecuteablePath + "\\SavedStates"))
                return Directory.GetFiles(ExecuteablePath + "\\SavedStates").ToList().Select(x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            else
                Directory.CreateDirectory(ExecuteablePath + "\\SavedStates");
            return new List<string>();
        }

    }
}
