using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public float pointDistance = 0.5f;

    public float inkLevelMaxValue = 100.0f;
    public float inkLevel = 100.0f;

    [HideInInspector]
    public float lastInkLevel;

    public int score = 0;
    public float timeLeft = 20.0f;

    public GameObject shapePrefab;

    public Vector2 mousePos;
    [HideInInspector]
    public GameObject currentShape;

    Vector3 shapeLastPos;

    //ShapeCreator shapeCreator;
    [HideInInspector]
    public DrawObject dr;
    [HideInInspector]
    public PlayerMovement player;

    Touch touch;
    public bool usingComputer = false;

    bool leftMouseReleased()
    {
        if (usingComputer) return Input.GetMouseButtonUp(0);
        else return (touch.phase == TouchPhase.Ended);
    }

    bool leftMousePressed()
    {
        if (usingComputer) return Input.GetMouseButtonDown(0);
        else return (touch.phase == TouchPhase.Began);
    }

    bool leftMouseHold()
    {
        if (usingComputer) return Input.GetMouseButton(0);
        else return (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary);
    }

    void reloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void Start()
    {
        //shapeCreator = FindObjectOfType<ShapeCreator>();
        dr = FindObjectOfType<DrawObject>();
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!usingComputer)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        mousePos = touch.position;
                        break;
                    case TouchPhase.Moved:
                        //if (!onlySwipeAfterRelaease)
                        {
                            mousePos = touch.position;
                            //CheckForSwipe();
                        }
                        break;
                    case TouchPhase.Ended:
                        mousePos = touch.position;
                        //CheckForSwipe();
                        break;
                }
            }
        }

        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        

        timeLeft -= Time.deltaTime;
        inkLevel = Mathf.Min(inkLevelMaxValue, Mathf.Max(0, inkLevel));

        if (leftMousePressed())
        {
            lastInkLevel = inkLevel;
            dr.StartNewShape();
        }

        if (leftMouseHold())
        {
            dr.UpdateShape();
        }

        if (leftMouseReleased())
        {
            dr.FinishShape();
        }

        if (timeLeft <= 0)
        {
            reloadLevel();
        }
    }
}
