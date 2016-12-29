using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;

namespace Assets.Code.Manager {
    public class LevelManager : MonoBehaviour {

        private char[,] _levelData;
        private string _loadedLevel;

        #region Dateinamen-Konfiguration
        private readonly string Dateiendung = ".txt";
        private readonly string Dateipfad = "./Level/";
        private readonly string Dateiprefix = "Level_";
        #endregion

        public void LoadFromFile(int index) {
            var Dateiname = Dateiprefix + index + Dateiendung;
            var levelData = new List<string>();
            levelData.Clear();

            using (var Datei = new StreamReader(Dateipfad + Dateiname)) {
                string data;

                while ((data = Datei.ReadLine()) != null)
                    levelData.Add(data);
            }
            _loadedLevel = Dateiname;
            _levelData = to2dCharArray(levelData);
        }

        //Hilfsfunktion
        private char[,] to2dCharArray(List<string> list) {
            var charArray2d = new char[list.Count, list[0].Length];

            for (var i = 0; i < list.Count; ++i)
                for (var j = 0; j < list[i].Length; ++j)
                    charArray2d[i, j] = list[i][j];

            return charArray2d;
        }

        public string GetLoadedLevel() {
            return _loadedLevel;
        }

        public char[,] GetLevelData() {
            return _levelData;
        }
    }
}