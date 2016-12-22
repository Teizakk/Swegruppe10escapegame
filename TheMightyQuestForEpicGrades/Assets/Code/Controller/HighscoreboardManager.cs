using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Code.Scripts.UtilityScripts;

public class HighscoreboardManager : MonoBehaviour
{

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
    private static string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

    // Use this for initialization
    //nur beim Highscoreboard genutzt
    void Start()
    {


        if (Application.loadedLevelName == "Highscore")
        {

            try
            {
                highscoreliste = new List<Highscore>();
                highscoreliste = Persist.load<List<Highscore>>(Name);
            }
            catch
            {
                highscoreliste = new List<Highscore>();
            }


            //Wenn Liste voll ist
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
            //Es git Einträge Liste ist aber nicht voll
            else if (highscoreliste.Count > 0)
            {
                int anzahl = highscoreliste.Count;
                int i = 0;
                for (; i < anzahl; i++)
                {
                    if (i == 0)
                        p1.text = "1. " + highscoreliste[0].name + "\t" + highscoreliste[0].score + "\t" + highscoreliste[0].zeit;
                    if (i == 1)
                        p2.text = "2. " + highscoreliste[1].name + "\t" + highscoreliste[1].score + "\t" + highscoreliste[1].zeit;
                    if (i == 2)
                        p3.text = "3. " + highscoreliste[2].name + "\t" + highscoreliste[2].score + "\t" + highscoreliste[2].zeit;
                    if (i == 3)
                        p4.text = "4. " + highscoreliste[3].name + "\t" + highscoreliste[3].score + "\t" + highscoreliste[3].zeit;
                    if (i == 4)
                        p5.text = "5. " + highscoreliste[4].name + "\t" + highscoreliste[4].score + "\t" + highscoreliste[4].zeit;
                    if (i == 5)
                        p6.text = "6. " + highscoreliste[5].name + "\t" + highscoreliste[5].score + "\t" + highscoreliste[5].zeit;
                    if (i == 6)
                        p7.text = "7. " + highscoreliste[6].name + "\t" + highscoreliste[6].score + "\t" + highscoreliste[6].zeit;
                    if (i == 7)
                        p8.text = "8. " + highscoreliste[7].name + "\t" + highscoreliste[7].score + "\t" + highscoreliste[7].zeit;
                    if (i == 8)
                        p9.text = "9. " + highscoreliste[8].name + "\t" + highscoreliste[8].score + "\t" + highscoreliste[8].zeit;
                    if (i == 9)
                        p10.text = "10. " + highscoreliste[9].name + "\t" + highscoreliste[9].score + "\t" + highscoreliste[9].zeit;
                }
                //Liste ist leer, es gibt keien Einträge
                for (; i < 10; i++)
                {
                    if (i == 0)
                        p1.text = "1. XX\t\tXX\t\tXX";
                    if (i == 1)
                        p2.text = "2. XX\t\tXX\t\tXX";
                    if (i == 2)
                        p3.text = "3. XX\t\tXX\t\tXX";
                    if (i == 3)
                        p4.text = "4. XX\t\tXX\t\tXX";
                    if (i == 4)
                        p5.text = "5. XX\t\tXX\t\tXX";
                    if (i == 5)
                        p6.text = "6. XX\t\tXX\t\tXX";
                    if (i == 6)
                        p7.text = "7. XX\t\tXX\t\tXX";
                    if (i == 7)
                        p8.text = "8. XX\t\tXX\t\tXX";
                    if (i == 8)
                        p9.text = "9. XX\t\tXX\t\tXX";
                    if (i == 9)
                        p10.text = "10. XX\t\tXX\t\tXX";
                }
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
}
