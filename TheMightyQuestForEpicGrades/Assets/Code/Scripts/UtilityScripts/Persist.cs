using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Code.Scripts.UtilityScripts {
    internal class Persist {
        static Persist() {
            ExecuteablePath = Application.persistentDataPath;
            FExt = ".dat";
            AssureDirectoryAndFilesExists();
        }

        public static string ExecuteablePath { get; set; }
        public static string FExt { get; set; }

        // Highscores/highscores
        // SavedStates
        private static void AssureDirectoryAndFilesExists() {
            var st = ExecuteablePath + "\\SavedStates";
            var hs = ExecuteablePath + "\\Highscores";
            if (!Directory.Exists(st))
                Directory.CreateDirectory(st);
            if (!Directory.Exists(hs))
                Directory.CreateDirectory(hs);
        }

        public static void Save<T>(T state, string fileName) {
            try {
                var bf = new BinaryFormatter();
                using (var file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.OpenOrCreate)) {
                    bf.Serialize(file, state);
                    file.Close();
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        public static T Load<T>(string fileName) where T : new() {
            try {
                if (File.Exists(ExecuteablePath + "\\" + fileName + ".dat")) {
                    var bf = new BinaryFormatter();
                    using (var file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.Open)) {
                        var state = (T) bf.Deserialize(file);
                        file.Close();
                        return state;
                    }
                }
                return default(T);
            }
            catch (Exception e) {
                return new T();
            }
        }

        public static List<string> GetSavedStates() {
            if (Directory.Exists(ExecuteablePath + "\\SavedStates"))
                return Directory.GetFiles(ExecuteablePath + "\\SavedStates").ToList().Select( x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            Directory.CreateDirectory(ExecuteablePath + "\\SavedStates");
            return new List<string>();
        }
    }
}