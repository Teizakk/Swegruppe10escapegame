using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ScriptQuestionDialog : MonoBehaviour
{
    #region UnityObjects
    public Text TimerText;
    public GameObject btnBild0, btnBild1, btnBild2, btnBild3;
    private GameObject[] btnBild;
    #endregion

    #region Properties
    private int tippsShowed;
    private int chosenAnswerIndex;
    private float startTime;
    private float timer;
    private string[] imagePaths;
    private static Question q;
    #endregion

    private int punkte;


    // Use this for initialization
    void Start()
    {
        // Werte initialisieren
        tippsShowed = 1;
        chosenAnswerIndex = 1;
        timer = 0.0f;
        btnBild = new GameObject[4]
        {
            btnBild0, btnBild1, btnBild2, btnBild3
        };

        // Frage laden
        LoadQuestion();
    }

    //Update is called once per frame
    void Update()
    {
        timer = Time.time - startTime;
        int mins = (int) timer/60;
        string minutes = (mins < 10 ? "0" : "") + mins;
        string seconds = ((timer % 60)).ToString("f0").PadLeft(2,'0');
        TimerText.text = minutes + ":" + seconds;
    }

    // beantwortet die Frage
    public void AnswerQuestion()
    {

        if (AnswerCorrect())
        {
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
        if (tippsShowed <= 3)
        {
            Text lblTipp = GameObject.Find("lblTipp" + tippsShowed).GetComponent<Text>();
            Text outTipp = GameObject.Find("outTipp" + tippsShowed).GetComponent<Text>();

            // Tipp anzeigen
            lblTipp.enabled = true;
            outTipp.enabled = true;

            Debug.Log("Hintstein eingesetzt!");
            Debug.Log("Tipp" + tippsShowed + " wurde angezeigt");

            // gameManager.Hintsteine--
            tippsShowed++;
        }
    }

    // zeigt Bild an
    public void ShowPicture(int index)
    {
        Debug.Log("Bild " + Path.GetFullPath(imagePaths[index]) + " anzeigen!");
    }

    // Frage und Antworten in den Dialog laden
    void LoadQuestion()
    {
        // Question = gameManager.questionController.getQuestion();
        q = new Question
        {
            QuestionText = "Was ist das Internet?",
            Difficulty = Difficulties.Easy,
            Level = 1,
            ImagePath = "Assets/Pictures/Beispielbild.jpg",
            Answers =
                new Dictionary<string, string>
                {
                        {"Ein Netz", "Assets/Pictures/Beispielbild.jpg"},
                        {"Nur physikalisch vorhanden", ""},
                        {"Ein Netz von Netzen", ""}
                },
            CorrectAnswer = 3,
            Hints = new List<string> { "inter", "connected", "networks" }
        };

        // Frage laden
        GameObject.Find("outQuestion").GetComponent<Text>().text = q.QuestionText;


        imagePaths = new string[q.Answers.Count + 1];

        // Bildpfad zu Frage
        imagePaths[0] = q.ImagePath;


        // Antworten laden
        int i = 1;
        foreach (var answer in q.Answers)
        {
            GameObject.Find("outAnswer" + i).GetComponent<Text>().text = answer.Key;
            imagePaths[i] = answer.Value;

            // Bild vorhanden?
            if (!string.IsNullOrEmpty(imagePaths[i - 1]))
            {
                // Button anzeigen
                btnBild[i - 1].SetActive(true);
            }
            i++;
        }

        // Tipps laden
        i = 1;
        foreach (var tipp in q.Hints)
        {
            GameObject.Find("outTipp" + i).GetComponent<Text>().text = tipp;
            i++;
        }

    }



    // leitet die Frage weiter
    public static Question ForwardQuestion()
    {
        return q;
    }


}

