﻿using System;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            set {
                if (SceneManager.GetActiveScene().name != "NewGame") {
                    throw new UnityException("Der Schwierigkeitsgrad darf nur in NewGame geändert werden!");
                }
                GameStateObject.GameOptions.Difficulty = value;
            }
        }
        #endregion

        #region Modul
        public string ModuleChosen {
            get { return GameStateObject.GameOptions.Module; }
            set {
                if (SceneManager.GetActiveScene().name != "NewGame") {
                    throw new UnityException("Der Schwierigkeitsgrad darf nur in NewGame geändert werden!");
                }
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
        public int LevelInUse { //Hier geht es um die Level-Datei
            get { return GameStateObject.LevelState.Level; }
        }
        
        private void LevelGetNewRandomly() {
            if (SceneManager.GetActiveScene().name != "InBetweenLevels") {
                //Nur dann darf auf die Grundlegenden Leveldaten zugegriffen werden.
                throw new UnityException("Die grundlegenden Leveldaten dürfen zu diesem Zeitpunkt nicht verändert werden");
            }
            //Ja diese Funktion könnte einfacher geschrieben werden, wenn man davon ausgeht, dass
            //die Levelindizes immer nur aufsteigend in dem Ordner zu finden sind, das crashed allerdings
            //dann, wenn man mal nachträglich z.B. Level_3.txt entfernt..
            var rand = new System.Random();
            var allLevels = Master.Instance().MyLevel.GetAllUseableLevelIndices();
            var checkedLevels = new bool[allLevels.Length];

            for (var i = 0; i < checkedLevels.Length; i++) {
                int indexToCheck;
                do {
                    indexToCheck = rand.Next(allLevels.Length);
                } while (checkedLevels[indexToCheck] != false); //heißt wenn dort schon geguckt wurde nochmal probieren
                //Levelindex heraussuchen
                var levelToTest = allLevels[indexToCheck];
                //Gucken ob dieser in dem aktuellen Spieldurchlauf bereits benutzt wurde
                if (!GameStateObject.LevelState.LevelsUsed.Contains(levelToTest)) {
                    //Ungenutztes Level gefunden -> als aktuelles Level in GameStateObject setzen
                    GameStateObject.LevelState.Level = levelToTest;
                    return;
                }
                //Ansonsten zu geprüften hinzufügen und von vorne anfangen
                checkedLevels[indexToCheck] = true;
            }
            throw new UnityException("Keine unbenutzten Level gefunden!");
        }

        public int StageCurrent {
            get { return GameStateObject.LevelState.Stage; }
        }

        public void MoveToNextStage() {
            if (GameStateObject.LevelState.Stage < 3)
            {
                GameStateObject.LevelState.Stage++;
                Debug.Log("Wir befinden uns nun in Stage: " + GameStateObject.LevelState.Stage);
                return;
            }
            Debug.Log("Stageanzahl würde über 3 gehen - gewollt ? ");
        }
        
        //Übergang einleiten
        public void SetUpNextLevel(bool startOfGame = false) {
            // A L T E S   K A P I T E L   B E E N D E N
            //Genutzte Daten merken
            if (!startOfGame) {
                GameStateObject.LevelState.ChaptersUsed.Add(GameStateObject.LevelState.Chapter);
                GameStateObject.LevelState.LevelsUsed.Add(GameStateObject.LevelState.Level);
                MoveToNextStage();
            }
            //Wechsel/Übergang durchführen = neue Werte eintragen
            LevelGetNewRandomly();
            ChapterGetNewRandomly();

            Debug.Log("Folgendes Level ist jetzt das LevelInUse: " + LevelInUse);
            Debug.Log("Folgendes Kapitel ist jetzt das ChapterInUse: " + ChapterInUse);

            //Master.Instance().MyQuestion.LoadQuestionsByChapter(ChapterInUse);
            Master.Instance().MyLevel.LoadFromFile(LevelInUse);
            Debug.Assert(Master.Instance().MyLevel.GetLoadedLevelIndex() == Master.Instance().MyGameState.LevelInUse, "Geladenes Level stimmt nicht überein");
            //Übergang fertig
        }
        #endregion

        #region Question-related
        public DateTime TimeTakenUntilNow { get { return GameStateObject.LevelState.Time; } }
        public void AddTime(TimeSpan timeTaken) {
            if (timeTaken.Ticks > 0) {
                GameStateObject.LevelState.Time += timeTaken;
                return;
            }
            Debug.LogError("Hinzuzufügende Zeit darf nicht kleiner oder gleich 0 sein!");
        }

        public string ChapterInUse {
            get { return GameStateObject.LevelState.Chapter; }
        }

        private void ChapterGetNewRandomly() {
            if (SceneManager.GetActiveScene().name != "InBetweenLevels") {
                //Nur dann darf auf die Grundlegenden Leveldaten zugegriffen werden.
                throw new UnityException("Die grundlegenden Leveldaten dürfen zu diesem Zeitpunkt nicht verändert werden");
            }
            var rand = new System.Random();
            var allChapters = Master.Instance().MyQuestion.GetAllChapters();
            var checkedChapters = new bool[allChapters.Length];

            for (var i = 0; i < checkedChapters.Length; i++) {
                int indexToCheck;
                do {
                    indexToCheck= rand.Next(allChapters.Length);
                } while (checkedChapters[indexToCheck] != false); //heißt wenn dort schon geguckt wurde nochmal probieren
                //Kapitelnamen heraussuchen
                var chapterToTest = allChapters[indexToCheck];
                //Gucken ob dieser in dem aktuellen Spieldurchlauf bereits benutzt wurde
                if (!GameStateObject.LevelState.ChaptersUsed.Contains(chapterToTest)) {
                    //Ungenutztes Kapitel gefunden -> Kapitel in GameStateObject setzen
                    GameStateObject.LevelState.Chapter = chapterToTest;
                    return;
                }
                //Ansonsten zu geprüften hinzufügen und von vorne anfangen
                checkedChapters[indexToCheck] = true;
            }
            throw new UnityException("Keine unbenutzten Kapitel gefunden!");
        }

        #endregion

        #region Chest
        //TODO CHEST FUNCTIONS
        #endregion

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
        
        #region Portalstones
        //TODO PORTALSTONE FUNCTIONS
        public bool HasUsedAllPortalStones() {
            //if (GameStateObject.LevelState.PinkPortalStone.Used &&
            //    GameStateObject.LevelState.GreenPortalStone.Used &&
            //    GameStateObject.LevelState.BluePortalStone.Used) {
            //    return true;
            //}
            //return false;
            return true;
        }
        #endregion

        #region Cheatmode
        public bool CheatmodeActive {
            get { return GameStateObject.LevelState.Cheatmode; }
            set { GameStateObject.LevelState.Cheatmode = value; }
        }
        #endregion

        #region Game-Session-related
        private bool _gameisFinished;
        private bool _gameIsWon; // aka 3tes Level erfolgreich abgeschlossen
        public bool GameIsWon { get {
            return _gameisFinished && _gameIsWon;
        } }
        public bool GameIsFinished { get { return _gameisFinished; } }

        public void SetGameWon() {
            if (SceneManager.GetActiveScene().name != "MainGame")
                throw new UnityException(
                    "Das Spiel darf nicht außerhalb der MainGame Szene gewonnen oder verloren werden");
            _gameisFinished = true;
            _gameIsWon = true;
        }

        public void SetGameLost() {
            if (SceneManager.GetActiveScene().name != "MainGame")
                throw new UnityException(
                    "Das Spiel darf nicht außerhalb der MainGame Szene gewonnen oder verloren werden");
            _gameisFinished = true;
            _gameIsWon = false; //Um sicher zu gehen eigentlich bereits so
        }
        #endregion

        public void Awake() {
            GameStateObject = new GameState();
            //Standardmäßig geblockt - muss im ersten InBetweenLevels ent-locked werden
            _gameisFinished = false;
            _gameIsWon = false;
        }

        public void OnDestroy() {
            Debug.LogWarning("Hier wurden geraden alle Infos vom GameStateObject mit gelöscht... gewollt?");
        }
    }
}