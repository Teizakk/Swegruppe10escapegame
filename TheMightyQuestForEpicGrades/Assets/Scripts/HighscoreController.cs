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

    //Für Speichern
    public Text score;
    public Text zeit;
    public Text name;

    //Für laden
    public Text p1;
    public Text p2;
    public Text p3;
    public Text p4;
    public Text p5;
    public Text p6;
    public Text p7;
    public Text p8;
    public Text p9;
    public Text p10;


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
        Debug.Log("Ich werde ausgeführt!");
        
        if (Application.loadedLevelName == "Highscore")
        {
            Debug.Log("Ich bin in der Scene!");
            if (highscoreliste.Count >= 10)
            {
                p1.text = "1. " + highscoreliste[0].name + "\t" + highscoreliste[0].score + "\t" + highscoreliste[0].zeit;
                p2.text = "2. " + highscoreliste[1].name + "\t" + highscoreliste[1].score + "\t" + highscoreliste[1].zeit;
                p3.text = "3. " + highscoreliste[2].name + "\t" + highscoreliste[2].score + "\t" + highscoreliste[2].zeit;
                p4.text = "4. " + highscoreliste[3].name + "\t" + highscoreliste[3].score + "\t" + highscoreliste[3].zeit;
                p5.text = "5. " + highscoreliste[4].name + "\t" + highscoreliste[4].score + "\t" + highscoreliste[4].zeit;
                p6.text = "6. " + highscoreliste[5].name + "\t" + highscoreliste[5].score + "\t" + highscoreliste[5].zeit;
                p7.text = "7. " + highscoreliste[6].name + "\t" + highscoreliste[6].score + "\t" + highscoreliste[6].zeit;
                p8.text = "8. " + highscoreliste[7].name + "\t" + highscoreliste[7].score + "\t" + highscoreliste[7].zeit;
                p9.text = "9. " + highscoreliste[8].name + "\t" + highscoreliste[8].score + "\t" + highscoreliste[8].zeit;
                p10.text = "10. " + highscoreliste[9].name + "\t" + highscoreliste[9].score + "\t" + highscoreliste[9].zeit;
            }
            else
            {
                p1.text = "1. XX\t\tXX\t\tXX";
                p2.text = "2. XX\t\tXX\t\tXX";
                p3.text = "3. XX\t\tXX\t\tXX";
                p4.text = "4. XX\t\tXX\t\tXX";
                p5.text = "5. XX\t\tXX\t\tXX";
                p6.text = "6. XX\t\tXX\t\tXX";
                p7.text = "7. XX\t\tXX\t\tXX";
                p8.text = "8. XX\t\tXX\t\tXX";
                p9.text = "9. XX\t\tXX\t\tXX";
                p10.text = "10. XX\t\tXX\t\tXX";

                
            }
        }
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
