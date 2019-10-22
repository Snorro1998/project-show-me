using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool walking = false;
    Rigidbody2D rb;
    public Vector3 spawnPoint, respawnPoint;

    Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        respawnPoint = spawnPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (walking)
        {
            Vector2 newPos = new Vector2(transform.position.x + 5 * Time.deltaTime, transform.position.y);
            transform.position = newPos;

            float dist = Vector2.Distance(transform.position, lastPos);
            if (dist < 0.05) rb.AddForce(Vector2.up * 100);
        }
        
        if (transform.position.y < 0)
        {
            //respawn;
            walking = false;
            transform.position = respawnPoint;
        }
        lastPos = transform.position;
    }
}
