using System.Collections;
using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Code.Manager {
    public class AudioManager : MonoBehaviour { 

        private AudioSource music = null;

        // Use this for initialization
        private void Awake() {
            music = gameObject.AddComponent<AudioSource>();
            music.clip = Resources.Load("Audio/Sheltered_Gameplay4") as AudioClip;
            music.Play();
            music.loop = true;
            music.volume = 0.25f;
            music.spatialBlend = 0.123f;
            music.spread = 200;
        }

        //Müsste immer beim Szenenwechseln, bevor die Kamera rendert aufgerufen werden
        public void ChangeTracks () {
            music.Stop();
            music.clip = Resources.Load("Audio/BackgroundMusic") as AudioClip;
            StartCoroutine(Wait2Seconds());
        }

        private IEnumerator Wait2Seconds() {
            yield return new WaitForSeconds(2);
            music.Play();
            music.loop = true;
        }
    }
}
