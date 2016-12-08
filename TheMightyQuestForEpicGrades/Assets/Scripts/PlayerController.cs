﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public float position_X;
    public float position_Z;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.tag = "Player";

        //Setzen der Start-Position
        rb.MovePosition(new Vector3(position_X,0,position_Z));
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Chest") && Input.GetKeyDown("E"))
        {
            //öffnen der Truhe,Fragen laden
        }
        else if (other.gameObject.CompareTag("PinkPortal") && Input.GetKeyDown("E"))// && Portalstein vorhanden)
        {
            PinkPortalSkript.Activated = true;
        }
        else if (other.gameObject.CompareTag("GreenPortal") && Input.GetKeyDown("E"))// && Portalstein vorhanden)
        {
            GreenPortalSkript.Activated = true;
        }
        else if (other.gameObject.CompareTag("BluePortal") && Input.GetKeyDown("E"))// && Portalstein vorhanden)
        {
            BluePortalSkript.Activated = true;
        }
    }

    void FixedUpdate()
    {
        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement*speed;
    }
}