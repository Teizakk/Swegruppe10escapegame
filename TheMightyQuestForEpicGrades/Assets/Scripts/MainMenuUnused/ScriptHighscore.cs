using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptHighscore : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start ()
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
        Debug.Log("Highscore wurde gedrückt");
    }
}
