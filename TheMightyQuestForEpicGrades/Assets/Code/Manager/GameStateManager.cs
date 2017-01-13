using System;
using System.Collections.Generic;
using System.Globalization;
using Assets.Code.GLOBALS;
using Assets.Code.Models;
using Assets.Code.Scripts.FeatureScripts;
using Assets.Code.Scripts.SceneControllers;
using Assets.Code.Scripts.UtilityScripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class GameStateManager : MonoBehaviour {
      
        //Quasi ein riesiges Backingfield
        private GameState GameStateObject;

        #region Audio
        private UnityEvent EnterNewGameOrSaveGame;

        private void ChangeTracksOnAudioManager() {
            Debug.Log("ChangeTracksOnAudioManager invoked");
            Master.Instance().MyAudio.ChangeTracks();
        }
        #endregion

        #region Savegame Handling
        public void SaveGame(string nameOfSavegameFile) {
            //PlayerPos auslesen
            Debug.Log(PlayerPosCurrent);
            Debug.Log(PlayerScript.GetInstance().GetPosition());
            PlayerPosCurrent = PlayerScript.GetInstance().GetPosition();
            Debug.Log(PlayerPosCurrent);

            //Fragen abspeichern
            GameStateObject.LevelState.Questions = Master.Instance().MyQuestion.GetQuestionField();

            var sgi = new SavegameInfo
            {
                ChosenDifficulty = this.DifficultyChosen,
                ChosenModule = this.ModuleChosen,
                CustomName = nameOfSavegameFile,
                PlayerName = this.PlayerName,
            };
            var timeCode = new TimeSpan(DateTime.Now.Ticks).Ticks;

            var gsoFileName = "SaveGames\\" + timeCode + "_gso"; //GameStateObject File
            var sgiFileName = "SaveGames\\" + timeCode + "_sgi"; //SaveGameInfo File

            //in sgi den Link auf sgo speichern & timecode für bessere Erreichbarkeit ablegen
            sgi.FilenameOfGameStateSave = gsoFileName;
            sgi.TimeCode = timeCode;


            Persist.Save(GameStateObject, gsoFileName);
            Persist.Save(sgi, sgiFileName);
        }

        public void LoadGame(string nameOfSavegameFile)
        {
            Debug.Log("MyGameState.Load(" + nameOfSavegameFile + ") aufgerufen");
            GameStateObject = Persist.Load<GameState>(nameOfSavegameFile);
            //Weiterleitung zu inbetweenLevels
            Debug.Log(GameStateObject != null ? "GSO ist nicht null" : "GSO ist null!");
            InBetweenLevelsDialogController._firstTimeUseOfScript = true; //Muss resettet werden da sonst falsche Sachen von dem Skript gemacht werden
            InBetweenLevelsDialogController._loadingASaveGame = true;
            PlayerScript._loadingASavedGame = true;
            Master.Instance().MyQuestion.SetQuestionFromSavegame(GameStateObject.LevelState.Questions);
            SceneManager.LoadScene("InBetweenLevels");
            EnterNewGameOrSaveGame.Invoke();
        }

        public void OverwriteGame(string nameOfSavegameFile)
        {
            //PlayerPos auslesen
            Debug.Log(PlayerPosCurrent);
            Debug.Log(PlayerScript.GetInstance().GetPosition());
            PlayerPosCurrent = PlayerScript.GetInstance().GetPosition();
            Debug.Log(PlayerPosCurrent);

            Persist.Save(GameStateObject, nameOfSavegameFile);
        }
        
        public List<SavegameInfo> GetAllGSIs() {
            var listOfSGIFileNames = Persist.GetAllSGIFileNames();
            var listOfSGIs = new List<SavegameInfo>();
            foreach (var sgiFileName in listOfSGIFileNames)
            {
                var sgiFile = Persist.Load<SavegameInfo>("SaveGames\\" + sgiFileName);
                if (sgiFile != null && sgiFile.GetType() == typeof(SavegameInfo)) {
                    listOfSGIs.Add(Persist.Load<SavegameInfo>("SaveGames\\" + sgiFileName));
                }
                else
                {
                    Debug.LogWarning("Fehler beim Laden von: " + sgiFileName);
                }
            }
            return listOfSGIs;
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
        //Konvertiert von Vector3_Serializable zu Vector3 und zurück
        public Vector3 PlayerPosCurrent {
            get
            {
                var x_value = GameStateObject.LevelState.PlayerPosition.x;
                var y_value = GameStateObject.LevelState.PlayerPosition.y;
                var z_value = GameStateObject.LevelState.PlayerPosition.z;
                return new Vector3(x_value, y_value, z_value);
            }
            set
            {
                GameStateObject.LevelState.PlayerPosition.x = value.x;
                GameStateObject.LevelState.PlayerPosition.y = value.y;
                GameStateObject.LevelState.PlayerPosition.z = value.z;
            }
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
            var rand = new System.Random((int)DateTime.Now.Ticks);
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
        public void SetUpNextLevel(bool changeValues, bool switchingLevels) {
            // A L T E S   K A P I T E L   B E E N D E N
            //Genutzte Daten merken
            Debug.Log("Benutztes Kapitel und Level sollen weggespeichert werden? = " + switchingLevels);
            if (switchingLevels) {
                GameStateObject.LevelState.ChaptersUsed.Add(GameStateObject.LevelState.Chapter);
                GameStateObject.LevelState.LevelsUsed.Add(GameStateObject.LevelState.Level);
                MoveToNextStage();
                ResetLivesDependingOnDifficulty(); 

                //Portalsteine zurücksetzen
                PortalStoneBlueIsInPossession = false;
                PortalStoneGreenIsInPossession = false;
                PortalStonePinkIsInPossession = false;

                PortalStoneBlueHasBeenUsed = false;
                PortalStoneGreenHasBeenUsed = false;
                PortalStonePinkHasBeenUsed = false;
                
                //HUD sollte sich daraus ergeben wenn er refreshed wird
            }
            //Wechsel/Übergang durchführen = neue Werte eintragen, wenn das spiel nicht geladen wird = changeValues == true
            if (changeValues) {
                LevelGetNewRandomly();
                ChapterGetNewRandomly();
            }

            Debug.Log("Folgendes Level ist jetzt das LevelInUse: " + LevelInUse);
            Debug.Log("Folgendes Kapitel ist jetzt das ChapterInUse: " + ChapterInUse);
            
            //Master.Instance().MyQuestion.LoadQuestionsByChapter(ChapterInUse);
            Master.Instance().MyLevel.LoadFromFile(LevelInUse);
            Debug.Assert(Master.Instance().MyLevel.GetLoadedLevelIndex() == Master.Instance().MyGameState.LevelInUse, "Geladenes Level stimmt nicht überein");
            //Übergang fertig
        }
        #endregion

        #region SteineZurVerfügung
        public int anzahlHinweissteine = 7;
        public int portalStein1 = 1;
        public int portalStein2 = 1;
        public int portalStein3 = 1;
        public System.Random rnd = new System.Random();
        #endregion


        #region Question-related
        public TimeSpan TimeTakenUntilNow { get { return GameStateObject.LevelState.Time; } }
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
            var rand = new System.Random((int)DateTime.Now.Ticks);
            var allChapters = Master.Instance().MyQuestion.GetAllChaptersInThisModule();
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

        GameObject opendChest = null;

        #region Chest
        //TODO chestToOpen parameter korrekt?
        // TODO wenn alle Hintsteine bekommen und alle Portalsteine was dann?
        public void OpenChest(GameObject chestToOpen) { //chestToOpen nicht zwingend erforderlich weil es eine bestimmte Instanz des ChestScripts sein sollte
            Debug.Log(chestToOpen.GetInstanceID());
            opendChest = chestToOpen;
            var questionDialogController = Master.Instance().CurrentDialogController.GetComponent<QuestionDialogController>();
            questionDialogController.ShowQuestion();
            if (questionDialogController.AnswerCorrect())
            {
                //chestToOpen.GetComponent<ChestScript>().Lock();
                Debug.Log("Frage korrekt beantwortet");
                /* Portalstein oder Hintstein bekommen */
                // wenn noch nicht alle Portalsteine bekommen
              
                //Dominiks Steine ziehen
                /*
                if (!GotAllPortalStones())
                {
                    // zufällig auswählen zwischen Portalstein und Hintstein
                    var randomizer = new System.Random((int) DateTime.Now.Ticks);
                    var rdmInt = randomizer.Next(0, 2);
                    switch (rdmInt)
                    {
                        case 0:
                            WinHintstone();
                            break;
                        case 1:
                            // PortalStein mit zufälliger Farbe vergeben
                            var color = (PortalColor) randomizer.Next((int) PortalColor.Blue, (int) PortalColor.Pink);
                            Debug.Log(color + " bekommen");
                            WinPortalStone(color);
                            break;
                    }
                }
                else
                {
                    // nur Hintsteine ausgeben
                    WinHintstone();
                }
                */
          }
        }
        //TODO triggert funktion in chestscript? 
        public void CloseChest(bool answerCorrect)
        {
            if (answerCorrect)
            {
                opendChest.GetComponentInChildren<Light>().enabled = true;
                opendChest.GetComponent<SphereCollider>().enabled = false;
            }
            else
            {
                opendChest.GetComponentInChildren<Light>().enabled = false;
                opendChest.GetComponent<SphereCollider>().enabled = true;
            }
            opendChest = null;
        }
        #endregion

        #region Lives
        public int LivesRemaining {
            get { return GameStateObject.LevelState.Lives; }
        }
        public void ResetLivesDependingOnDifficulty() {
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
                default:
                    throw new UnityException("Ungültiger Schwierigkeitsgrad angegeben...bricht ab....");
            }
            Debug.Log("Anzahl der Leben auf: " + GameStateObject.LevelState.Lives + " zurückgesetzt.\nDies sollte nie außerhalb des Levelwechsels passieren");
        }
        public void LoseOneLive() {
            if (GameStateObject.LevelState.Lives > 1)
            {
                GameStateObject.LevelState.Lives--;
                Master.Instance().CurrentDialogController.GetComponent<MainGameDialogController>().HUD.RemoveHeart();
                return;
            }
            if (GameStateObject.LevelState.Lives == 1) {
                GameStateObject.LevelState.Lives--;
                Debug.Log("Spiel ist nun game over");
                //TODO noch mehr zu tun?
                SetGameLost();
                SceneManager.LoadScene("EndOfGame");
                return;
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
                if (SceneManager.GetActiveScene().name == "MainGame")
                {
                    Master.Instance()
                        .CurrentDialogController.GetComponent<MainGameDialogController>()
                        .HUD.AddHintStone();
                }
                Debug.Log("Hintstein erhalten");
                return;
            }
            Debug.Log("HintStone nicht hinzugefügt, bereits Maximum erreicht");
        }
        public void UseHintStone() {
            if (GameStateObject.LevelState.HintStones > 0) {
                GameStateObject.LevelState.HintStones--;
                if (SceneManager.GetActiveScene().name == "MainGame")
                {
                    Master.Instance()
                        .CurrentDialogController.GetComponent<MainGameDialogController>()
                        .HUD.RemoveHintStone();
                }
                return;
            }
            throw new UnityException("HintStone kann nicht genutzt werden - keiner im Inventar");
        }
        #endregion

        #region Score
        public int ScoreCurrent {
            get { return GameStateObject.LevelState.Score; }
        }
        public int AddPointsToScore(TimeSpan timeUsed) {
            var amount = 1.0;
            switch (DifficultyChosen) {
                case Difficulties.Easy:
                    amount *= 1000;
                    break;
                case Difficulties.Medium:
                    amount *= 2000;
                    break;
                case Difficulties.Hard:
                    amount *= 5000;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("difficulty");
            }
            amount /= timeUsed.TotalSeconds;
            amount = Math.Round(amount);
            //amount -= amount % 10;
            var finalAmount = Convert.ToInt32(amount);
            GameStateObject.LevelState.Score += finalAmount;
            if (SceneManager.GetActiveScene().name == "MainGame")
            {
                Master.Instance()
                    .CurrentDialogController.GetComponent<MainGameDialogController>()
                    .HUD.UpdateScore(finalAmount);
            }
            return finalAmount;
        }
        #endregion
        
        #region Portals & Portalstones
        //Einfach nur links atm
        public bool PortalStoneBlueIsInPossession {
            get { return GameStateObject.LevelState.BluePortalStone.InPossession; }
            private set { GameStateObject.LevelState.BluePortalStone.InPossession = value; }
        }
        public bool PortalStoneBlueHasBeenUsed {
            get { return GameStateObject.LevelState.BluePortalStone.Used; }
            private set { GameStateObject.LevelState.BluePortalStone.Used = value; }
        }
        public bool PortalStoneGreenIsInPossession {
            get { return GameStateObject.LevelState.GreenPortalStone.InPossession; }
            private set { GameStateObject.LevelState.GreenPortalStone.InPossession = value; }
        }
        public bool PortalStoneGreenHasBeenUsed {
            get { return GameStateObject.LevelState.GreenPortalStone.Used; }
            private set { GameStateObject.LevelState.GreenPortalStone.Used = value; }
        }
        public bool PortalStonePinkIsInPossession {
            get { return GameStateObject.LevelState.PinkPortalStone.InPossession; }
            private set { GameStateObject.LevelState.PinkPortalStone.InPossession = value; }
        }
        public bool PortalStonePinkHasBeenUsed {
            get { return GameStateObject.LevelState.PinkPortalStone.Used; }
            private set { GameStateObject.LevelState.PinkPortalStone.Used = value; }
        }
        
        public bool HasUsedAllPortalStones() {
            if (GameStateObject.LevelState.PinkPortalStone.Used &&
                GameStateObject.LevelState.GreenPortalStone.Used &&
                GameStateObject.LevelState.BluePortalStone.Used) {
                return true;
            }
            return false;
        }

        public bool GotAllPortalStones()
        {
            return (PortalStonePinkIsInPossession || PortalStonePinkHasBeenUsed) && (PortalStoneBlueIsInPossession || PortalStoneBlueHasBeenUsed) && (PortalStoneGreenIsInPossession || PortalStoneGreenHasBeenUsed);
        }

        public void WinPortalStone(PortalColor color) {
            Debug.Log("Genutzter Portalstein: " + color);
            //Stein einsetzen und Portal damit aktivieren
            switch (color) {
                case PortalColor.Blue:
                    PortalStoneBlueIsInPossession = true;
                    break;
                case PortalColor.Green:
                    PortalStoneGreenIsInPossession = true;
                    break;
                case PortalColor.Pink:
                    PortalStonePinkIsInPossession = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("color", color, null);
            }
            if (SceneManager.GetActiveScene().name == "MainGame")
            {
                Master.Instance()
                    .CurrentDialogController.GetComponent<MainGameDialogController>()
                    .HUD.ReceivePortalStone(color);
                Debug.Log(color + " Portalstein erhalten");
            }
        }

        public void InsertPortalStone(GameObject portal, PortalColor color) {
            Debug.Log("Genutzter Portalstein: " + portal.GetInstanceID());
            //Stein einsetzen und Portal damit aktivieren
            switch (color) {
                case PortalColor.Blue:
                    PortalStoneBlueIsInPossession = false;
                    
                    PortalStoneBlueHasBeenUsed = true;
                    break;
                case PortalColor.Green:
                    PortalStoneGreenIsInPossession = false;
                    PortalStoneGreenHasBeenUsed = true;
                    break;
                case PortalColor.Pink:
                    PortalStonePinkIsInPossession = false;
                    PortalStonePinkHasBeenUsed = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("color", color, null);
            }
            if (SceneManager.GetActiveScene().name == "MainGame")
            {
                Master.Instance()
                    .CurrentDialogController.GetComponent<MainGameDialogController>()
                    .HUD.UsePortalStone(color);
            }
            portal.GetComponent<PortalScript>().Activate();
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

        public void SetupNewGame(string playerName, string moduleName, Difficulties difficulty) {
            //Mitgegebene GameOptions setzen
            PlayerName = playerName;
            Debug.Log("Gewählter Spielername = " + PlayerName);
            ModuleChosen = moduleName;
            DifficultyChosen = difficulty;

            //Abhängig davon Startwerte setzen
            //Stage ist durch Default-Wert gesetzt
            ResetLivesDependingOnDifficulty();
            //Hintstones ist durch Default-Wert gesetzt
            //TODO - braucht es hier noch was?
            EnterNewGameOrSaveGame.Invoke();
            Master.Instance().MyQuestion.LoadQuestionsFromFile(moduleName, difficulty);
        }
        
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

            EnterNewGameOrSaveGame = new UnityEvent();
            EnterNewGameOrSaveGame.AddListener(ChangeTracksOnAudioManager);
            
            _gameisFinished = false;
            _gameIsWon = false;
        }

        public void OnDestroy() {
            Debug.LogWarning("Hier wurden geraden alle Infos vom GameStateObject mit gelöscht... gewollt?");
        }
    }
}