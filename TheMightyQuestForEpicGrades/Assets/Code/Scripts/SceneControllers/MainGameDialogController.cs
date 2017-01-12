using System;
using Assets.Code.GLOBALS;
using Assets.Code.Manager;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers {
    public class MainGameDialogController : MonoBehaviour {

        [Header("BoardBuilder Prefab")]
        public BoardBuilderScript BoardBuilder;
        [Header("Player Prefab")]
        public PlayerScript Player;
        [Header("Tooltip Prompt")]
        public GameObject TooltipPanel;
        [Header("HUD")]
        public HUDScript HUD;


        // Use this for initialization
        private void Awake() {
            DEVPanel.SetActive(Master.IsDEVMode());
            if (PlayerScript.GetInstance() == null)
                Instantiate(Player);

            //BoardBuilder initialisieren und Szene bauen lassen
            Master.Instance().MyLevel.BoardBuilder_TMP = Instantiate(BoardBuilder);
            Master.Instance().MyLevel.BoardBuilder_TMP.SetupScene();
            //Danach wird er vom PlayerScript wenn er damit fertig ist gekillt, weil er nicht mehr gebraucht wird.
            
            //Tooltip per default auf nicht sichtbar
            TooltipPanel.SetActive(false);

            //HUD aufsetzen (darf nur aus gespeichterten Werten lesen - sonst probs beim Laden)
            HUD.gameObject.SetActive(false); //Vorsichts Maßnahme, damit man das ändern der Werte auf keinen Fall sieht.
            HUD.SetUpHUD();
            HUD.gameObject.SetActive(true);
        }

        private void ActivateTooltip(string message) {
            TooltipPanel.GetComponentInChildren<Text>().text = "Taste 'E': " + message;
            TooltipPanel.SetActive(true);
        }
        private void DeactivateTooltip() {
            TooltipPanel.SetActive(false);
        }

        #region Für das Pausenmenü
        //Im Endeffekt nur eine Weiterleitung
        public void LeaveToMainMenu() {
            //Player weg
            if (PlayerScript.GetInstance() != null) DestroyImmediate(PlayerScript.GetInstance().gameObject);
            //Ganzer Master weg 
            Master.KILLME();
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitApplication() {
            Application.Quit();
        }
        #endregion

        #region Master-Link
        private void Start() {
            Master.Instance().CurrentDialogController = this.gameObject;
        }

        private void OnDestroy() {
            //Master.Instance().CurrentDialogController = null; //nervt immer mit Fehlermeldungen
        }
        #endregion

        #region DEVONLY
        [Header("DEVONLY PANEL")] public GameObject DEVPanel;
        public void DEVGetHint() {
            Master.Instance().MyGameState.WinHintstone();
            Master.Instance().CurrentDialogController.GetComponent<MainGameDialogController>().HUD.AddHintStone();
        }
        public void DEVResetHearts() {
            Master.Instance().MyGameState.ResetLivesDependingOnDifficulty();
            Master.Instance().CurrentDialogController.GetComponent<MainGameDialogController>().HUD.DEVResetHeartsDisplay();
        }

        public void DEVGetAllPortalStones()
        {
            Master.Instance().MyGameState.WinPortalStone(PortalColor.Blue);
            Master.Instance().MyGameState.WinPortalStone(PortalColor.Green);
            Master.Instance().MyGameState.WinPortalStone(PortalColor.Pink);
        }

        public void LateUpdate() {
            DEVPanel.SetActive(Master.IsDEVMode());
        }

        #endregion
    }
}
