using System.Collections.Generic;
using System.IO;

namespace Assets.Models
{
    public class Level {

        public string Dateipfad = "./Level/";
        public string Dateiprefix = "Level_";
        public string Dateiendung = ".txt";

        private string Dateiname;

        public char[,] loadLevel(int level)
        {
            string Dateiname = Dateiprefix + level + Dateiendung;
            List<string> levelData = new List<string>();
            levelData.Clear();

            using (StreamReader Datei = new StreamReader(Dateipfad + Dateiname))
            {
                string data;

                while ((data = Datei.ReadLine()) != null)
                {
                    levelData.Add(data);
                }
            }

            return to2dCharArray(levelData);
        }

        char[,] to2dCharArray(List<string> list)
        {
            char[,] charArray2d = new char[list.Count, list[0].Length];

            for (int i = 0; i < list.Count; ++i)
                for (int j = 0; j < list[i].Length; ++j)
                    charArray2d[i,j] = list[i][j];

            return charArray2d;
        }
    }
}
