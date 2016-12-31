﻿using System.Collections.Generic;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class HighscoreboardDialogController : MonoBehaviour {
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

        private List<Highscore> highscoreliste = new List<Highscore>();
        private static readonly string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

        // Use this for initialization
        //nur beim Highscoreboard genutzt
        private void Start() {
            if (SceneManager.GetActiveScene().name == "Highscore") {
                try {
                    highscoreliste = new List<Highscore>();
                    highscoreliste = Persist.Load<List<Highscore>>(Name);
                }
                catch {
                    highscoreliste = new List<Highscore>();
                }


                //Wenn Liste voll ist
                if (highscoreliste.Count >= 10) {
                    p1.text = "1. " + highscoreliste[0].PlayerName + "\t" + highscoreliste[0].Score + "\t" +
                              highscoreliste[0].Zeit;
                    p2.text = "2. " + highscoreliste[1].PlayerName + "\t" + highscoreliste[1].Score + "\t" +
                              highscoreliste[1].Zeit;
                    p3.text = "3. " + highscoreliste[2].PlayerName + "\t" + highscoreliste[2].Score + "\t" +
                              highscoreliste[2].Zeit;
                    p4.text = "4. " + highscoreliste[3].PlayerName + "\t" + highscoreliste[3].Score + "\t" +
                              highscoreliste[3].Zeit;
                    p5.text = "5. " + highscoreliste[4].PlayerName + "\t" + highscoreliste[4].Score + "\t" +
                              highscoreliste[4].Zeit;
                    p6.text = "6. " + highscoreliste[5].PlayerName + "\t" + highscoreliste[5].Score + "\t" +
                              highscoreliste[5].Zeit;
                    p7.text = "7. " + highscoreliste[6].PlayerName + "\t" + highscoreliste[6].Score + "\t" +
                              highscoreliste[6].Zeit;
                    p8.text = "8. " + highscoreliste[7].PlayerName + "\t" + highscoreliste[7].Score + "\t" +
                              highscoreliste[7].Zeit;
                    p9.text = "9. " + highscoreliste[8].PlayerName + "\t" + highscoreliste[8].Score + "\t" +
                              highscoreliste[8].Zeit;
                    p10.text = "10. " + highscoreliste[9].PlayerName + "\t" + highscoreliste[9].Score + "\t" +
                               highscoreliste[9].Zeit;
                }
                //Es git Einträge Liste ist aber nicht voll
                else if (highscoreliste.Count > 0) {
                    var anzahl = highscoreliste.Count;
                    var i = 0;
                    for (; i < anzahl; i++) {
                        if (i == 0)
                            p1.text = "1. " + highscoreliste[0].PlayerName + "\t" + highscoreliste[0].Score + "\t" +
                                      highscoreliste[0].Zeit;
                        if (i == 1)
                            p2.text = "2. " + highscoreliste[1].PlayerName + "\t" + highscoreliste[1].Score + "\t" +
                                      highscoreliste[1].Zeit;
                        if (i == 2)
                            p3.text = "3. " + highscoreliste[2].PlayerName + "\t" + highscoreliste[2].Score + "\t" +
                                      highscoreliste[2].Zeit;
                        if (i == 3)
                            p4.text = "4. " + highscoreliste[3].PlayerName + "\t" + highscoreliste[3].Score + "\t" +
                                      highscoreliste[3].Zeit;
                        if (i == 4)
                            p5.text = "5. " + highscoreliste[4].PlayerName + "\t" + highscoreliste[4].Score + "\t" +
                                      highscoreliste[4].Zeit;
                        if (i == 5)
                            p6.text = "6. " + highscoreliste[5].PlayerName + "\t" + highscoreliste[5].Score + "\t" +
                                      highscoreliste[5].Zeit;
                        if (i == 6)
                            p7.text = "7. " + highscoreliste[6].PlayerName + "\t" + highscoreliste[6].Score + "\t" +
                                      highscoreliste[6].Zeit;
                        if (i == 7)
                            p8.text = "8. " + highscoreliste[7].PlayerName + "\t" + highscoreliste[7].Score + "\t" +
                                      highscoreliste[7].Zeit;
                        if (i == 8)
                            p9.text = "9. " + highscoreliste[8].PlayerName + "\t" + highscoreliste[8].Score + "\t" +
                                      highscoreliste[8].Zeit;
                        if (i == 9)
                            p10.text = "10. " + highscoreliste[9].PlayerName + "\t" + highscoreliste[9].Score + "\t" +
                                       highscoreliste[9].Zeit;
                    }
                    //Liste ist leer, es gibt keien Einträge
                    for (; i < 10; i++) {
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
                else {
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
}
