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

        private static string ExecuteablePath { get; set; }
        private static string FExt { get; set; }

        // Highscores/highscores
        // SavedStates
        private static void AssureDirectoryAndFilesExists() {
            var st = ExecuteablePath + "\\SavedStates";
            var hs = ExecuteablePath + "\\Highscores";
            var sg = ExecuteablePath + "\\SaveGames";
            if (!Directory.Exists(st))
                Directory.CreateDirectory(st);
            if (!Directory.Exists(hs))
                Directory.CreateDirectory(hs);
            if (!Directory.Exists(sg))
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
                Debug.LogError(e);
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
                Debug.LogError(e);
                return new T();
            }
        }

        public static List<string> GetSavedStates() {
            if (Directory.Exists(ExecuteablePath + "\\SavedStates"))
                return Directory.GetFiles(ExecuteablePath + "\\SavedStates").ToList().Select( x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            Directory.CreateDirectory(ExecuteablePath + "\\SavedStates"); //eigentlich unnötig, da dies oben im Konstruktor schon gemacht wird.
            return new List<string>();
        }

        public static List<string> GetSavedGames() {
            if (Directory.Exists(ExecuteablePath + "\\SaveGames"))
                return Directory.GetFiles(ExecuteablePath + "\\SaveGames").ToList().Select(x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            Directory.CreateDirectory(ExecuteablePath + "\\SaveGames"); //eigentlich unnötig, da dies oben im Konstruktor schon gemacht wird.
            return new List<string>();
        }
    }
}