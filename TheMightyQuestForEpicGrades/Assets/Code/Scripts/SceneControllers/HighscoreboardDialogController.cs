using System;
using System.Collections.Generic;
using Assets.Code.Manager;
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
        private void Awake() {
            if (SceneManager.GetActiveScene().name == "Highscore") {
                if(Persist.InitializeHighscoreList())
                //if (Persist.Load<HighscoreWrapper>(Name) != null) -> funktioniert schmeißt aber einen Fehler beim Laden, wenn keine Datei da ist und verursacht im Moment einen Debug.Break()
                {
                    var wrappedHighscore = Persist.Load<HighscoreWrapper>(Name);
                    highscoreliste = wrappedHighscore.List;
                }

                //Wenn Liste voll ist
                if (highscoreliste.Count >= 10) {
                    p1.text = " 1.   " + highscoreliste[0].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[0].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[0].Zeit + "   " + highscoreliste[0].Datum.Date.ToString();
                    p2.text = " 2.   " + highscoreliste[1].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[1].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[1].Zeit + "   " + highscoreliste[1].Datum.Date.ToString();
                    p3.text = " 3.   " + highscoreliste[2].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[2].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[2].Zeit + "   " + highscoreliste[2].Datum.Date.ToString();
                    p4.text = " 4.   " + highscoreliste[3].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[3].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[3].Zeit +"   " + highscoreliste[3].Datum.Date.ToString();
                    p5.text = " 5.   " + highscoreliste[4].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[4].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[4].Zeit + "   " + highscoreliste[4].Datum.Date.ToString();
                    p6.text = " 6.   " + highscoreliste[5].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[5].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[5].Zeit + "   " + highscoreliste[5].Datum.Date.ToString();
                    p7.text = " 7.   " + highscoreliste[6].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[6].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[6].Zeit + "   " + highscoreliste[6].Datum.Date.ToString();
                    p8.text = " 8.   " + highscoreliste[7].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[7].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[7].Zeit + "   " + highscoreliste[7].Datum.Date.ToString();
                    p9.text = " 9.   " + highscoreliste[8].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[8].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[8].Zeit + "   " + highscoreliste[8].Datum.Date.ToString();
                    p10.text = "10.   " + highscoreliste[9].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[9].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[9].Zeit + "   " + highscoreliste[9].Datum.Date.ToString();
                }
                
                //Es git Einträge Liste ist aber nicht voll
                else if (highscoreliste.Count > 0) {
                    DateTime defaultt = new DateTime();
                    var anzahl = highscoreliste.Count;
                    var i = 0;
                    for (; i < anzahl; i++) {
                        if (i == 0)
                            p1.text = " 1.   " + highscoreliste[0].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[0].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[0].Zeit + "   " + highscoreliste[0].Datum.Date.ToString();
                        if (i == 1)
                            p2.text = " 2.   " + highscoreliste[1].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[1].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[1].Zeit + "   " + highscoreliste[1].Datum.Date.ToString();
                        if (i == 2)
                            p3.text = " 3.   " + highscoreliste[2].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[2].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[2].Zeit + "   " + highscoreliste[2].Datum.Date.ToString();
                        if (i == 3)
                            p4.text = " 4.   " + highscoreliste[3].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[3].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[3].Zeit + "   " + highscoreliste[3].Datum.Date.ToString();
                        if (i == 4)
                            p5.text = " 5.   " + highscoreliste[4].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[4].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[4].Zeit + "   " + highscoreliste[4].Datum.Date.ToString();
                        if (i == 5)
                            p6.text = " 6.   " + highscoreliste[5].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[5].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[5].Zeit + "   " + highscoreliste[5].Datum.Date.ToString();
                        if (i == 6)
                            p7.text = " 7.   " + highscoreliste[6].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[6].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[6].Zeit + "   " + highscoreliste[6].Datum.Date.ToString();
                        if (i == 7)
                            p8.text = " 8.   " + highscoreliste[7].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[7].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[7].Zeit + "   " + highscoreliste[7].Datum.Date.ToString();
                        if (i == 8)
                            p9.text = " 9.   " + highscoreliste[8].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[8].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[8].Zeit + "   " + highscoreliste[8].Datum.Date.ToString();
                        if (i == 9)
                            p10.text = "10.   " + highscoreliste[9].Score.ToString().PadLeft(6, '0') +  "   " + highscoreliste[9].PlayerName.PadRight(20, ' ') + "   " +
                                       highscoreliste[9].Zeit + "   " + highscoreliste[9].Datum.Date.ToString();
                    }
                    //Liste ist leer, es gibt keien Einträge
                    
                    for (; i < 10; i++) {
                        if (i == 0)
                            p1.text = " 1.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 1)
                            p2.text = " 2.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 2)
                            p3.text = " 3.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 3)
                            p4.text = " 4.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 4)
                            p5.text = " 5.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 5)
                            p6.text = " 6.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 6)
                            p7.text = " 7.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 7)
                            p8.text = " 8.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 8)
                            p9.text = " 9.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                        if (i == 9)
                            p10.text = "10.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    }
                }
                
                else {
                    DateTime defaultt = new DateTime();
                    p1.text = " 1.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p2.text = " 2.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p3.text = " 3.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p4.text = " 4.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p5.text = " 5.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p6.text = " 6.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p7.text = " 7.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p8.text = " 8.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p9.text = " 9.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p10.text = "10.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                }
            }
        }

        #region Master-Link
        private void Start() {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}
