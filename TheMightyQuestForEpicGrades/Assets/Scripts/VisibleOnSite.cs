using UnityEngine;
using System.Collections;

public class VisibleOnSite : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    this.gameObject.GetComponent<Renderer>().enabled = false;
        //Macht das Objekt beim starten Unsichtbar
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
           //Wenn der Player zu Nahe kommt wird das Objekt dauerhaft Sichtbar
        }
    }
}
