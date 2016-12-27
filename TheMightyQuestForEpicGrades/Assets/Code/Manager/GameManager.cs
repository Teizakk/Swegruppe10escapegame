using Assets.Code.Scripts.FeatureScripts;
using UnityEngine;

//TODO Singleton benötigt?

namespace Assets.Code.Manager {
    public class GameManager : MonoBehaviour {
        private static short _numOfInstances;
        public static GameManager instance;
        public static bool cheat = false;
        public BoardBuilderScript boardManager;

        //private int level = 1;

        //private QuestionManager qc { get; set; }

        // Use this for initialization
        //private void Awake() {
        //    if (instance == null)
        //        instance = this;
        //    else if (instance != this)
        //        Destroy(gameObject);
        //    boardManager = GetComponent<BoardBuilderScript>();
        //    DontDestroyOnLoad(instance);
        //    // TODO InitGame darf erst geschehen, wenn das eigentliche Spiel gestartet wird
        //    InitGame();
        //}

        private void InitGame() {
            //TODO Das muss auch noch differntiert werden...
            boardManager.SetupScene(1);
        }

        //TODO billig-singleton-check ohne singleton idee
        public short GetNumberOfInstances() {
            return _numOfInstances;
        }

        private void Awake() {
            _numOfInstances++;
            //qc = QuestionManager.GetInstance();
        }

        public void OnDestroy() {
            _numOfInstances--;
        }

        //public QuestionManager QuestionControllerInstance() {
        //    return qc;
        //}
    }
}