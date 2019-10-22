using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    float horDist;

    // Start is called before the first frame update
    void Start()
    {
        horDist = transform.position.x - followObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(followObject.transform.position.x + horDist, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
}
