using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Assets.Scripts;

public class PopupController : MonoBehaviour
{
    private ImagePopup _imagePopup;
    public Question usedQuestion;
    private void Awake()
    {
        Debug.Log("Awake on LauncherSkript called");
        _imagePopup = ImagePopup.Instance();
    }

    //Das ImagePopup mit Daten füttern um es zu testen
    public void TestImagePopupRaw()
    {
        string display_text = "Dies ist eine Frage";
        string image_path = "C:/Test/Beispielbild.png";
        string Hint1Text = "Ich bin Hinweis 1";
        string Hint2Text = "Ich bin Hinweis 2";
        string Hint3Text = "Ich bin Hinweis 3";

        _imagePopup.ConfigureAndShow(display_text, image_path, Hint1Text, Hint2Text, Hint3Text);
        //_imagePopup.ConfigureAndShow(display_text, image_path);
    }

    public void SetUpImagePopupQuestion(int hintsToShow)
    {
        switch (hintsToShow)
        {
            case 0:
                _imagePopup.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath);
                break;
            case 1:
                _imagePopup.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath, usedQuestion.Hints[0]);
                break;
            case 2:
                _imagePopup.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath, usedQuestion.Hints[0],usedQuestion.Hints[1]);
                break;
            case 3:
                _imagePopup.ConfigureAndShow(usedQuestion.QuestionText, usedQuestion.ImagePath, usedQuestion.Hints[0], usedQuestion.Hints[1], usedQuestion.Hints[2]);
                break;
        }
    }

    public void SetUpImagePopupAnswer(int hintsToShow) {
        switch (hintsToShow)
        {
            case 0:
                _imagePopup.ConfigureAndShow(usedQuestion.Answers[0].AnswerText, usedQuestion.Answers[0].ImagePath);
                break;
            case 1:
                _imagePopup.ConfigureAndShow(usedQuestion.Answers[0].AnswerText, usedQuestion.Answers[0].ImagePath, usedQuestion.Hints[0]);
                break;
            case 2:
                _imagePopup.ConfigureAndShow(usedQuestion.Answers[0].AnswerText, usedQuestion.Answers[0].ImagePath, usedQuestion.Hints[0], usedQuestion.Hints[1]);
                break;
            case 3:
                _imagePopup.ConfigureAndShow(usedQuestion.Answers[0].AnswerText, usedQuestion.Answers[0].ImagePath, usedQuestion.Hints[0], usedQuestion.Hints[1], usedQuestion.Hints[2]);
                break;
        }
    }
}