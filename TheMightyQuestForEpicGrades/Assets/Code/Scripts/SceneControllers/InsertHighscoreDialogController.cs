using System;
using System.Collections.Generic;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class InsertHighscoreDialogController : MonoBehaviour {

        List<Highscore> highscoreliste = new List<Highscore>();
        private static string Name = @"Highscores\highscores"; //"/Speicher/Highscores/highscoreliste";

        public Text score;
        public Text zeit;
        public Text playerName;
        public Text oldPlayerName;

        void Awake()
        {
            oldPlayerName.text = Master.Instance().MyGameState.PlayerName;
            score.text = Master.Instance().MyGameState.ScoreCurrent.ToString();
            zeit.text = String.Format("{0:hh\\:mm\\:ss\\:ff}", Master.Instance().MyGameState.TimeTakenUntilNow);
            //zeit.text = String.Format("{0:hh\\:mm\\:ss}", Master.Instance().MyGameState.TimeTakenUntilNow);
        }

        public void NextWindowOnClick()
        {
            if (SceneManager.GetActiveScene().name == "InsertHighscoreEndOfGame" ||
                SceneManager.GetActiveScene().name == "InsertHighscoreEndOFGame")
            {
                try
                {
                    if (Persist.Load<HighscoreWrapper>(Name) == null)
                    {
                        highscoreliste = new List<Highscore>();
                    }
                    else {
                        //entpacken
                        var wrappendHighscore = Persist.Load<HighscoreWrapper>(Name);
                        highscoreliste = wrappendHighscore.List;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    highscoreliste = new List<Highscore>();
                }
                string time = Master.Instance().MyGameState.TimeTakenUntilNow.Hours.ToString().PadLeft(2, '0') + ":" + Master.Instance().MyGameState.TimeTakenUntilNow.Minutes.ToString().PadLeft(2, '0') + ":" + Master.Instance().MyGameState.TimeTakenUntilNow.Seconds.ToString().PadLeft(2, '0');

                //Highscore schreiben
                Highscore neu = new Highscore()
                {
                    PlayerName = string.IsNullOrEmpty(playerName.text) ? oldPlayerName.text : playerName.text,
                    Zeit = time,
                    Score = Master.Instance().MyGameState.ScoreCurrent,
                    Datum = DateTime.Now
                };
                //Highscore hinzufügen
                highscoreliste.Add(neu);
                //highscoreliste.OrderBy(x => Convert.ToInt32(x.Score)).Take(10).ToList();
                //sortieren
                highscoreliste = Order(highscoreliste);
                //verpacken
                var hsw = new HighscoreWrapper();
                hsw.List = highscoreliste;
                Persist.Save<HighscoreWrapper>(hsw, Name);

                LeaveWithoutInsert(); //Macht das gleiche
            }
        }

        public void LeaveWithoutInsert()
        {
            Master.KILLME();
            SceneManager.LoadScene("MainMenu");
        }

        public List<Highscore> Order(List<Highscore> unsortiert)
        {
            var neu = new List<Highscore>();

            if (unsortiert.Count > 10)
            {
                int niedrigste = 0;
                for (int i = 1; i < 11; i++)
                {
                    if (unsortiert[i].Score <= unsortiert[niedrigste].Score)
                    {
                        niedrigste = i;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (i != niedrigste)
                    {
                        neu.Add(unsortiert[i]);
                    }
                }
            }
            else
            {
                neu = unsortiert;
            }

            neu = insertionSort(neu);

            return neu;
        }

        private List<Highscore> insertionSort(List<Highscore> input)
        {
            int i = 1;
            while (i < input.Count)
            {
                Highscore tmp = input[i];
                int j = i;

                while (j > 0 && tmp.Score > input[j - 1].Score)
                {
                    input[j] = input[j - 1];
                    j--;
                }
                input[j] = tmp;

                i++;
            }
            return input;
        }

        
            

        #region Master-Link
        private void Start() {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}
