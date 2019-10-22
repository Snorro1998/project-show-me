using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_block : MonoBehaviour
{
    Collider2D col;
    public float newXposition;
    public float newYposition;
    public float speed;
    public int isTouched = 0;

    public bool usingPhone = true;

    public Transform highPoint;
    public Transform lowPoint;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {


        if (usingPhone)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                    if (col == touchedCollider && isTouched == 0)
                    {
                        isTouched = 1;
                    }
                }


                if (touch.phase == TouchPhase.Ended)
                {
                    if (isTouched == 1)
                    {
                        transform.position = lowPoint.position;
                        isTouched = 2;
                    }
                }
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouched = 1;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isTouched == 1)
                {
                    transform.position = lowPoint.position;
                }
            }
        }
        
    }
}
