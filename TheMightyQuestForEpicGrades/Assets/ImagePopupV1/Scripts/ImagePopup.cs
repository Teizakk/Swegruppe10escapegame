using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopup : MonoBehaviour
{
    private static ImagePopup imagePopup;
    public Text QOrAText;
    public RawImage QOrAImage;
    public Text Hint1Text;
    public Text Hint2Text;
    public Text Hint3Text;
    public Button BackButton;

    public GameObject imagePopupObject;

    //modifiziertes Singleton-Pattern
    public static ImagePopup Instance()
    {
        if (!imagePopup)
        {
            imagePopup = FindObjectOfType(typeof(ImagePopup)) as ImagePopup;
            if (!imagePopup)
                Debug.LogError("Es muss ein aktives ImagePopup Skript auf einem GameObject in der Scene existieren");
        }
        return imagePopup;
    }

    //Funktionen
    public void ConfigureAndShow(string q_or_a_text,
        string q_or_a_image_path,
        string hint_1_text = "",
        string hint_2_text = "",
        string hint_3_text = "")
    {
        //Fenster aktivieren
        imagePopupObject.SetActive(true);

        //BackButton konfigurieren
        BackButton.onClick.RemoveAllListeners();
        BackButton.onClick.AddListener(closePopup); //müsste so gehen

        //Elemente setzen / anzeigen lassen
        this.QOrAText.gameObject.SetActive(false);
        this.QOrAText.text = q_or_a_text;
        this.QOrAText.gameObject.SetActive(true); //Könnte sein, dass man das nicht braucht
        BackButton.gameObject.SetActive(true);

        //Bild konfigurieren
        if (File.Exists(q_or_a_image_path))
        {
            var bytes = File.ReadAllBytes(q_or_a_image_path);
            var new_texture = new Texture2D(15, 15);
            //new_texture.filterMode
            new_texture.LoadImage(bytes);
            QOrAImage.texture = new_texture;
            QOrAImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Die Bilddatei gibt es nicht.");
        }


        //Prüfen ob Hints mitgegeben wurden
        if (hint_1_text == "")
        {
            this.Hint1Text.text = "DONTSHOWME_1";
            this.Hint1Text.gameObject.SetActive(false);
        }
        else
        {
            this.Hint1Text.text = "Hinweis 1: " + hint_1_text;
            this.Hint1Text.gameObject.SetActive(true);
        }

        if (hint_2_text == "")
        {
            this.Hint2Text.text = "DONTSHOWME_2";
            this.Hint2Text.gameObject.SetActive(false);
        }
        else
        {
            this.Hint2Text.text = "Hinweis 2: " + hint_2_text;
            this.Hint2Text.gameObject.SetActive(true);
        }

        if (hint_3_text == "")
        {
            this.Hint3Text.text = "DONTSHOWME_3";
            this.Hint3Text.gameObject.SetActive(false);
        }
        else
        {
            this.Hint3Text.text = "Hinweis 3: " + hint_3_text;
            this.Hint3Text.gameObject.SetActive(true);
        }
    }

    private void closePopup()
    {
        imagePopupObject.SetActive(false);
    }
}