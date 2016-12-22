using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;

namespace Assets.Code.Controller {
    public class GameManager : MonoBehaviour {
        public static GameManager instance;
        public static bool cheat = false;
        public BoardBuilderScript boardManager;
        private int level = 1;
        private QuestionManager qc { get; set; }

        // Use this for initialization
        private void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            boardManager = GetComponent<BoardBuilderScript>();
            DontDestroyOnLoad(instance);
            // TODO InitGame darf erst geschehen, wenn das eigentliche Spiel gestartet wird
            InitGame();
        }

        private void InitGame() {
            //TODO Das muss auch noch differntiert werden...
            boardManager.SetupScene(1);
        }

        private void Start() {
            qc = QuestionManager.GetInstance();
        }

        public QuestionManager QuestionControllerInstance() {
            return qc;
        }
    }
}