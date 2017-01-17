using System;
using System.Collections.Generic;

namespace Assets.Code.Models {
    [Serializable]
    public class Highscore {
        public int Score;
        public string Zeit;
        public string PlayerName;
        public DateTime Datum;
		public string Module;
    }

    [Serializable]
    public class HighscoreWrapper {
        public List<Highscore> List;
        public HighscoreWrapper() {
            List = new List<Highscore>();
        }
    }
}