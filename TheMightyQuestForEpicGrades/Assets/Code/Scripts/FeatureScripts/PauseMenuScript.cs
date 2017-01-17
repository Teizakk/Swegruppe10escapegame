using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.SceneControllers;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.FeatureScripts {
    public class PauseMenuScript : MonoBehaviour {
        private const float _FADE_FACTOR = 0.04f;

        #region variablen

        //private bool switches
        private bool _gamePaused;
        private bool _visible;
        private bool _fadingIn;
        private bool _fadingOut;
        private bool _inSubWindow;

        [Header("Untermenüs")]
        public GameObject SaveMenu;
        public GameObject LoadMenu;
        public GameObject LoadWarning;
        public GameObject CloseWarning;
        public GameObject QuitWarning;

        private Button SpielFortsetzenButton;

        private CanvasGroup _pauseMenuCanvasGroup;
        private CanvasGroup _saveMenuCanvasGroup;
        private CanvasGroup _loadMenuCanvasGroup;
        private CanvasGroup _loadWarningCanvasGroup;
        private CanvasGroup _closeWarningCanvasGroup;
        private CanvasGroup _quitWarningCanvasGroup;

        #endregion

        #region Save&Load
        [Header("Save & Load Fields")]
        public InputField SaveGameName;
        public ScrollRect LoadableGames;
        public ScrollRect OverwriteableGames;
        public GameObject SaveGamePrefab;
        private List<SavegameInfo> _listOfSavegameDescriptions = new List<SavegameInfo>();
        private List<GameObject> _saveGameLinks = new List<GameObject>();
        private int selectedSaveGameLink = -1;

        public void SaveGame() {
            //Weiterleitung
            //Konventionen für Namensgebung auf die Seite schreiben? -> egal so wie im Moment gespeichert
            Master.Instance().MyGameState.SaveGame(SaveGameName.text);
            HideSaveMenu();
        }

        public void OverwriteGame()
        {
            var saveGameToOverwrite = _listOfSavegameDescriptions[selectedSaveGameLink];
            Debug.Log("Overwrite Game von PMS fileToOverwrite: " + saveGameToOverwrite.FilenameOfGameStateSave);
            //Laden starten
            Master.Instance().MyGameState.OverwriteGame(saveGameToOverwrite);
            HideSaveMenu();
        }

        private void RefreshSaveGames(bool SaveGame = false) {
            Debug.Log("Speicherstände werden geladen");

            //alte löschen
            foreach (var saveGameLink in _saveGameLinks) {
                Destroy(saveGameLink.gameObject);
            }
            _saveGameLinks.Clear();

            //Alle verfügbaren SaveGames anhand deren Infos laden
            _listOfSavegameDescriptions = Master.Instance().MyGameState.GetAllGSIs();

            for (int index = 0; index < _listOfSavegameDescriptions.Count; index++) {

                var item = _listOfSavegameDescriptions[index];

                //Nur Savegames vom gleichen Spieler anzeigen
                //if (!item.PlayerName.Equals(Master.Instance().MyGameState.PlayerName)) continue;
                GameObject saveGameDisplay;
                // wenn im LoadMenu
                if (!SaveGame)
                {
                    saveGameDisplay = Instantiate(SaveGamePrefab, LoadableGames.content, false) as GameObject;
                }
                else
                {
                    // wenn im Save Menu
                    // wegen überlade funktion
                    saveGameDisplay = Instantiate(SaveGamePrefab, OverwriteableGames.content, false) as GameObject;
                }

                DateTime timeOfSaving = new DateTime(item.TimeCode, DateTimeKind.Local);

                //var ci = CultureInfo.CurrentCulture; //oder "de-DE"
                var ci = new CultureInfo("de-DE");

                saveGameDisplay.GetComponentInChildren<Text>().text = item.ToString() +
                                                                      timeOfSaving.ToString("d", ci) + "\t\t" + timeOfSaving.TimeOfDay.ToString().Split('.')[0];
                saveGameDisplay.GetComponent<DataHolderScript>().StoredValues.Add("IndexOfSGI", index);
                _saveGameLinks.Add(saveGameDisplay);
            }
            EventSystem.current.SetSelectedGameObject(_saveGameLinks[0]);
            _saveGameLinks[0].GetComponent<Button>().onClick.Invoke();
        }

        public void SetSelectedSaveGameLink() {
            //unboxing
            selectedSaveGameLink = (int)EventSystem.current.currentSelectedGameObject.GetComponent<DataHolderScript>().StoredValues["IndexOfSGI"];
            Debug.Log("Das ausgewählte Savegame ist nun Nummer: " + selectedSaveGameLink);
        }

        public void LoadGame() {
            //Gucken welches 
            var fileToLoad = _listOfSavegameDescriptions[selectedSaveGameLink].FilenameOfGameStateSave;
            Debug.Log("Func Load Game von PMS fileToLoad: " + fileToLoad);
            //Laden starten
            Master.Instance().MyGameState.LoadGame(fileToLoad);
        }
        #endregion

        #region Sonstige Funktionen
        //Funktion um von außen zu fragen, ob das Spiel pausiert ist (für die Controls zb)
        public bool IsGamePaused() {
            return _gamePaused;
        }

        public void BlockUnblockPauseMenu() {
            //wenn im subwindow dann kann nicht geöffnet werden
            _inSubWindow = !_inSubWindow;
        }
        //"Nachrichten"-Funktion an PlayerScript
        public void blockAndUnblockMovement() {
            PlayerScript.GetInstance().SwitchControlBlock();
        }

        public void ContinueGame() {
            _fadingOut = true;
            //Trick um das gedrückte Aussehen des Buttons zu beheben, wenn man erneut ins Pausenmenü geht (und in der Zwischenzeit nichts "gedrückt" wurde)
            SpielFortsetzenButton.enabled = false;
            SpielFortsetzenButton.enabled = true;
        }

        #endregion

        #region show&hide submenus

        //Speichermenü anzeigen / ausblenden
        public void ShowSaveMenu() {
            SaveMenu.SetActive(true);
            _saveMenuCanvasGroup.alpha = 1.0f;
            _saveMenuCanvasGroup.interactable = true;
            _inSubWindow = true;
            RefreshSaveGames(true);
        }

        public void HideSaveMenu() {
            _saveMenuCanvasGroup.alpha = 0.0f;
            _saveMenuCanvasGroup.interactable = false;
            _inSubWindow = false;
            SaveMenu.SetActive(false);
        }

        //Lademenü anzeigen / ausblenden
        public void ShowLoadMenu() {
            LoadMenu.SetActive(true);
            _loadMenuCanvasGroup.alpha = 1.0f;
            _loadMenuCanvasGroup.interactable = true;
            _inSubWindow = true;
            RefreshSaveGames();
        }

        public void HideLoadMenu() {
            _loadMenuCanvasGroup.alpha = 0.0f;
            _loadMenuCanvasGroup.interactable = false;
            _inSubWindow = false;
            LoadMenu.SetActive(false);
        }

        //Schließwarnung anzeigen / ausblenden
        public void ShowCloseWarning() {
            CloseWarning.SetActive(true);
            _closeWarningCanvasGroup.alpha = 1.0f;
            _closeWarningCanvasGroup.interactable = true;
            _inSubWindow = true;
        }

        public void HideCloseWarning() {
            _closeWarningCanvasGroup.alpha = 0.0f;
            _closeWarningCanvasGroup.interactable = false;
            _inSubWindow = false;
            CloseWarning.SetActive(false);
        }

        //Ladewarnung
        public void ShowLoadWarning() {
            LoadWarning.SetActive(true);
            _loadWarningCanvasGroup.alpha = 1.0f;
            _loadWarningCanvasGroup.interactable = true;
            //_inSubWindow = true;
        }

        public void HideLoadWarning() {
            _loadWarningCanvasGroup.alpha = 0.0f;
            _loadWarningCanvasGroup.interactable = false;
            //_inSubWindow = true;
            LoadWarning.SetActive(false);
        }

        //Ladewarnung
        public void ShowQuitWarning() {
            QuitWarning.SetActive(true);
            _quitWarningCanvasGroup.alpha = 1.0f;
            _quitWarningCanvasGroup.interactable = true;
            //_inSubWindow = true;
        }

        public void HideQuitWarning() {
            _quitWarningCanvasGroup.alpha = 0.0f;
            _quitWarningCanvasGroup.interactable = false;
            //_inSubWindow = true;
            QuitWarning.SetActive(false);
        }
        #endregion

        #region Unity Call-Backs

        // Use this for initialization
        private void Start() {
            //Das Pausenmenü selbst
            gameObject.SetActive(true);
            _pauseMenuCanvasGroup = gameObject.GetComponent<CanvasGroup>();
            _pauseMenuCanvasGroup.alpha = 0;
            _pauseMenuCanvasGroup.interactable = false;
            //Das Speichernmenü
            SaveMenu.SetActive(false);
            _saveMenuCanvasGroup = SaveMenu.GetComponent<CanvasGroup>();
            _saveMenuCanvasGroup.alpha = 0;
            _saveMenuCanvasGroup.interactable = false;
            //Das Lademenü
            LoadMenu.SetActive(false);
            _loadMenuCanvasGroup = LoadMenu.GetComponent<CanvasGroup>();
            _loadMenuCanvasGroup.alpha = 0;
            _loadMenuCanvasGroup.interactable = false;
            //Die Schließen-Warnung
            CloseWarning.SetActive(false);
            _closeWarningCanvasGroup = CloseWarning.GetComponent<CanvasGroup>();
            _closeWarningCanvasGroup.alpha = 0;
            _closeWarningCanvasGroup.interactable = false;
            //Die Lade-Warnung
            LoadWarning.SetActive(false);
            _loadWarningCanvasGroup = LoadWarning.GetComponent<CanvasGroup>();
            _loadWarningCanvasGroup.alpha = 0;
            _loadWarningCanvasGroup.interactable = false;
            //Die Beenden-Warnung
            QuitWarning.SetActive(false);
            _quitWarningCanvasGroup = LoadWarning.GetComponent<CanvasGroup>();
            _quitWarningCanvasGroup.alpha = 0;
            _quitWarningCanvasGroup.interactable = false;
            //Der eine Button den man für den kleinen Trick in der ContinueGame Funktion braucht
            var buttonList = FindObjectsOfType<Button>().ToList();
            SpielFortsetzenButton = buttonList.FindLast(button => button.name.Equals("ContinueGameButton"));
            //evtl. Fehleranfällig
            if (SpielFortsetzenButton == null)
                Debug.LogError(
                    "Der Spielfortsetzen Button wurde nicht unter dem Namen 'ContinueGameButton' gefunden - falsch benannt?");
            buttonList.Clear();
        }

        // Update is called once per frame
        private void Update() {
            //Wenn ESC gedrückt wurde und es noch nicht gerade erscheint oder verschwindet
            if (Input.GetKeyDown(KeyCode.Escape) && !_fadingIn && !_fadingOut && !_inSubWindow) {
                //Und das Pausenmenu nicht angezeigt wird
                if (!_visible) _fadingIn = true;
                //Oder gerade angezeigt wird
                else if (_visible) _fadingOut = true;
            }
            //Wenn es gerade dabei ist zu erscheinen oder zu verschwinden
            else if (_fadingIn || _fadingOut) {
                var mainCanvasGroup = GetComponent<CanvasGroup>();
                //Wenn wir gerade einblenden
                if (_fadingIn) {
                    //Wenn es die volle Sichtbarkeit erreicht hat
                    if (mainCanvasGroup.alpha.Equals(1.0f)) {
                        //... sonst Verlust der Genauigkeit? ist das hässlich...
                        _visible = true;
                        mainCanvasGroup.interactable = true; //lässt Button beim reinfaden verblassen
                        _fadingIn = false;
                        _gamePaused = true;
                        blockAndUnblockMovement();
                        Debug.Log("Spiel ist nun pausiert");
                    }
                    //Wenn es noch nicht voll sichtbar ist
                    else {
                        GetComponent<CanvasGroup>().alpha += _FADE_FACTOR;
                    }
                }
                //Wenn wir gerade ausblenden
                else {
                    //Wenn es komplett unsichtbar geworden ist
                    if (mainCanvasGroup.alpha.Equals(0.0f)) {
                        _visible = false;
                        mainCanvasGroup.interactable = false;
                        _fadingOut = false;
                        _gamePaused = false;
                        blockAndUnblockMovement();
                        Debug.Log("Spiel wird fortgesetzt");
                    }
                    //Wenn es noch nicht voll sichtbar ist
                    else {
                        GetComponent<CanvasGroup>().alpha -= _FADE_FACTOR;
                    }
                }
            }
        }

        #endregion
    }
}