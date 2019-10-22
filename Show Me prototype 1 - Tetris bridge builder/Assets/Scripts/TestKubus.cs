using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKubus : MonoBehaviour
{
    PolygonCollider2D col;
    LineRenderer lr;

    public List<float> afstanden;
    public List<float> rotaties;

    public Vector3 spawnPunt;

    void Start()
    {
        col = gameObject.GetComponent<PolygonCollider2D>();
        lr = gameObject.GetComponent<LineRenderer>();

        afstanden = new List<float>();
        rotaties = new List<float>();

        Vector2[] colliderPunten = col.GetPath(0);

        for (int i = 0; i < colliderPunten.Length; i++)
        {
            //afstand is hetzelfde als de schuine zijde
            float afstand = Vector2.Distance((Vector2)transform.position, colliderPunten[i]);
            afstanden.Add(afstand);

            float aanLiggendeZijde = colliderPunten[i].x - transform.position.x;
            float hoek = Mathf.Acos(aanLiggendeZijde / afstand);

            hoek = Mathf.Rad2Deg * hoek;

            //als het punt in het 3e of 4e kwadrant ligt
            if (colliderPunten[i].y < transform.position.y)
            {
                hoek = 360.0f - hoek;
            }

            rotaties.Add(hoek);

            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, colliderPunten[i]);

            
        }
        transform.position = spawnPunt;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void Update()
    {
        float zRotatie = transform.rotation.eulerAngles.z;

        for (int i = 0; i < afstanden.Count; i++)
        {
            float rotatie = zRotatie + rotaties[i];

            float xx = transform.localPosition.x;
            float yy = transform.localPosition.y;

            float xPos = transform.position.x + afstanden[i] * Mathf.Cos(Mathf.Deg2Rad * rotatie);
            float yPos = transform.position.y + afstanden[i] * Mathf.Sin(Mathf.Deg2Rad * rotatie);

            Vector2 newPos = new Vector2(xPos, yPos);
            lr.SetPosition(i, newPos);
        }
    }
}
