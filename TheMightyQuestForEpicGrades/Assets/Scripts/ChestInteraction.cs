using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChestInteraction : MonoBehaviour
{
    private bool ChestIsOpen = false;
    private bool ChestIsLocked = false;

    public void OnTriggerStay(Collider col)
    {
        if (Input.GetKeyDown("e") && !ChestIsLocked) //col.gameObject.tag == "Player")
        {
            ChestIsOpen = true;
            OpenChest();
            ChestIsLocked = true; //Truhe abschließen
            Destroy(gameObject);//Lässt Truhe verschwinden
        }
    }

    public void OpenChest()
    {
        Debug.Log("derp");
        //Fragemenü aufrufen
        ChestIsOpen = false;
    }
}
