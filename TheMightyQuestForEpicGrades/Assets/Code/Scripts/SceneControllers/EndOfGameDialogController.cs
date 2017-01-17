using System.Collections.Generic;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class EndOfGameDialogController : MonoBehaviour {

        // Use this for initialization
        //Anzeigetext der Scene
        public Text loseOrWin;
		public Text timeText;
		public Text pointsText;

        HighscoreWrapper highscoreliste = new HighscoreWrapper();
        private static string Name = @"Highscores\highscores";

        //0= Main Menü
        //2= Insert Highscore End OF Game --> muss noch durch richtige ersetzt werden
        private int nextWindow = -1;

        void Awake () {
            //Wenn wir in dieser Szene sind muss der Player ebenfalls gekillt werden
            if (PlayerScript.GetInstance() != null) Destroy(PlayerScript.GetInstance().gameObject);

            //Wenn Spiel gewonnen
            if (Master.Instance().MyGameState.GameIsWon)
            {
                highscoreliste = Persist.Load<HighscoreWrapper>(Name);
                if (highscoreliste != null) { //Wenn highscores erfolgreich geladen
                    if (highscoreliste.List.Count == 0 || (Master.Instance().MyGameState.ScoreCurrent > highscoreliste.List[highscoreliste.List.Count - 1].Score)) {
                        //Wenn es noch keine Highscores gibt oder wenn der erreichte Score ein Highscore ist
                        nextWindow = 11;
                        loseOrWin.text = "Sie haben Gewonnen\nund einen Highscore erzielt!";
                    }
                    else
                    {
                        nextWindow = 0;
                        loseOrWin.text = "Sie haben Gewonnen";
                    }
                }
                else {
                    Debug.LogError("Laden der HighscoreListe fehlgeschlagen!");    
                    //Debug.Break();
                    nextWindow = 11;
                }
                Debug.Log("Jetzt müsste dort eine Meldung stehen");
            }
            else
            {
                loseOrWin.text = "Game over!\n Versuchs Nochmal!";
                nextWindow = 0;
            }

			timeText.text = System.String.Format("Zeit: {0:hh\\:mm\\:ss\\:ff}", Master.Instance().MyGameState.TimeTakenUntilNow);
			pointsText.text = "Punkte: " + Master.Instance ().MyGameState.ScoreCurrent.ToString ();
        }

        public void NextWindowOnClick()
        {
            if (nextWindow == 0) { //== zurück ins Hauptmenu
                Master.KILLME();
            }
            SceneManager.LoadScene(nextWindow);
        }

        #region Master-Link
        private void Start()
        {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}
