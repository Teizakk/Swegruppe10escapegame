using UnityEngine;

// Skript "bindet" die Bewegung der Kamera an die des Spielers
// Daher muss das Skript auf der Kamera ausgeführt werden

namespace Assets.Code.Scripts.FeatureScripts {
    public class MovingCameraScript : MonoBehaviour {
        // Reference zum Player gameobject
        public static GameObject player;
        // Offset Variable (= Distanz zwischen Spielfigur und Kamera)
        private Vector3 offset;

        private void Start() {
            // Player gameobject finden
            player = GameObject.Find("PlayerTicTac(Clone)");
            // Debug und Sicherheit
            if (player == null) throw new UnityException("Spielfigur nicht gefunden");
            // Berechnet den offset als Distanz zwischen Spieler und Kamera
            offset = new Vector3(5.0f, 9.5f, 0.0f);
            //transform.position - player.transform.position;
        }

        // LateUpdate is called after Update each frame
        // => um sicher zu stellen, dass es immer nach der Spielerbewegung
        // aufgerufen wird.
        private void LateUpdate() {
            // Setzt die Position der Kamera auf die des Spielers +/- Offset
            transform.position = player.transform.position + offset;
        }
    }
}