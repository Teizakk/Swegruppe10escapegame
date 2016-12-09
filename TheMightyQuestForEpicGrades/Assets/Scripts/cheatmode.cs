using UnityEngine;
using System.Collections;

public class cheatmode : MonoBehaviour {

    private bool h = false;
    private bool a= false;

    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     /*   if (Input.GetKeyDown("h"))
        {
            Debug.Log("h");
            h = true;
            


        }

        if (Input.GetKeyDown("a"))
        {
            if (h == true)
            {
                Debug.Log("a");
                a = true;
            }


        }

        if (Input.GetKeyDown("x"))
        {
            if (h == true && a == true)
            {
                Debug.Log("x");
                GameManager.cheat = true;
                if (GameManager.cheat == true)
                    Debug.Log("easy");
            }

    

        }*/
        if (Input.anyKey) {

            if (Input.GetKey("h"))
            {
                h = true;
                Debug.Log("h");
            } else if (Input.GetKey("a") && h == true)
            {
                a = true;
                Debug.Log("a");
            } else if (Input.GetKey("x") && h == true && a == true)
            {

                Debug.Log("x");
                //GameManager.cheat = true;
                //if (GameManager.cheat == true)
                    Debug.Log("easy");
            } else
            {
                Debug.Log("false");
                h = false;
                a = true;
            }

        }

        

       

    }
}
