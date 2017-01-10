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

        public void InsertHighscore()
        {
            if (SceneManager.GetActiveScene().name == "InsertHighscoreEndOFGame")
            {
                try
                {
                    if(Persist.Load<List<Highscore>>(Name)==null)
                    {
                        highscoreliste = new List<Highscore>();
                    }
                    else
                    {
                        highscoreliste = Persist.Load<List<Highscore>>(Name);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    highscoreliste = new List<Highscore>();
                }

                Highscore neu = new Highscore();

                neu.PlayerName = playerName.text;
                neu.Zeit = zeit.text;
                string scorString = score.text;
                int scoreAsInt = -1;
                int.TryParse(scorString, out scoreAsInt);
                neu.Score = scoreAsInt;


                highscoreliste.Add(neu);
                //highscoreliste.OrderBy(x => Convert.ToInt32(x.Score)).Take(10).ToList();
                highscoreliste = Order(highscoreliste);
                Persist.Save<List<Highscore>>(highscoreliste, Name);

                LeaveWithoutInsert(); //Macht das gleiche
            }
        }

        public void LeaveWithoutInsert() {
            Master.KILLME();
            SceneManager.LoadScene("MainMenu");
        }

        public List<Highscore> Order(List<Highscore> unsortiert)
        {
            List<Highscore> neu = new List<Highscore>();
            
            if(unsortiert.Count>10)
            {
                int niedrigste = 0;
                for (int i = 1; i < 11; i++)
                {
                    if(unsortiert[i].Score <= unsortiert[niedrigste].Score)
                    {
                        niedrigste = i;
                    }
                }

                for(int i=0;i<10;i++)
                {
                    if(i!=niedrigste)
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

        List<Highscore> insertionSort(List<Highscore> input)
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

        private void OnDestroy() {
            //Darf hier nicht mehr passieren, weil es Master jetzt nicht mehr gibt.
            //Master.Instance().CurrentDialogController = null;
        }
        #endregion
    }
}
