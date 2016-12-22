using System;
using UnityEngine;

namespace Assets.Code.Models {
    [Serializable]
    public class Highscore : MonoBehaviour {
        public int Score { get; set; }
        public string Zeit { get; set; }
        public string PlayerName { get; set; }
    }
}
