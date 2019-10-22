using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    LineRenderer lr;
    PolygonCollider2D poly;

    public List<float> pointRotations;
    public List<float> distances;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        poly = gameObject.GetComponent<PolygonCollider2D>();

        lr.positionCount = poly.GetPath(0).Length;

        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector2 currentPointPos = poly.GetPath(0)[i];
            Vector2 pos = currentPointPos + (Vector2)transform.position;
            lr.SetPosition(i, pos);

            float angle = Vector2.Angle((Vector2)transform.position, currentPointPos);
            //float angle = Vector2.Angle(currentPointPos, (Vector2)transform.position);
            float dist = Vector2.Distance(currentPointPos, (Vector2)transform.position);

            pointRotations.Add(angle);
            distances.Add(dist);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 10 * Time.deltaTime));

        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, (Vector2)transform.position + poly.GetPath(0)[i]);

            /*
            float rotZ = transform.rotation.eulerAngles.z;
            Vector2 currentPointPos = poly.GetPath(0)[i];

            float dist = Vector2.Distance(transform.position, currentPointPos);
            float angle = Vector2.Angle();
            
            //float dist = distances[i];
            //float currentRot = rotZ + pointRotations[i];

            float xPos = dist * Mathf.Cos(Mathf.Deg2Rad * currentRot);
            float yPos = dist * Mathf.Sin(Mathf.Deg2Rad * currentRot);

            Vector2 pos = new Vector2(xPos, yPos);
            Vector2 newPos = (Vector2)transform.position + pos;

            lr.SetPosition(i, newPos);

            //Vector2 currentPointPos = poly.GetPath(0)[i];
            //float angle = Vector2.Angle(currentPointPos, (Vector2)transform.position);
            /*
            float dist = Vector2.Distance(currentPointPos, (Vector2)transform.position);
            float width = currentPointPos.x - transform.position.x;

            float deg = Mathf.Rad2Deg * Mathf.Acos(width / dist);
            float rotZ = transform.rotation.eulerAngles.z;

            deg += rotZ;
            deg = Mathf.Deg2Rad * deg;

            float xPos = dist * Mathf.Cos(deg);
            float yPos = dist * Mathf.Sin(deg);
            */
            //Vector2 pos = currentPointPos + (Vector2)transform.position;
            /*
            if (i == 1)
            {
                print("Angle: " + angle);
            }*/

            //lr.SetPosition(i, pos);

            //lr.SetPosition(i, /*poly.GetPath(0)[i] /*/ pos/* + (Vector2)transform.position*/);
        }
        //print("length = " + poly.GetPath(0).Length);
        //for (int i = 0; i < poly.GetPath(0).)
        /*
        Vector2[] pointsTmp = poly.GetPath(0);
        Vector3[] points = new Vector3[pointsTmp.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = pointsTmp[i];
        }

        lr.SetPositions(points);*/
        /*
        
        List<Vector3> pointsTmp = new List<Vector3>();

        for (int i = 0; i < points.Length; i++)
        {
            pointsTmp.Add(points[i]);
        }

        lr.SetPositions(pointsTmp);*/

        //
        //Vector3[] poin = points;

        //
    }
}
