using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

    //private bool switches
    private bool gamePaused;
    private bool visible;
    private bool fadingIn;
    private bool fadingOut;

    #region Submenus
    public GameObject SaveMenu;
    public GameObject LoadMenu;
    public GameObject CloseWarning;
    #endregion

    //private CanvasGroup pauseMenuCanvasGroup;
    //private CanvasGroup saveMenuCanvasGroup;
    //private CanvasGroup loadMenuCanvasGroup;
    //private CanvasGroup closeWarningCanvasGroup;

    //Funktion um von außen zu fragen, ob das Spiel pausiert ist (für die Controls zb)
    public bool IsGamePaused() {
        return gamePaused;
    }

    // Use this for initialization
    void Start () {
	    gameObject.SetActive(true);
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        SaveMenu.SetActive(false);
        LoadMenu.SetActive(false);
        CloseWarning.SetActive(false);
	}
    private void StartFadingIn() {
        //TODO
    }

    private void StartFadingOut() {
        //TODO
    }
    // Update is called once per frame
    void Update () {
        //Wenn ESC gedrückt wurde und es noch nicht gerade erscheint oder verschwindet
	    if (Input.GetKeyDown(KeyCode.Escape) && !fadingIn && !fadingOut) {
            //Und das Pausenmenu nicht angezeigt wird
            if (!visible) { 
	            fadingIn = true;
                StartFadingIn();
	        }
            //Oder gerade angezeigt wird
            else if (visible) {
	            fadingOut = true;
	            StartFadingOut();
	        }
	    }
        //Wenn es gerade dabei ist zu erscheinen oder zu verschwinden
	    else if (fadingIn || fadingOut) {
            var mainCanvasGroup = GetComponent<CanvasGroup>();
            //Wenn wir gerade einblenden
            if (fadingIn) {
                //Wenn es die volle Sichtbarkeit erreicht hat
                if (mainCanvasGroup.alpha.Equals(1.0f)) { //... sonst Verlust der Genauigkeit? ist das hässlich...
                    visible = true;
                    mainCanvasGroup.interactable = true;
                    fadingIn = false;
                    gamePaused = true;
                    Debug.Log("Spiel ist nun pausiert");
                }
                //Wenn es noch nicht voll sichtbar ist
                else {
                    GetComponent<CanvasGroup>().alpha += 0.02f; //50 Ticks für volles alpha
                }
	        }
            //Wenn wir gerade ausblenden
	        else {
                //Wenn es komplett unsichtbar geworden ist
                if (mainCanvasGroup.alpha.Equals(0.0f)) {
                    visible = false;
                    mainCanvasGroup.interactable = false;
                    fadingOut = false;
                    gamePaused = false;
                    Debug.Log("Spiel wird fortgesetzt");
                }
                //Wenn es noch nicht voll sichtbar ist
                else {
                    GetComponent<CanvasGroup>().alpha -= 0.02f; //50 Ticks für komplett unsichtbar
                }
            }
        }
	}
}
