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

        List<Highscore> highscoreliste = new List<Highscore>();
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
                highscoreliste = Persist.Load<List<Highscore>>(Name);
                if (highscoreliste != null) { //Wenn highscores erfolgreich geladen
                    if (highscoreliste.Count == 0 || (Master.Instance().MyGameState.ScoreCurrent > highscoreliste[highscoreliste.Count - 1].Score)) {
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
