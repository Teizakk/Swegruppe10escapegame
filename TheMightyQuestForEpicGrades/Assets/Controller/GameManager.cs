using Assets.Scripts.FeatureScripts;
using UnityEngine;

namespace Assets.Controller
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;
        public static bool cheat = false;
        public BoardBuilderScript boardManager;
        private QuestionManager qc { get; set; }
        private int level = 1;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            boardManager = GetComponent<BoardBuilderScript>();
            DontDestroyOnLoad(instance);
            // TODO InitGame darf erst geschehen, wenn das eigentliche Spiel gestartet wird
            InitGame();
        }

        void InitGame()
        {
            //TODO Das muss auch noch differntiert werden...
            boardManager.SetupScene(1);
        }

        // Update is called once per frame
        void Update()
        {

        }
        void Start()
        {
            qc = QuestionManager.GetInstance();
        }

        public QuestionManager QuestionControllerInstance()
        {
            return qc;
        }


    }
}