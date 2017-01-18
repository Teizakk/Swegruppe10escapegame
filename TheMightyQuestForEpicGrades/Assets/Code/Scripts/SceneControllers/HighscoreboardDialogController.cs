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
                //if (Persist.Load<HighscoreWrapper>(Name) != null) -> funktioniert schmeißt aber einen Fehler beim Laden, wenn keine Datei da ist und verursacht im Moment einen Debug.Break()
                {
                    var wrappedHighscore = Persist.Load<HighscoreWrapper>(Name);
                    highscoreliste = wrappedHighscore.List;
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
                    }
                    //Liste ist leer, es gibt keien Einträge
                    
					for (; i < 10; i++) {
						str = (i < (10 - 1)) ? " " : "";
						ps [i].text = str + (i + 1).ToString () + ".\t" + 0.ToString ().PadLeft (6, '0') + "\t" +
						"-empty-".PadRight (20, ' ') + "\t" +
							"00:00:00".PadRight(12, ' ') + "\t" + defaultt.Date.ToString ("dd/MM/yyyy");
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
