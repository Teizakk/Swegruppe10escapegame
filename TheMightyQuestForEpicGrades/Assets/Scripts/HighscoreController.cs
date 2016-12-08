using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{
    public class Highscore
    {
        public string score { get; set; }
        public string zeit { get; set; }
        public string name { get; set; }
    }

    public Text score;
    public Text zeit;
    public Text name;

    List<Highscore> highscoreliste = new List<Highscore>();

    // Use this for initialization
    //Start ruft on Gui auf wenn man im Highscoreboard ist (hoffentlich :S)
    void Start()
    {
        //if (SceneManager.GetActiveScene().ToString() == "InsertHighscoreEndOFGame")
        //   {
        //     //if (Persist.load<List<Highscore>>("highscores", Directory.GetCurrentDirectory()) != null)
        //        {
        //             //highscoreliste = Persist.load<List<Highscore>>("highscores", Directory.GetCurrentDirectory());
        //        }
        //     //else
        //      {
        //          highscoreliste = new List<Highscore>();
        //      }
        //   }
        //else if (SceneManager.GetActiveScene().ToString() == "Highscore")
        //       {
        //            if (Persist.load<List<Highscore>>("highscores", Directory.GetCurrentDirectory()) != null)
        //               {
        //                   highscoreliste = Persist.load<List<Highscore>>("highscores", Directory.GetCurrentDirectory());
        //                   //OnGUI();
        //               }
        //            else
        //               {
        //                   highscoreliste = new List<Highscore>();
        //                   //OnGUI();
        //               }
        //        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Speichern eines Highscores EndOFGame
    public void NextWindowOnClick(int level)
    {
        Highscore neu = new Highscore();

        //Felder haben den entsprechenden Tag bekommen
        neu.name = name.text;
        neu.zeit = zeit.text;
        neu.score = score.text;
        Debug.Log("Werte gesetzt auf: " + neu.name + " " + neu.zeit + " " + neu.score);

        highscoreliste.Add(neu);
        //Persist.save<List<Highscore>>(highscoreliste);
        SceneManager.LoadScene(level);
    }

    //Labels auf Gui Highscoreboard
    //void OnGUI()
    //{
    //    if (highscoreliste.Count >= 10)
    //    {
    //        int x = 17;
    //        int y = 138;
    //        int height = 20;
    //        int width = 505;
    //        int platz = 1;

    //        //erzeuge Labels an der stelle wo immoment die Textfelder stehen
    //        for (int i = 0; i < 10; i++)
    //        {
    //            string str = platz + ". " + highscoreliste[i].name + "\t" + highscoreliste[i].score + "\t" + highscoreliste[i].zeit;
    //            GUI.Label(new Rect(x, y, width, height), str);
    //            platz++;
    //            y -= 28;
    //        }

    //    }
    //    else
    //    {
    //        int anzahl = highscoreliste.Count;
    //        int j = 10 - anzahl;

    //        int x = 17;
    //        int y = 138;
    //        int height = 20;
    //        int width = 505;
    //        int platz = 1;

    //        //erzeuge Labels an der stelle wo immoment die Textfelder stehen
    //        for (int i = 0; i < anzahl; i++)
    //        {
    //            string str = platz + ". " + highscoreliste[i].name + "\t" + highscoreliste[i].score + "\t" + highscoreliste[i].zeit;
    //            GUI.Label(new Rect(x, y, width, height), str);
    //            platz++;
    //            y -= 28;
    //        }

    //        for (int k = 0; k < anzahl; k++)
    //        {
    //            string str = platz + ". ";
    //            GUI.Label(new Rect(x, y, width, height), str);
    //            platz++;
    //            y -= 28;
    //        }

    //    }
    //}


}
