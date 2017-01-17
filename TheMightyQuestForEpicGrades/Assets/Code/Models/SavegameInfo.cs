using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using Assets.Code.GLOBALS;

namespace Assets.Code.Models {
    //Ist quasi nur eine Verpackung um Informationen über SaveGames ausgeben zu können
    //ohne den kompletten GameState zu Laden und gleichzeitig um dsa Problem ungültiger
    //Zeichen in Dateinamen etc zu verhindern.
    [Serializable]
    public class SavegameInfo
    {
        #region Dateipfad zum gespeicherten Spielstand

        public string FilenameOfGameStateSave;
        public string FilenameOfSaveGameInfo;

        #endregion
        #region "Meta-Tags"
        public string CustomName;
        public string PlayerName;
        public Difficulties ChosenDifficulty;
        public string ChosenModule;
        public long TimeCode;
        #endregion
        public override string ToString() {
             return string.Format("{0} | {1} | {2} | {3} | ", CustomName, PlayerName, ChosenModule, HELPER.DifficultyToString(ChosenDifficulty));
        }
    }
}

