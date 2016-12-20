﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Assets.Scripts;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    private bool controlsBlocked;

    public float speed;

    [HideInInspector]
    public float position_X;
    [HideInInspector]
    public float position_Z;

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

        //Standardmäßig sind die Kontrollen natürlich an...
        controlsBlocked = false;
    }

    public PlayerController GetInstance()
    {
        return instance;
    }

    public void switchControlBlock()
    {
        controlsBlocked = !controlsBlocked;
        //Debug Ausgabe
        Debug.Log(controlsBlocked ? "Controls are now blocked" : "Controls are now enabled");
    }

    public void SetStartPosition()
    {
        position_X = GameManager.instance.boardManager.StartPosition.x;
        position_Z = GameManager.instance.boardManager.StartPosition.z;

        rb.MovePosition(new Vector3(position_X, 1.0f, position_Z));
    }

    private int DebugLogVar = 0;
    void OnCollisionStay(Collision other)
    {
        DebugLogVar++;
        if (!controlsBlocked)
        {
            if (other.gameObject.CompareTag("Chest") && Input.GetKeyDown(KeyCode.E))
            {
                //öffnen der Truhe,Fragen laden
                Debug.Log("Truhe öffnen (" + DebugLogVar + ")");

                var questionDialog = ScriptQuestionDialog.Instance();

                Question question = new Question
                {
                    QuestionText = "Was ist das Internet?",
                    Difficulty = Difficulties.Easy,
                    Level = 1,
                    ImagePath = Path.GetFullPath("Assets/Samples+Placeholder/Beispielbild.png"),
                    Answers =
                new List<Question.Answer>()
                {
                        new Question.Answer()
                        {
                            AnswerText = "Ein Netz",
                            ImagePath = ""
                        },
                        new Question.Answer()
                        {
                            AnswerText = "Nur physikalisch vorhanden",
                            ImagePath = "Assets/Samples+Placeholder/Bild2.png"
                        },
                        new Question.Answer()
                        {
                            AnswerText = "Ein Netz von Netzen",
                            ImagePath = ""
                        },
                },
                    CorrectAnswer = 3,
                    Hints = new List<string> { "inter", "connected", "networks" }
                };

                questionDialog.ShowQuestion(question);
            }
            else if (other.gameObject.CompareTag("PinkPortal") && Input.GetKeyDown(KeyCode.E))// && Portalstein vorhanden)
            {
                PinkPortalSkript.Activated = true;
                Debug.Log("PinkPortalSkript.Activated = " + PinkPortalSkript.Activated.ToString() + " (" + DebugLogVar + ")");
            }
            else if (other.gameObject.CompareTag("GreenPortal") && Input.GetKeyDown(KeyCode.E))// && Portalstein vorhanden)
            {
                GreenPortalSkript.Activated = true;
                Debug.Log("GreenPortalSkript.Activated = " + GreenPortalSkript.Activated.ToString() + " (" + DebugLogVar + ")");
            }
            else if (other.gameObject.CompareTag("BluePortal") && Input.GetKeyDown(KeyCode.E))// && Portalstein vorhanden)
            {
                BluePortalSkript.Activated = true;
                Debug.Log("BluePortalSkript.Activated = " + BluePortalSkript.Activated.ToString() + " (" + DebugLogVar + ")");
            }
        }
    }

    void FixedUpdate()
    {
        if (!controlsBlocked)
        {
            //Movement
            float moveHorizontal = Input.GetAxis("Vertical") * (-1);
            float moveVertical = Input.GetAxis("Horizontal");

            //Gucken ob die kombinierte Bewegung von X und Z Achse über dem gesetzten Maximum von 1 liegt
            float combindedSpeed = Mathf.Sqrt((moveHorizontal * moveHorizontal) + (moveVertical * moveVertical));
            if (combindedSpeed > 1.0f)
            {
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
}
