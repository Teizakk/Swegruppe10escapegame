using System.Collections.Generic;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGameDialogController : MonoBehaviour {

    // Use this for initialization
    //Anzeigetext der Scene
    public Text loseOrWin;
    int score;

    List<Highscore> highscoreliste = new List<Highscore>();
    private static string Name = @"Highscores\highscores";


    //0= Main Menü
    //2= Insert Highscore End OF Game --> muss noch durch richtige ersetzt werden
    int nextWindow = -1;

    void Awake () {

        if(Master.Instance().MyGameState.GameFinished==true)
        {
            if (Persist.Load<List<Highscore>>(Name) != null)
            {
                highscoreliste = Persist.Load<List<Highscore>>(Name);
                if(score>highscoreliste[highscoreliste.Count-1].Score)
                {
                    nextWindow = 1;
                    loseOrWin.text = "Sie haben Gewonnen und einen Highscore erzielt!";
                }
            }
            else
            {
                nextWindow = 1;
                loseOrWin.text = "Sie haben Gewonnen";
            }
        }
        else
        {
            loseOrWin.text = "Game over!\n Versuchs Nochmal!";
            nextWindow = 0;
        }
    }

    public void NextWindowOnClick()
    {
        SceneManager.LoadScene(nextWindow);
    }

    #region Master-Link
    private void Start()
    {
        Master.Instance().CurrentDialogController = this.gameObject;
    }

    private void OnDestroy()
    {
        Master.Instance().CurrentDialogController = null;
    }
    #endregion
}
