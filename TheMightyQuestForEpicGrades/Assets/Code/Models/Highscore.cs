using System;

namespace Assets.Code.Models {
    [Serializable]
    public class Highscore {
        public int Score { get; set; }
        public string Zeit { get; set; }
        public string PlayerName { get; set; }
        public DateTime Datum { get; set; }
		public string Module { get; set; }
    }
}
