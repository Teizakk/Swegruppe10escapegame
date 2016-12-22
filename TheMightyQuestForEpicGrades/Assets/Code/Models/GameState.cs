using System;
using System.Collections.Generic;
using Assets.Code.GLOBALS;
using UnityEngine;

namespace Assets.Code.Models {
    [Serializable]
    public class GameState {
        //Konstruktor
        public GameState() {
            GameOptions = new Preselectives();
            LevelState = new CurrentLevelState();
            //LevelState.PlayerPosition wird beim ersten Speichern gesetzt
            LevelState.Chests = new[] {false};
            LevelState.Questions = new List<Question>();
            LevelState.BluePortalStone = new PortalStone();
            LevelState.GreenPortalStone = new PortalStone();
            LevelState.PinkPortalStone = new PortalStone();
            //TODO weitere Werte initialisieren?
        }

        #region Interne Klassendefinitionen

        [Serializable]
        public class Preselectives { //Vorausgewählte Einstellungen
            public Difficulties Difficulty { get; set; } //Ausgewählter Schwierigkeitsgrad
            public string Modul { get; set; }
            //imo fehleranfällig, mal gucken ob wir damit durchkommen (sollte aber ok sein, weil in der Frage das Modul auch als String gespeichert ist)
            public string PlayerName { get; set; }
        }

        [Serializable]
        public class CurrentLevelState {
            //TODO eine Pfadangabe oder sogar der Index müsste reichen, allerdings sind dann Savegames broken, wenn die Leveldateien nicht mehr übereinstimmen
            public int Level { get; set; } //Nach Muster Level_X.txt X = der hier gespeicherte int32-Wert
            public int Kapitel { get; set; } //Aktuelle Stufe/Kapitel bzw das 'Level' im Spieldurchgang
            public Vector3 PlayerPosition { get; set; } //Die aktuelle Position des Spielers
            public bool[] Chests { get; set; } //Bool-Array[10] wo an der Stelle der ID der Truhe (in der Truhe gespeichert) steht ob sie locked ist //TODO müsste als Info reichen
            public List<Question> Questions { get; set; } //Die Liste aller zum Modul und zum Schwierigkeitsgrad passenden Fragen
            public int Lives { get; set; } //Die noch verbleibende Anzahl Leben
            public int HintStones { get; set; } //Die noch verbleibende Anzahl an Hinweissteinen
            public int Score { get; set; } //Der bisher erreichte Punktestand
            public PortalStone BluePortalStone { get; set; } //'Zustand'des blauen Portalsteins und damit auch Portals
            public PortalStone GreenPortalStone { get; set; } //'Zustand'des grünen Portalsteins und damit auch Portals
            public PortalStone PinkPortalStone { get; set; } //'Zustand'des pinken Portalsteins und damit auch Portals
            public bool Cheatmode { get; set; } //Ob der CheatModeScript aktiv ist
        }

        [Serializable]
        public class PortalStone {
            public bool InPossession { get; set; } //Ob der Portalstein 'gewonnen' wurde
            public bool Used { get; set; } //Ob der Portalstein in das Portal eingesetzt wurde
        }

        #endregion

        #region Zu speichernde Werte / Attribute

        // V O R E I N G E S T E L L T E   W E R T E 
        public Preselectives GameOptions;

        // W E R T E   D E S   A K T U E L L E N   L E V E L S 
        public CurrentLevelState LevelState;

        #endregion
    }
}