using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.Manager {
    public class LevelManager : MonoBehaviour {
        public string Dateiendung = ".txt";

        private string Dateiname;

        public string Dateipfad = "./Level/";
        public string Dateiprefix = "Level_";

        public char[,] loadLevel(int level) {
            var Dateiname = Dateiprefix + level + Dateiendung;
            var levelData = new List<string>();
            levelData.Clear();

            using (var Datei = new StreamReader(Dateipfad + Dateiname)) {
                string data;

                while ((data = Datei.ReadLine()) != null)
                    levelData.Add(data);
            }

            return to2dCharArray(levelData);
        }

        private char[,] to2dCharArray(List<string> list) {
            var charArray2d = new char[list.Count, list[0].Length];

            for (var i = 0; i < list.Count; ++i)
                for (var j = 0; j < list[i].Length; ++j)
                    charArray2d[i, j] = list[i][j];

            return charArray2d;
        }

        public void Awake() {
            Debug.Log("Awake called bei Component LevelManager");
        }
        public void Start() {
            Debug.Log("Start called bei Component LevelManager");
        }
    }
}