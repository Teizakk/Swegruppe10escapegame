using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class ImagePopup : MonoBehaviour {

    public Text q_or_a_text;
    public Text hint_1_text;
    public Text hint_2_text;
    public Text hint_3_text;
    public RawImage q_or_a_image;
    public Button back_button;
    public GameObject imagePopupObject;


    //modifiziertes Singleton-Pattern
    private static ImagePopup image_popup;

    public static ImagePopup Instance() {
        if (!image_popup) {
            image_popup = FindObjectOfType(typeof(ImagePopup)) as ImagePopup;
            if (!image_popup) {
                Debug.LogError("Es muss ein aktives ImagePopup Skript auf einem GameObject in der Scene existieren");
            }
        }
        return image_popup;
    }

    //Funktionen
    public void ConfigureAndShow (string q_or_a_text,
                           string q_or_a_image_path,
                           string hint_1_text = "",
                           string hint_2_text = "",
                           string hint_3_text = "") {

        //Fenster aktivieren
        imagePopupObject.SetActive(true);

        //back_button konfigurieren
        back_button.onClick.RemoveAllListeners();
        back_button.onClick.AddListener(closePopup); //müsste so gehen

        //Elemente setzen / anzeigen lassen
        this.q_or_a_text.gameObject.SetActive(false);
        this.q_or_a_text.text = q_or_a_text;
        this.q_or_a_text.gameObject.SetActive(true); //Könnte sein, dass man das nicht braucht
        back_button.gameObject.SetActive(true);

        //Bild konfigurieren
        if (File.Exists(q_or_a_image_path)) {
            byte[] bytes = File.ReadAllBytes(q_or_a_image_path);
            Texture2D new_texture = new Texture2D(15, 15);
            //new_texture.filterMode
            new_texture.LoadImage(bytes);
            this.q_or_a_image.texture = new_texture;
            q_or_a_image.gameObject.SetActive(true);
        }
        else {
            Debug.LogError("Die Bilddatei gibt es nicht.");
        }
       

        //Prüfen ob Hints mitgegeben wurden
        if (hint_1_text == "") {
            this.hint_1_text.text = "DONTSHOWME_1";
            this.hint_1_text.gameObject.SetActive(false);
        }
        else {
            this.hint_1_text.text = hint_1_text;
            this.hint_1_text.gameObject.SetActive(true);
        }

        if (hint_2_text == "") {
            this.hint_2_text.text = "DONTSHOWME_2";
            this.hint_2_text.gameObject.SetActive(false);
        }
        else {
            this.hint_2_text.text = hint_2_text;
            this.hint_2_text.gameObject.SetActive(true);
        }

        if (hint_3_text == "") {
            this.hint_3_text.text = "DONTSHOWME_3";
            this.hint_3_text.gameObject.SetActive(false);
        }
        else {
            this.hint_3_text.text = hint_3_text;
            this.hint_3_text.gameObject.SetActive(true);
        }
    }

    void closePopup() {
        imagePopupObject.SetActive(false);
    }

}
