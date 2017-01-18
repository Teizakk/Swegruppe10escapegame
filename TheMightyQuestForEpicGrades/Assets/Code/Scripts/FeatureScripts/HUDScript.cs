using System.Collections.Generic;
using System.ComponentModel;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts.FeatureScripts {
    public class HUDScript : MonoBehaviour {
        private int maxLives;
        private int numberOfHints;
        private int numberOfLives;

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

        public void SetUpHUD() {
            //GameStateManager zwischenspeichern für schnelleren Zugriff
            var gameStateLink = Master.Instance().MyGameState;
            
            //ANZEIGE DER LEBEN SETZEN
            numberOfLives = gameStateLink.LivesRemaining;
            Debug.Log("Anzahl Leben die gezeigt werden soll: " + numberOfLives);
            //kleiner missbrauch von numberOfLives
            foreach (var heart in hearts) {
                if (numberOfLives > 0) {
                    heart.enabled = true;
                }
                else heart.enabled = false;
                Debug.Log("Herz ist: " + heart.enabled);
                numberOfLives--;
            }
            //missbrauch rückgängig machen :^)
            numberOfLives = gameStateLink.LivesRemaining;

            //ANZEIGE DER HINWEISE SETZEN
            numberOfHints = gameStateLink.HintstonesRemaining;
            Debug.Log("Anzahl Hints die gezeigt werden soll: " + numberOfHints);
            //kleiner missbrauch von numberOfHints
            foreach (var hint in hints) {
                if (numberOfHints > 0) {
                    hint.enabled = true;
                }
                else hint.enabled = false;
                numberOfHints--;
            }
            //missbrauch rückgängig machen :^)
            numberOfHints = gameStateLink.HintstonesRemaining;

            //Anzeige der Portalsteine setzen
            bluePortalStone.enabled = gameStateLink.PortalStoneBlueIsInPossession || gameStateLink.PortalStoneBlueHasBeenUsed;
            greenPortalStone.enabled = gameStateLink.PortalStoneGreenIsInPossession || gameStateLink.PortalStoneGreenHasBeenUsed;
            pinkPortalStone.enabled = gameStateLink.PortalStonePinkIsInPossession || gameStateLink.PortalStonePinkHasBeenUsed;

            //Anzeige ob PortalSteine genutzt wurden
            if (gameStateLink.PortalStoneBlueHasBeenUsed) UsePortalStone(PortalColor.Blue);
            if (gameStateLink.PortalStoneGreenHasBeenUsed) UsePortalStone(PortalColor.Green);
            if (gameStateLink.PortalStonePinkHasBeenUsed) UsePortalStone(PortalColor.Pink);

            //Points auf aktuellen Wert setzen
            points = gameStateLink.ScoreCurrent;
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
            if (numberOfHints == 0) {
                Debug.LogError("Fehler im einem Controller - Anzahl Hints würde unter 0 fallen!");
                return;
            }
            //muss hier vorher gemacht werden, oder halt nachher mit [numberOfHints]...
            hints[numberOfHints - 1].enabled = false;
            numberOfHints--;
        }

        public void RemoveHeart() {
            if (numberOfLives == 1)
                Debug.Log(
                    "Laut HUD gibt es nun keine Leben/Herzen mehr... jetzt sollte das Game eigentlich GAME OVER sein...");
            if (numberOfLives == 0) {
                Debug.LogError(
                    "Fehler im einem Controller - Spiel sollte längst GAME OVER sein, Anzahl an Leben in HUD == 0!");
                Debug.Break();
                return;
            }
            hearts[numberOfLives - 1].enabled = false;
            numberOfLives--;
        }

       public void ReceivePortalStone(PortalColor color) {
           switch (color) {
               case PortalColor.Blue:
                   bluePortalStone.enabled = true;
                   bluePortalStone.color = new Color(1, 1, 1, 1);
                   break;
               case PortalColor.Green:
                   greenPortalStone.enabled = true;
                   greenPortalStone.color = new Color(1, 1, 1, 1);
                   break;
               case PortalColor.Pink:
                   pinkPortalStone.enabled = true;
                   pinkPortalStone.color = new Color(1, 1, 1, 1);
                   break;
               default:
                   Debug.LogError("Falscher Farbcode für Portalstein übergeben! Gültig: [B/b]lue, [G/g]reen, [P/p]ink");
                   break;
           }
       }

        public void UsePortalStone(PortalColor color) {
            switch (color) {
                case PortalColor.Blue:
                    bluePortalStone.color = new Color(1, 1, 1, 0.25f); //nur alpha runter setzen
                    break;
                case PortalColor.Green:
                    greenPortalStone.color = new Color(1, 1, 1, 0.25f); //nur alpha runter setzen
                    break;
                case PortalColor.Pink:
                    pinkPortalStone.color = new Color(1, 1, 1, 0.25f); //nur alpha runter setzen
                    break;
                default:
                    Debug.LogError("Falscher Farbcode für Portalstein übergeben! Gültig: [B/b]lue, [G/g]reen, [P/p]ink");
                    break;
            }
        }

        public void UpdateScore(int pointsToAdd) {
            points += pointsToAdd;
            scoreDisplay.text = points.ToString().PadLeft(6, '0');
        }

        #region DEVTOOLS
        public void DEVResetHeartsDisplay(){
            //ANZEIGE DER LEBEN SETZEN
            numberOfLives = Master.Instance().MyGameState.LivesRemaining;
            Debug.Log("Anzahl Leben die gezeigt werden soll: " + numberOfLives);
            //kleiner missbrauch von numberOfLives
            foreach (var heart in hearts)
            {
                if (numberOfLives > 0)
                {
                    heart.enabled = true;
                }
                else heart.enabled = false;
                Debug.Log("Herz ist: " + heart.enabled);
                numberOfLives--;
            }
            //missbrauch rückgängig machen :^)
            numberOfLives = Master.Instance().MyGameState.LivesRemaining;
        }
        #endregion

        // Update is called once per frame
        private void Update() {
            //Anzeige Logik hier hin verschieben? oder ist das gut so, weil es Abfragen spart?
        }
    }
}