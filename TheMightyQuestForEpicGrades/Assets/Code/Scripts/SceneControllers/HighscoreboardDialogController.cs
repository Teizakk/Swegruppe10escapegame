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
        

		public Text[] ps;

        private List<Highscore> highscoreliste = new List<Highscore>();
        private static readonly string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

        // Use this for initialization
        //nur beim Highscoreboard genutzt
        private void Awake() {
			
			Debug.Log ("Highscoreliste.Count: " + highscoreliste.Count);
            if (SceneManager.GetActiveScene().name == "Highscore") {
                if(Persist.InitializeHighscoreList())
                //if (Persist.Load<List<Highscore>>(Name) != null) -> funktioniert schmeißt aber einen Fehler beim Laden, wenn keine Datei da ist und verursacht im Moment einen Debug.Break()
                {
                    highscoreliste = Persist.Load<List<Highscore>>(Name);
                }

                //Wenn Liste voll ist
                if (highscoreliste.Count >= 10) {
					string str;
					for (int i = 0; i < 10; ++i) {
						str = (i < (10 - 1)) ? " " : "";

						ps [i].text = str + (i + 1).ToString() + ".\t" + highscoreliste [i].Score.ToString ().PadLeft (6, '0') + "\t" +
							highscoreliste [i].PlayerName.PadRight (12, ' ') + "\t(" +
							highscoreliste [i].Module + ")\t" +
							highscoreliste [i].Zeit.PadRight (12, ' ') + "\t" + highscoreliste [i].Datum.Date.ToString ("dd/MM/yyyy");
					}
					/*p1.text = " 1.   " + highscoreliste[0].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[0].PlayerName.PadRight(12, ' ') + "   (" + highscoreliste[0].Module + ")\t" +
						highscoreliste[0].Zeit.PadRight(12, ' ') + "   " + highscoreliste[0].Datum.Date.ToString();
					p2.text = " 2.   " + highscoreliste[1].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[1].PlayerName.PadRight(12, ' ') + "   (" + highscoreliste[1].Module + ")\t" +
						highscoreliste[1].Zeit.PadRight(12, ' ') + "   " + highscoreliste[1].Datum.Date.ToString();
					p3.text = " 3.   " + highscoreliste[2].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[2].PlayerName.PadRight(12, ' ') + "   (" + highscoreliste[2].Module + ")\t" +
						highscoreliste[2].Zeit.PadRight(12, ' ') + "   " + highscoreliste[2].Datum.Date.ToString();
					p4.text = " 4.   " + highscoreliste[3].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[3].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[3].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[3].Zeit +"   " + highscoreliste[3].Datum.Date.ToString();
					p5.text = " 5.   " + highscoreliste[4].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[4].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[4].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[4].Zeit + "   " + highscoreliste[4].Datum.Date.ToString();
					p6.text = " 6.   " + highscoreliste[5].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[5].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[5].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[5].Zeit + "   " + highscoreliste[5].Datum.Date.ToString();
					p7.text = " 7.   " + highscoreliste[6].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[6].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[6].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[6].Zeit + "   " + highscoreliste[6].Datum.Date.ToString();
					p8.text = " 8.   " + highscoreliste[7].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[7].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[7].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[7].Zeit + "   " + highscoreliste[7].Datum.Date.ToString();
					p9.text = " 9.   " + highscoreliste[8].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[8].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[8].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[8].Zeit + "   " + highscoreliste[8].Datum.Date.ToString();
					p10.text = "10.   " + highscoreliste [9].Score.ToString ().PadLeft (6, '0') + "   " + highscoreliste [9].PlayerName.PadRight (20, ' ') + "   (" + highscoreliste [9].Module.PadRight (6, ' ') + ")   " +
					highscoreliste [9].Zeit + "   " + highscoreliste [9].Datum.Date.ToString ();*/
                }
                
                //Es git Einträge Liste ist aber nicht voll
                else if (highscoreliste.Count > 0) {
                    DateTime defaultt = new DateTime();
                    var anzahl = highscoreliste.Count;
					string str;
					int i = 0;
					for (; i < anzahl; i++) {
						str = (i < (10 - 1)) ? " " : "";
						ps [i].text = str + (i + 1).ToString () + ".\t" + highscoreliste [i].Score.ToString ().PadLeft (6, '0') + "\t" +
						highscoreliste [i].PlayerName.PadRight (12, ' ') + "\t(" +
							highscoreliste [i].Module + ")\t" +
						highscoreliste [i].Zeit.PadRight (12, ' ') + "\t" + highscoreliste [i].Datum.Date.ToString ("dd/MM/yyyy");
                        /*if (i == 0)
							p1.text = " 1.   " + highscoreliste[0].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[0].PlayerName.PadRight(6, ' ') + "   (" + highscoreliste[0].Module + ")\t" +
								highscoreliste[0].Zeit.PadRight(12, ' ') + "   " + highscoreliste[0].Datum.Date.ToString();
                        if (i == 1)
							p2.text = " 2.   " + highscoreliste[1].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[1].PlayerName.PadRight(6, ' ') + "   (" + highscoreliste[1].Module + ")\t" +
								highscoreliste[1].Zeit.PadRight(12, ' ') + "   " + highscoreliste[1].Datum.Date.ToString();
                        if (i == 2)
							p3.text = " 3.   " + highscoreliste[2].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[2].PlayerName.PadRight(6, ' ') + "   (" + highscoreliste[2].Module + ")\t" +
								highscoreliste[2].Zeit.PadRight(12, ' ') + "   " + highscoreliste[2].Datum.Date.ToString();
                        if (i == 3)
							p4.text = " 4.   " + highscoreliste[3].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[3].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[3].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[3].Zeit + "   " + highscoreliste[3].Datum.Date.ToString();
                        if (i == 4)
							p5.text = " 5.   " + highscoreliste[4].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[4].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[4].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[4].Zeit + "   " + highscoreliste[4].Datum.Date.ToString();
                        if (i == 5)
							p6.text = " 6.   " + highscoreliste[5].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[5].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[5].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[5].Zeit + "   " + highscoreliste[5].Datum.Date.ToString();
                        if (i == 6)
							p7.text = " 7.   " + highscoreliste[6].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[6].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[6].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[6].Zeit + "   " + highscoreliste[6].Datum.Date.ToString();
                        if (i == 7)
							p8.text = " 8.   " + highscoreliste[7].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[7].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[7].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[7].Zeit + "   " + highscoreliste[7].Datum.Date.ToString();
                        if (i == 8)
							p9.text = " 9.   " + highscoreliste[8].Score.ToString().PadLeft(6, '0') + "   " + highscoreliste[8].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[8].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[8].Zeit + "   " + highscoreliste[8].Datum.Date.ToString();
                        if (i == 9)
							p10.text = "10.   " + highscoreliste[9].Score.ToString().PadLeft(6, '0') +  "   " + highscoreliste[9].PlayerName.PadRight(20, ' ') + "   (" + highscoreliste[9].Module.PadRight(6, ' ') + ")   " +
                                       highscoreliste[9].Zeit + "   " + highscoreliste[9].Datum.Date.ToString();*/
                    }
                    //Liste ist leer, es gibt keien Einträge
                    
					for (; i < 10; i++) {
						str = (i < (10 - 1)) ? " " : "";
						ps [i].text = str + (i + 1).ToString () + ".\t" + 0.ToString ().PadLeft (6, '0') + "\t" +
						"-empty-".PadRight (20, ' ') + "\t" +
							"00:00:00".PadRight(12, ' ') + "\t" + defaultt.Date.ToString ("dd/MM/yyyy");
                        /*if (i == 0)
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
                            p10.text = "10.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();*/
                    }
                }
                
                else {
                    DateTime defaultt = new DateTime();
					string str;
					for (int i = 0; i < 10; ++i) {
						str = (i < (10 - 1)) ? " " : "";
						ps [i].text = str + (i + 1).ToString () + ".\t" + 0.ToString ().PadLeft (6, '0') + "\t" +
						"-empty-".PadRight (20, ' ') + "\t" +
							"00:00:00".PadRight(12, ' ') + "\t" + defaultt.Date.ToString ("dd/MM/yyyy");
					}
                    /*p1.text = " 1.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p2.text = " 2.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p3.text = " 3.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p4.text = " 4.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p5.text = " 5.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p6.text = " 6.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p7.text = " 7.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p8.text = " 8.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p9.text = " 9.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();
                    p10.text = "10.   " + 0.ToString().PadLeft(6, '0') + "   " + "-empty-".PadRight(20, ' ') + "   " + "00:00:00" + "   " + defaultt.Date.ToString();*/
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
