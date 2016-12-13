using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScriptQuestionDialog : MonoBehaviour
{
    #region UnityObjects
    public Text TimerText;
    public List<GameObject> btnPictures;

    #endregion

    #region Properties
    private static ScriptQuestionDialog questionDialog;
    private int tippsShowed;
    private int chosenAnswerIndex;
    private DateTime startTime;
    private DateTime endTime;
    private TimeSpan usedTime;
    private String[] imagePaths;
    private static Question q;
    #endregion

    private int punkte;


    // Use this for initialization
    void Start()
    {
        // Werte initialisieren
        tippsShowed = 0;
        chosenAnswerIndex = 1;
        startTime = DateTime.Now;

        // Frage laden
        LoadQuestion();
    }

    //Update is called once per frame
    void Update()
    {
        usedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime.Ticks);
        TimerText.text = String.Format("{0:hh\\:mm\\:ss\\:ff}", usedTime);
        //int mins = (int) timer/60;
        //string minutes = (mins < 10 ? "0" : "") + mins;
        //string seconds = ((timer % 60)).ToString("f0").PadLeft(2,'0');
        //TimerText.text = minutes + ":" + seconds;
    }


    public static ScriptQuestionDialog Instance()
    {
        if (!questionDialog)
        {
            questionDialog = FindObjectOfType(typeof(ScriptQuestionDialog)) as ScriptQuestionDialog;
            if (!questionDialog)
                Debug.LogError("Es muss ein aktives ScriptQuestionDialog Skript auf einem GameObject in der Scene existieren");
        }
        return questionDialog;
    }

    // beantwortet die Frage
    public void AnswerQuestion()
    {
        if (AnswerCorrect())
        {
            endTime = DateTime.Now;
            usedTime = new TimeSpan(endTime.Ticks - startTime.Ticks);
            Debug.Log("Frage korrekt beantwortet!");
            // Popup anzeigen


            // Punkte addieren
            switch (q.Difficulty)
            {
                case Difficulties.Easy:
                    punkte = 1;
                    break;
                case Difficulties.Medium:
                    punkte = 2;
                    break;
                case Difficulties.Hard:
                    punkte = 3;
                    break;
            }
            // gameManager.portalsteine++;
            // gameManager.punkte += punkte;
            Debug.Log("Portalstein erhalten!");
            Debug.Log(punkte + " Punkt(e) erhalten!");
        }
        else
        {
            Debug.Log("Frage wurde falsch beantwortet!");
            Debug.Log("Leben - 1");
            // gameManager.Leben--
        }

        // in beiden Fällen
        Debug.Log("Zeit zur Gesamtzeit addieren");

        // gameManager.Time += timer;
        // Close
    }

    // ändert die ausgewählte Antwort
    public void AnswerChanged(int index)
    {
        chosenAnswerIndex = index;
        Debug.Log(chosenAnswerIndex + ". Antwort ausgewählt!");
    }

    // überprüft, ob die Antwort richtig war
    bool AnswerCorrect()
    {
        return (chosenAnswerIndex == q.CorrectAnswer);
    }


    // Tipp anzeigen
    public void ShowTipp()
    {
        if (tippsShowed < 3
            /* && gameManager.Hintsteine>0*/)
        {
            tippsShowed++;
            // gameManager.Hintsteine--

            Text lblTipp = GameObject.Find("lblTipp" + tippsShowed).GetComponent<Text>();
            Text outTipp = GameObject.Find("outTipp" + tippsShowed).GetComponent<Text>();
            // Tipp anzeigen
            lblTipp.enabled = true;
            outTipp.enabled = true;

            Debug.Log("Hintstein eingesetzt!");
            Debug.Log("Tipp" + tippsShowed + " wurde angezeigt");
        }
    }

    // zeigt Bild (ImagePopup) an
    public void ShowPicture(int index)
    {
        // TODO : connect to ImagePopup
        var popupController = GetComponent<PopupController>();
        popupController.usedQuestion = q;
        if (index > 0)
        {
            popupController.SetUpImagePopupAnswer(tippsShowed,index);
        }
        else
        {
            popupController.SetUpImagePopupQuestion(tippsShowed);
        }
        Debug.Log("Bild '" + imagePaths[index] + "' anzeigen!");
    }

    // Frage und Antworten in den Dialog laden
    void LoadQuestion()
    {
        //q = QuestionController.GetInstance().GetQuestionNotInUse();
        q = new Question
        {
            QuestionText = "Was ist das Internet?",
            Difficulty = Difficulties.Easy,
            Level = 1,
            ImagePath = Path.GetFullPath("Assets/ImagePopupV1/Data/Beispielbild.png"),
            Answers =
                new List<Question.Answer>()
                {
                        new Question.Answer()
                        {
                            AnswerText = "Ein Netz",
                            ImagePath = ""
                        },
                        new Question.Answer()
                        {
                            AnswerText = "Nur physikalisch vorhanden",
                            ImagePath = "Assets/ImagePopupV1/Data/Bild2.png"
                        },
                        new Question.Answer()
                        {
                            AnswerText = "Ein Netz von Netzen",
                            ImagePath = ""
                        },
                },
            CorrectAnswer = 3,
            Hints = new List<string> { "inter", "connected", "networks" }
        };

        // Frage laden
        GameObject.Find("outQuestion").GetComponent<Text>().text = q.QuestionText;


        imagePaths = new string[q.Answers.Count + 1];

        // Bildpfad zu Frage
        imagePaths[0] = q.ImagePath;
        
        // Bild vorhanden?
        if (!string.IsNullOrEmpty(imagePaths[0]) && File.Exists(Path.GetFullPath(imagePaths[0])))
        {
            // Button anzeigen
            btnPictures[0].SetActive(true);
        }

        // Antworten laden
        int i = 1;
        Text outAnswer;
        foreach (var answer in q.Answers)
        {
            outAnswer = GameObject.Find("outAnswer" + i).GetComponent<Text>();
            outAnswer.text = answer.AnswerText;
            imagePaths[i] = answer.ImagePath;
            // Bild vorhanden?
            if (!string.IsNullOrEmpty(imagePaths[i]) && File.Exists(Path.GetFullPath(imagePaths[i])))
            {
                // Button anzeigen
                btnPictures[i].SetActive(true);
            }

            i++;
        }

        // Tipps laden
        i = 1;
        Text outTipp;
        foreach (var tipp in q.Hints)
        {
            outTipp = GameObject.Find("outTipp" + i).GetComponent<Text>();
            outTipp.text = tipp;
            i++;
        }

    }



    // leitet die Frage weiter
    public static Question ForwardQuestion()
    {
        return q;
    }


}

