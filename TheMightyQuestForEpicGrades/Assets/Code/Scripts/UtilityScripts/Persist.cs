using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Code.Models;
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
            var hs = ExecuteablePath + "\\Highscores";
			var sg = ExecuteablePath + "\\SaveGames";
            var q = ExecuteablePath + "\\Modules";
            var r = ExecuteablePath + "\\Resources";
            if (!Directory.Exists(hs))
                Directory.CreateDirectory(hs);
			if (!Directory.Exists(sg))
                Directory.CreateDirectory(sg);
            if (!Directory.Exists(q))
                Directory.CreateDirectory(q);
            if (!Directory.Exists(r))
                Directory.CreateDirectory(r);
        }

        public static void Save<T>(T state, string fileName) {
            try {
                var bf = new BinaryFormatter();
                using (var file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.OpenOrCreate)) {
                    bf.Serialize(file, state);
                    file.Close();
                    //Debug.Log("Datei: " + fileName + ".dat\nGespeichert in: " + ExecuteablePath + "\\");
                }
            }
            catch (Exception e) {
                //Debug.LogError(e);
            }
        }

        public static T Load<T>(string fileName) where T : new() {
            try {
                if (File.Exists(ExecuteablePath + "\\" + fileName + ".dat")) {
                    var bf = new BinaryFormatter();
                    using (var file = File.Open(ExecuteablePath + "\\" + fileName + ".dat", FileMode.Open)) {
                        var state = (T) bf.Deserialize(file);
                        file.Close();
                        //Debug.Log("Datei: " + fileName + ".dat\nGeladen aus: " + ExecuteablePath + "\\");
                        return state;
                    }
                }
                //Debug.LogError(ExecuteablePath + "\\" + fileName + " existiert nicht!");
                //Debug.Break();
                return default(T);
            }
            catch (Exception e) {
                //Debug.LogError(e);
                //Debug.Break();
                return new T();
            }
        }

        public static List<string> GetModuleFiles() {
            if (Directory.Exists(ExecuteablePath + "\\Modules"))
                return Directory.GetFiles(ExecuteablePath + "\\Modules").ToList().Select(x => { x = Path.GetFileNameWithoutExtension(x); return x; }).ToList();
            Directory.CreateDirectory(ExecuteablePath + "\\Modules"); //eigentlich unnötig, da dies oben im Konstruktor schon gemacht wird.
            return new List<string>();
        }

        public static List<string> GetAllSGIFileNames() {
            if (Directory.Exists(ExecuteablePath + "\\SaveGames"))
                return Directory.GetFiles(ExecuteablePath + "\\SaveGames").ToList().Select(x => {
                    x = Path.GetFileNameWithoutExtension(x);
                    return x;
                }).Where(x => !string.IsNullOrEmpty(x) && x.Contains("_sgi")).ToList();
            Directory.CreateDirectory(ExecuteablePath + "\\SaveGames"); //eigentlich unnötig, da dies oben im Konstruktor schon gemacht wird.
            return new List<string>();
        }

        public static string CopyPictureToResourcesFolder(string filePathAndName) {
            if (!File.Exists(filePathAndName)) {
                throw new FileNotFoundException("Zu kopierendes Bild war unter dem gesetzten Pfad nicht zu finden!");
            }
            var desiredFilePathAndNameAfterCopy = ExecuteablePath + "\\Resources\\" + Path.GetFileName(filePathAndName);
            File.Copy(filePathAndName, desiredFilePathAndNameAfterCopy);
            if (!File.Exists(desiredFilePathAndNameAfterCopy)) {
                throw new FileNotFoundException("Kopieren der Bilddatei in den Resources-Ordner gescheitert!");
            }
            return desiredFilePathAndNameAfterCopy;
        }

public static bool InitializeHighscoreList() {
            if (!Directory.Exists(ExecuteablePath + "\\Highscores")) {
                Debug.LogError("Konstruktor hätte Verzeichnis \\Highscores erstellen sollen?!");
                return false;
            }
            if (File.Exists(ExecuteablePath + "\\Highscores\\" + "highscores" + ".dat")) {
                return true;
            }
            var hsl =  new List<Highscore>()
            {
                new Highscore() {
                    PlayerName = "Hoever",
                    Score = 50,
                    Zeit = "Hoever"
                },
                new Highscore() {
                    PlayerName = "Claßen",
                    Score = 30,
                    Zeit = "schneller"
                },
                new Highscore() {
                    PlayerName = "Fassbender",
                    Score = 10,
                    Zeit = "schnell"
                }
            };
            Save(hsl, "Highscores\\" + "highscores");
            return true;
        }

        public static List<string> GetAllLevelFileNames() {
            return Directory.GetFiles(ExecuteablePath + "\\Levels").Select(x => {
                x = Path.GetFileNameWithoutExtension(x);
                return x;}).ToList();
        }

    }
}