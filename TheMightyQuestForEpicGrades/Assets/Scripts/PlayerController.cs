using UnityEngine;
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
        else if (other.gameObject.CompareTag("Portal") && Input.GetKeyDown("E"))// && Portalstein vorhanden)
        {
            //Portal öffnen
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
