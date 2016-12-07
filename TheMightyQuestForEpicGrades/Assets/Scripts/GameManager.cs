using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public BoardManager boardManager;
    private QuestionController qc { get; set; }
    private int level = 1;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        boardManager = GetComponent<BoardManager>();

        DontDestroyOnLoad(instance);
        // TODO
        // InitGame darf erst geschehen, wenn das eigentliche Spiel gestartet wird
        InitGame();
    }

    void InitGame()
    {
        boardManager.SetupScene(1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Start()
    {
        qc = QuestionController.GetInstance();
    }

    QuestionController QuestionControllerInstance()
    {
        return qc;
    }


}
