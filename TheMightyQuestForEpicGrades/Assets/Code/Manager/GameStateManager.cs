using System;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;

namespace Assets.Code.Manager {
    public class GameStateManager : MonoBehaviour {
      
        //Quasi ein riesiges Backingfield
        private GameState GameStateObject;

        #region Savegame Handling

        public void SaveGame(string nameOfSavegameFile) {
            TimeSpan TimeCode = new TimeSpan(DateTime.Now.Ticks);
            Persist.Save(GameStateObject, "SaveGames\\" + nameOfSavegameFile + "_" + TimeCode.TotalSeconds);
        }

        public void LoadGame(string nameOfSavegameFile) {
            var listOfSavegames = Persist.GetSavedGames();
            foreach (var item in listOfSavegames)
            {
                Debug.Log(item);
            }
            //TODO
        }
        #endregion

        #region Difficulty
        public Difficulties DifficultyChosen {
            get { return GameStateObject.GameOptions.Difficulty; }
            set { GameStateObject.GameOptions.Difficulty = value; }
        }
        #endregion

        #region Modul
        public string ModuleUsed {
            get { return GameStateObject.GameOptions.Module; }
            set {
                if (string.IsNullOrEmpty(value)) {
                    Debug.LogError("Das Modul muss gesetzt sein! (leerer String oder null übergeben)");
                    return;
                }
                GameStateObject.GameOptions.Module = value;
            }
        }
        #endregion

        #region Player
        public string PlayerName {
            get { return GameStateObject.GameOptions.PlayerName;}
            set {
                GameStateObject.GameOptions.PlayerName = string.IsNullOrEmpty(value) ? "Anonymous" : value;
            }
        }

        public Vector3 PlayerPosCurrent {
            get { return GameStateObject.LevelState.PlayerPosition; }
            set { GameStateObject.LevelState.PlayerPosition = value; }
        }
        #endregion

        #region Level
        public int LevelUsed {
            get { return GameStateObject.LevelState.Level; }
        }

        //TODO setter für LevelUsed - soll das von einer anderen Funktion gemacht werden?

        public int KapitelCurrent {
            get { return GameStateObject.LevelState.Kapitel; }
        }
        public void MoveToNextChapter() {
            if (GameStateObject.LevelState.Kapitel < 3) {
                GameStateObject.LevelState.Kapitel++;
                Debug.Log("Wir befinden uns nun in Kapitel: " + GameStateObject.LevelState.Kapitel);
                return;
            }
            Debug.Log("Kapitelanzahl würde über 3 gehen - gewollt?");
        }
        #endregion

        #region Question-related
        public DateTime TimeUsed { get { return GameStateObject.LevelState.Time; } }
        public void AddTime(TimeSpan timeTaken) {
            if (timeTaken.Ticks > 0) {
                GameStateObject.LevelState.Time += timeTaken;
                return;
            }
            Debug.LogError("Hinzuzufügende Zeit darf nicht kleiner oder gleich 0 sein!");
        }
        #endregion

        //TODO CHEST FUNCTIONS

        #region Lives
        public int LivesRemaining {
            get { return GameStateObject.LevelState.Lives; }
        }
        public void ResetLives() {
            //In Abhängigkeit vom Schwierigkeitsgrad die Leben zurücksetzen
            switch (GameStateObject.GameOptions.Difficulty) {
                case Difficulties.Easy:
                    GameStateObject.LevelState.Lives = 7;
                    break;
                case Difficulties.Medium:
                    GameStateObject.LevelState.Lives = 5;
                    break;
                case Difficulties.Hard:
                    GameStateObject.LevelState.Lives = 3;
                    break;
            }
            Debug.Log("Anzahl der Leben auf: " + GameStateObject.LevelState.Lives + " zurückgesetzt.\nDies sollte nie außerhalb des Levelwechsels passieren"); //TODO Durch Cheatmode?
        }
        public void LoseOneLive() {
            if (GameStateObject.LevelState.Lives > 1) {
                GameStateObject.LevelState.Lives--;
                return;
            }
            if (GameStateObject.LevelState.Lives == 1) {
                GameStateObject.LevelState.Lives--;
                Debug.Log("Spiel ist nun game over");
                //TODO weitere Aktionen einleiten?
            }
            throw new UnityException("Leben kann nicht entfernt werden - Spieler sollte längst GameOver sein!!!");
        }
        #endregion

        #region HintStones
        public int HintstonesRemaining {
            get { return GameStateObject.LevelState.HintStones; }
        }
        public void WinHintstone() {
            if (GameStateObject.LevelState.HintStones < 5) {
                GameStateObject.LevelState.HintStones++;
                return;
            }
            Debug.Log("HintStone nicht hinzugefügt, bereits Maximum erreicht");
        }
        public void UseHintStone() {
            if (GameStateObject.LevelState.HintStones > 0) {
                GameStateObject.LevelState.HintStones--;
                return;
            }
            throw new UnityException("HintStone kann nicht genutzt werden - keiner im Inventar");
        }
        #endregion

        #region Score
        public int ScoreCurrent {
            get { return GameStateObject.LevelState.Score; }
        }
        public void AddPointsToScore(int amount) {
            if (amount > 0) {
                GameStateObject.LevelState.Score += amount;
                return;
            }
            throw new UnityException("Zu addierende Punktezahl muss positiv und größer 0 sein!");
        }
        #endregion

        //TODO PORTALSTONE FUNCTIONS

        #region Cheatmode
        public bool CheatmodeActive {
            get { return GameStateObject.LevelState.Cheatmode; }
            set { GameStateObject.LevelState.Cheatmode = value; }
        }
        #endregion

        public void Awake() {
            GameStateObject = new GameState();
        }

        public void OnDestroy() {
            Debug.LogWarning("Hier wurde geraden alle Infos vom GameStateObject mit gelöscht... gewollt?");
        }
    }
}