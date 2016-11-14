using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptNewContents : MonoBehaviour {

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
        Debug.Log("Neue Inhalte wurde gedrückt");
    }
}
