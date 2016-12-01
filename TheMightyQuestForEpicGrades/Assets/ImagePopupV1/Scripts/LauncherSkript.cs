using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LauncherSkript : MonoBehaviour
{
    private ImagePopup image_popup;
    private void Awake()
    {
        Debug.Log("Awake on LauncherSkript called");
        image_popup = ImagePopup.Instance();
    }

    //Das ImagePopup mit Daten füttern um es zu testen
    public void TestImagePopup()
    {
        string display_text = "Dies ist eine Frage";
        string image_path = "C:/Test/Beispielbild.png";
        string hint_1_text = "Ich bin Hinweis 1";
        string hint_2_text = "Ich bin Hinweis 2";
        string hint_3_text = "Ich bin Hinweis 3";

        image_popup.ConfigureAndShow(display_text, image_path, hint_1_text, hint_2_text);
    }
}
