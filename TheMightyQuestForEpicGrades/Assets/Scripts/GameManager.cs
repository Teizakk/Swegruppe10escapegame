using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardManager;

    private int level = 1;

	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(instance);
        InitGame();
	}

    void InitGame()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
