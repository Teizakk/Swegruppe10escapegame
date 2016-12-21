using UnityEngine;
using System.Collections;

public class GameLoaderScript : MonoBehaviour
{

    public GameController GameController;
    public PlayerController playerController;

    // Use this for initialization
    void Awake()
    {
        if (PlayerController.instance == null)
            Instantiate(playerController);
        if (GameController.instance == null)
            Instantiate(GameController);
    }
}
