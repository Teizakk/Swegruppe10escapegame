using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Assets.Code.Manager;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Scripts.SceneControllers
{
    public class LoadGameController : MonoBehaviour
    {
        // Use this for initialization
        void Awake()
        {
            RefreshSaveGames();
        }

        public void LeaveToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #region Save&Load
        [Header("Save & Load Fields")]
        public ScrollRect LoadableGames;
        public GameObject SaveGamePrefab;
        private List<SavegameInfo> _listOfSavegameDescriptions = new List<SavegameInfo>();
        private List<GameObject> _saveGameLinks = new List<GameObject>();
        private int selectedSaveGameLink = -1;

        private void RefreshSaveGames()
        {
            Debug.Log("Speicherstände werden geladen");

            //alte löschen
            foreach (var saveGameLink in _saveGameLinks)
            {
                Destroy(saveGameLink.gameObject);
            }
            _saveGameLinks.Clear();

            //Alle verfügbaren SaveGames anhand deren Infos laden
            _listOfSavegameDescriptions = Master.Instance().MyGameState.GetAllGSIs();

            for (int index = 0; index < _listOfSavegameDescriptions.Count; index++)
            {

                var item = _listOfSavegameDescriptions[index];

                //Nur Savegames vom gleichen Spieler anzeigen
                //if (!item.PlayerName.Equals(Master.Instance().MyGameState.PlayerName)) continue;

                var saveGameDisplay = Instantiate(SaveGamePrefab, LoadableGames.content, false) as GameObject;
                DateTime timeOfSaving = new DateTime(item.TimeCode, DateTimeKind.Local);

                //var ci = CultureInfo.CurrentCulture; //oder "de-DE"
                var ci = new CultureInfo("de-DE");

                saveGameDisplay.GetComponentInChildren<Text>().text = item.ToString() +
                                                                      timeOfSaving.ToString("d", ci) + " | " + timeOfSaving.TimeOfDay.ToString().Split('.')[0];
                saveGameDisplay.GetComponent<DataHolderScript>().StoredValues.Add("IndexOfSGI", index);
                _saveGameLinks.Add(saveGameDisplay);
            }
			if (_saveGameLinks.Count > 0) {
				EventSystem.current.SetSelectedGameObject (_saveGameLinks [0]);
				_saveGameLinks [0].GetComponent<Button> ().onClick.Invoke ();
			}
        }

        public void SetSelectedSaveGameLink()
        {
            //unboxing
            selectedSaveGameLink = (int)EventSystem.current.currentSelectedGameObject.GetComponent<DataHolderScript>().StoredValues["IndexOfSGI"];
            Debug.Log("Das ausgewählte Savegame ist nun Nummer: " + selectedSaveGameLink);
        }

        public void LoadGame()
        {

            //Gucken welches 
            var fileToLoad = _listOfSavegameDescriptions[selectedSaveGameLink].FilenameOfGameStateSave;
            Debug.Log("Func Load Game von PMS fileToLoad: " + fileToLoad);
            //Laden starten
            Master.Instance().MyGameState.LoadGame(fileToLoad);
        }
        #endregion

        #region Master-Link
        private void Start()
        {
            Master.Instance().CurrentDialogController = this.gameObject;
        }
        #endregion
    }
}
