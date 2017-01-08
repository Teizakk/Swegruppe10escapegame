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
    public class SavegameInfo {
        #region Dateipfad zum gespeicherten Spielstand
        public string FilenameOfGameStateSave { get; set; }
        #endregion
        #region "Meta-Tags"
        public string CustomName { get; set; }
        public string PlayerName { get; set; }
        public Difficulties ChosenDifficulty { get; set; }
        public string ChosenModule { get; set; }
        public long TimeCode { get; set; }
        #endregion
        public override string ToString() {
             return string.Format("{0}\t\t{1}\t\t{2}\t\t{3}\t\t", PlayerName, ChosenModule, HELPER.DifficultyToString(ChosenDifficulty), CustomName);
        }
    }
}

