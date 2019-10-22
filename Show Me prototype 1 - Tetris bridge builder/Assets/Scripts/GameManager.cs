using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCol;

    private main_collider colliderScript;

    public GameObject player;
    public Rigidbody2D playerRB;
    public float playerSpeed;
    private float playerStop = 0;
    public float playerTimer = 2;

    public Camera mainCamera;
    private float camY;
    private float camZ;

    public bool camCanMove = false;

    public List<GameObject> points;
    public GameObject balk;
    tetris_block echteBalk;

    int currentIndex = 0;
    int oldIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        colliderScript = mainCol.gameObject.GetComponent<main_collider>();
        playerRB = player.gameObject.GetComponent<Rigidbody2D>();
        mainCamera = mainCamera.GetComponent<Camera>();

        foreach (Transform child in transform)
        {
            points.Add(child.gameObject);
        }

        foreach(Transform child in balk.transform)
        {
            echteBalk = child.GetComponent<tetris_block>();
            if (echteBalk != null) break;
        }

        balk.transform.position = points[currentIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (echteBalk.isTouched > 0)
        //if (colliderScript.hasCollided == true)
        {
            
            playerRB.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            camCanMove = true;
            playerTimer -= Time.deltaTime;
            if (playerTimer < -0)
            {
                //verplaatst balk naar volgend punt
                playerSpeed = playerStop;
                echteBalk.gameObject.transform.position = echteBalk.highPoint.transform.position;
                currentIndex = (currentIndex + 1) % points.Count;
            }
            colliderScript.hasCollided = false;
            
            

        }

        if (camCanMove)
        {
            mainCamera.transform.position = mainCamera.transform.position + new Vector3(11f, 0f, 0f) * Time.deltaTime;
            camCanMove = false;
        }

        if (currentIndex != oldIndex)
        {
            balk.transform.position = points[currentIndex].transform.position;
            oldIndex = currentIndex;
        }
    }
}