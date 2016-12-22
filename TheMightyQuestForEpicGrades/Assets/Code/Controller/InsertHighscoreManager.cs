using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Assets.Code.Scripts.UtilityScripts;

public class InsertHighscoreManager : MonoBehaviour {

    List<Highscore> highscoreliste = new List<Highscore>();
    private static string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

    public Text score;
    public Text zeit;
    public Text playerName;

    public void NextWindowOnClick(int level)
    {
        if (Application.loadedLevelName == "InsertHighscoreEndOFGame")
        {

            try
            {

                highscoreliste = new List<Highscore>();
                highscoreliste = Persist.load<List<Highscore>>(Name);


            }
            catch (Exception e)
            {

                highscoreliste = new List<Highscore>();
            }

            Highscore neu = new Highscore();

            neu.name = playerName.text;
            neu.zeit = zeit.text;
            string scorString = score.text;
            int scoreAsInt = -1;
            int.TryParse(scorString, out scoreAsInt);
            neu.score = scoreAsInt;


            highscoreliste.Add(neu);
            highscoreliste.OrderBy(x => Convert.ToInt32(x.score)).Take(10).ToList();
            Persist.save<List<Highscore>>(highscoreliste, Name);

            SceneManager.LoadScene(level);
        }
    }

}
