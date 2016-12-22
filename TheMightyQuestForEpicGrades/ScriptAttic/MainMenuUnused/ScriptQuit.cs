using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptQuit : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClickListener);
	}

    void Destory()
    {
        button.onClick.RemoveAllListeners();
    }

    void onClickListener()
    {
        Application.Quit();
    }
}
