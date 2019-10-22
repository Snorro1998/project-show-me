using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    GameObject player;
    PlayerMovement play;

    public bool destroyPreviousShapes = true;
    public float timeBonus = 10;

    LevelController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<LevelController>();
        play = FindObjectOfType<PlayerMovement>();
        player = play.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            print("stop speler");
            play.walking = false;

            if (transform.position != play.respawnPoint)
            {
                play.respawnPoint = transform.position;
                controller.score += (int)(controller.inkLevel * 0.5f);
                controller.timeLeft += timeBonus;
            }

            if (destroyPreviousShapes)
            {
                controller.dr.deleteShapes();
            }
        }
    }
}
