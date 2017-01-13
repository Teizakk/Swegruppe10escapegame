using UnityEngine;
using System.Collections;

namespace Assets.Code.GLOBALS {

    public enum Difficulties {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum PortalColor {
        Blue = 0,
        Green = 1,
        Pink = 2
    }

    public static class HELPER {
        public static string DifficultyToString(Difficulties difficulty) {
            switch (difficulty) {
                case Difficulties.Easy:
                    return "Einfach";
                case Difficulties.Medium:
                    return "Mittel";
                case Difficulties.Hard:
                    return "Schwer";
                default:
                    //kann eigentlich nicht aufgerufen werden
                    return "#ILLEGAL_DIFFICULTY";
            }
        }
    } 
    
    //Globale Konstanten?
    public static class CONSTANTS {
        public static readonly float PLAYER_SPEED = 3.25f;
    }
}
