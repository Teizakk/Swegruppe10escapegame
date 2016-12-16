using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    enum difficulties {
       Easy = 1, Medium = 2, Hard = 3
    }

    #region HUDIcons
    public RawImage[] hearts = new RawImage[7];
    public RawImage[] hints = new RawImage[5];
    public RawImage bluePortalStone;
    public RawImage greenPortalStone;
    public RawImage pinkPortalStone;
    #endregion

    #region HUDPoints

    private int points;
    public Text scoreDisplay;
    #endregion


    private int maxLives;
    private int numberOfLives;
    private int numberOfHints;

    //hier muss man sich einigen wie man den Schwierigkeitsgrade übergibt
    //entweder easy = 1, medium = 2....
    //oder direkt durch die Anzahl der Leben...
    //aktulle Lösung ist über 1,2,3 mit "Übersetzung" durch das enum
    //TODO je nach Art des Szenenwechselns muss hier dann noch der Score übergeben werden
    public void FirstSetUpHUD(int difficulty) {
        //Je nach Schwierigkeit die Werte setzen
        switch (difficulty) {
            case (int)difficulties.Easy:
                numberOfLives = maxLives = 7;
                break;
            case (int)difficulties.Medium:
                numberOfLives = maxLives = 5;
                break;
            case (int)difficulties.Hard:
                numberOfLives = maxLives = 3;
                break;
            default:
                throw new InvalidEnumArgumentException("Ungültiger Schwierigkeitsgrad angegeben...bricht ab....");
        }
        //kleiner missbrauch von numberOfLives
        foreach (var heart in hearts) {
            if (numberOfLives > 0) {
                heart.enabled = true;
            }
            else {
                heart.enabled = false;
            }
            numberOfLives--;
        }
        //missbrauch rückgängig machen :^)
        numberOfLives = maxLives;

        numberOfHints = 0;

        //Anzeige der Portalsteine ausschalten
        bluePortalStone.enabled = false;
        greenPortalStone.enabled = false;
        pinkPortalStone.enabled = false;

        //Points auf 0 setzen
        points = 0;
        scoreDisplay.text = points.ToString().PadLeft(6, '0');
    }

    public void AddHintStone() {
        if (numberOfHints > 4) {
            Debug.LogError("Fehler im einem Controller - maximale Anzahl an Hints würde überschritten werden!");
            return;
        }
        numberOfHints++;
        hints[numberOfHints - 1].enabled = true; //sollte ja so klappen, nich?
    }

    public void RemoveHintStone() {
        if (numberOfHints == 0)
        {
            Debug.LogError("Fehler im einem Controller - Anzahl Hints würde unter 0 fallen!");
            return;
        }
        //muss hier vorher gemacht werden, oder halt nachher mit [numberOfHints]...
        hints[numberOfHints - 1].enabled = false;
        numberOfHints--;
    }

    public void RemoveHeart() {
        if (numberOfLives == 1) {
            Debug.Log("Laut HUD gibt es nun keine Leben/Herzen mehr... jetzt sollte das Game eigentlich GAME OVER sein...");
        }
        if (numberOfLives == 0) {
            Debug.LogError("Fehler im einem Controller - Spiel sollte längst GAME OVER sein, Anzahl an Leben in HUD == 0!");
            return;
        }
        hearts[numberOfLives - 1].enabled = false;
        numberOfLives--;
    }

    //machen wir das so? Gedacht für Levelwechsel - falls nicht gerbraucht -> Löschen TODO
    public void ResetHearts() {
        //gleicher numberOfLives Missbrauch wie im SetUp
        numberOfLives = maxLives;
        foreach (var heart in hearts) {
            if (numberOfLives > 0) {
                heart.enabled = true;
            }
            else {
                heart.enabled = false;
            }
            numberOfLives--;
        }
        numberOfLives = maxLives;
        //Hints bleiben erhalten
    }

    public void ReceivePortalStone(string colorOfPortalStone) {
        if (colorOfPortalStone.Equals("Blue") || colorOfPortalStone.Equals("blue")) {
            bluePortalStone.enabled = true;
            bluePortalStone.color = new Color(1, 1, 1, 1);
        }
        else if (colorOfPortalStone.Equals("Green") || colorOfPortalStone.Equals("green")) {
            greenPortalStone.enabled = true;
            greenPortalStone.color = new Color(1, 1, 1, 1);
        }
        else if (colorOfPortalStone.Equals("Pink") || colorOfPortalStone.Equals("pink")) {
            pinkPortalStone.enabled = true;
            pinkPortalStone.color = new Color(1, 1, 1, 1); 
        }
        else {
            Debug.LogError("Falscher Farbcode für Portalstein übergeben! Gültig: [B/b]lue, [G/g]reen, [P/p]ink");
        }
    }

    public void UsePortalStone(string colorOfPortalStone) {
        if (colorOfPortalStone.Equals("Blue") || colorOfPortalStone.Equals("blue")) {
            bluePortalStone.color = new Color(1,1,1,0.4f); //nur alpha runter setzen
        }
        else if (colorOfPortalStone.Equals("Green") || colorOfPortalStone.Equals("green")) {
            greenPortalStone.color = new Color(1, 1, 1, 0.4f); //nur alpha runter setzen
        }
        else if (colorOfPortalStone.Equals("Pink") || colorOfPortalStone.Equals("pink")) {
            pinkPortalStone.color = new Color(1, 1, 1, 0.4f); //nur alpha runter setzen
        }
        else {
            Debug.LogError("Falscher Farbcode für Portalstein übergeben! Gültig: [B/b]lue, [G/g]reen, [P/p]ink");
        }
    }

    public void UpdateScore(int pointsToAdd) {
        points += pointsToAdd;
        scoreDisplay.text = points.ToString().PadLeft(6, '0');

    }
    // Use this for initialization
    void Start () {
        //Werte initialisieren (-1 um sicher zu stellen, dass firstSetUp aufgerufen wurde)
        numberOfLives = -1;
        numberOfHints = -1;
        
        //Einmal den HUD "leeren"
        foreach (var hint in hints) {
           //hint.GetComponent<Renderer>().enabled = false;
            hint.enabled = false;
        }
        foreach (var heart in hearts) {
            //heart.GetComponent<Renderer>().enabled = false;
            heart.enabled = false;
        }
        scoreDisplay.text = points.ToString().PadLeft(6, '0');
        bluePortalStone.enabled = false;
        greenPortalStone.enabled = false;
        pinkPortalStone.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
	    //Anzeige Logik hier hin verschieben? oder ist das gut so, weil es Abfragen spart?
        

	}
}
