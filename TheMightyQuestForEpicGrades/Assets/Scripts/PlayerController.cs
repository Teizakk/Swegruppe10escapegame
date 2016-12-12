using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public float speed;

    [HideInInspector] public float position_X;
    [HideInInspector] public float position_Z;

    private Rigidbody rb;

    void Start()
    {
        if (instance == null)
        {
            Debug.Log("Instanz wurde verknuepft");
            instance = this;
        }
        else if (instance != null)
            Destroy(this.gameObject);

        rb = GetComponent<Rigidbody>();

        DontDestroyOnLoad(instance);
        SetStartPosition();

        //Setzen der Start-Position
        //rb.MovePosition(new Vector3(position_X,0,position_Z));
    }

    public void test()
    {
        Debug.Log("ashdaskjdaslkjdaskj");
    }

    public PlayerController GetInstance()
    {
        return instance;
    }

    public void SetStartPosition()
    {
        position_X = GameManager.instance.boardManager.StartPosition.x;
        position_Z = GameManager.instance.boardManager.StartPosition.z;

        rb.MovePosition(new Vector3(position_X, 1.0f, position_Z));
    }

    //void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Chest") && Input.GetKeyDown("e"))
    //    {
    //        //öffnen der Truhe,Fragen laden
    //        Debug.Log("Truhe öffnen");
    //    }
    //    else if (other.gameObject.CompareTag("PinkPortal") && Input.GetKeyDown("e"))// && Portalstein vorhanden)
    //    {
    //        PinkPortalSkript.Activated = true;
    //        Debug.Log("PinkPortalSkript.Activated = " + PinkPortalSkript.Activated.ToString());
    //    }
    //    else if (other.gameObject.CompareTag("GreenPortal") && Input.GetKeyDown("e"))// && Portalstein vorhanden)
    //    {
    //        GreenPortalSkript.Activated = true;
    //        Debug.Log("GreenPortalSkript.Activated = " + GreenPortalSkript.Activated.ToString());
    //    }
    //    else if (other.gameObject.CompareTag("BluePortal") && Input.GetKeyDown("e"))// && Portalstein vorhanden)
    //    {
    //        BluePortalSkript.Activated = true;
    //        Debug.Log("BluePortalSkript.Activated = " + BluePortalSkript.Activated.ToString());
    //    }
    //}

    int bla = 0;
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Chest") && Input.GetKeyDown("e"))
        {
            UnityEngine.Debug.Log(bla++);
        }
    }

    void FixedUpdate()
    {
        //Movement
        float moveHorizontal = Input.GetAxis("Vertical") * (-1);
        float moveVertical = Input.GetAxis("Horizontal");

        //Gucken ob die kombinierte Bewegung von X und Z Achse über dem gesetzten Maximum von 1 liegt
        float combindedSpeed = Mathf.Sqrt((moveHorizontal*moveHorizontal) + (moveVertical*moveVertical));
        if ( combindedSpeed > 1.0f ) {
            //Limitiert die Quadrate der Bewegungen so, dass maximal 1 als kombinierte Bewegung resultiert
            moveHorizontal /= combindedSpeed;
            moveVertical /= combindedSpeed;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        //Debug Ausgabe zeigt Spielergeschwindigkeit
        //Debug.Log(Mathf.Sqrt((moveHorizontal * moveHorizontal) + (moveVertical * moveVertical)));
    }
}
