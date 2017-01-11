using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class AudioManager : MonoBehaviour {

        private AudioSource music;

        // Use this for initialization
        private void Awake() {
            music = gameObject.AddComponent<AudioSource>();
            music.clip = Resources.Load("Audio/Sheltered_Gameplay4") as AudioClip;
            music.Play();
            music.loop = true;
            music.volume = 0.75f;
            music.spatialBlend = 0.123f;
            music.spread = 200;
        }

        //Müsste immer beim Szenenwechseln, bevor die Kamera rendert aufgerufen werden
        private void OnPreRender () {
            if (SceneManager.GetActiveScene().name == "MainGame") {
                music.clip = Resources.Load("Audio/BackgroundMusic") as AudioClip;
                music.Play();
                music.loop = true;
            }
        }
    }
}
