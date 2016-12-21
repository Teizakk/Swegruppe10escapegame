using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FeatureScripts {
    public class MenuButtonScript : MonoBehaviour {
        public void DebugLogOnClick(string str) {
            Debug.Log(str);
        }

        public void NextWindowOnClick(int level) {
            SceneManager.LoadScene(level);
        }

        public void CloseWindowOnClick() {
            Application.Quit();
        }
    }
}