using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Controller {
    public class InsertHighscoreManager : MonoBehaviour {

        List<Highscore> highscoreliste = new List<Highscore>();
        private static string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

        public Text score;
        public Text zeit;
        public Text playerName;

        public void NextWindowOnClick(int level)
        {
            if (SceneManager.GetActiveScene().name == "InsertHighscoreEndOFGame")
            {

                try
                {

                    highscoreliste = new List<Highscore>();
                    highscoreliste = Persist.Load<List<Highscore>>(Name);


                }
                catch (Exception e)
                {

                    highscoreliste = new List<Highscore>();
                }

                Highscore neu = new Highscore();

                neu.PlayerName = playerName.text;
                neu.Zeit = zeit.text;
                string scorString = score.text;
                int scoreAsInt = -1;
                int.TryParse(scorString, out scoreAsInt);
                neu.Score = scoreAsInt;


                highscoreliste.Add(neu);
                highscoreliste.OrderBy(x => Convert.ToInt32(x.Score)).Take(10).ToList();
                Persist.Save<List<Highscore>>(highscoreliste, Name);

                SceneManager.LoadScene(level);
            }
        }

    }
}
