using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptNewGame : MonoBehaviour {

    private Button button;

	void Start()
    {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClickListener);
    }

    void Destroy()
    {
        button.onClick.RemoveAllListeners();
    }

    void onClickListener()
    {
        Debug.Log("Neues Spiel wurde gedrückt");
    }
}
