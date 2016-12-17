using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.UI;

[Serializable]
public class GameState {

    #region Interne Klassendefinitionen

    [Serializable]
    public class Preselectives {
        public int Difficulty { get; set; }
        public string Modul { get; set; } //imo fehleranfällig, mal gucken ob wir damit durchkommen
        }

    [Serializable]
    public class CurrentLevelState {
        //Die genutzte Leveldatei
        public string Level { get; set; }
        //Die aktuelle Stufe bzw das Level im Spieldurchgang
        public int Stage { get; set; }
        public Vector3 PlayerPosition { get; set; }

        //Chests[10]
        //Fragen
        //Leben
        //Hinweise
        //Verbrauchte Zeit
        //Punkte
        //Portalsteine (im Besitz/benutzt)

        //ob cheatmode an ist
        public bool Cheatmode { get; set; }
    }
    #endregion

    #region Zu speichernde Werte
    // V O R E I N G E S T E L L T E   W E R T E 
    //Spieleinstellungen (Schwierigkeitsgrad und Modul) - siehe oben.
    public Preselectives GameOptions;

    // W E R T E   D E S   A K T U E L L E N   L E V E L S 
    public CurrentLevelState LevelState;

    #endregion

    //Kontruktor
    public GameState() {
        GameOptions = new Preselectives();
        LevelState = new CurrentLevelState();
    }
}
