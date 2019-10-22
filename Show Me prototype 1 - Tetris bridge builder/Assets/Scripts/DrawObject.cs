using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawObject : MonoBehaviour
{
    public LevelController controller;
    public LineRenderer lr;

    public GameObject shapePrefab;
    public GameObject ShapesObject;
    public List<Vector2> points;

    float pointDistance = 0.2f;

    void Start()
    {
        controller = FindObjectOfType<LevelController>();
        lr = GetComponent<LineRenderer>();
    }

    void addPoint()
    {
        lr.SetPosition(lr.positionCount - 1, transform.position);
        lr.positionCount++;
    }

    public void StartNewShape()
    {
        lr.loop = false;
        lr.positionCount = 1;
        addPoint();
    }

    public void UpdateShape()
    {
        float dist = Vector2.Distance(lr.GetPosition(lr.positionCount - 2), transform.position);
        if (dist > pointDistance && controller.inkLevel > 0)
        {
            controller.inkLevel -= 1;
            print("voegt punt toe");
            addPoint();
        }

        lr.SetPosition(lr.positionCount - 1, transform.position);
    }

    public void FinishShape()
    {
        //vorm kan niet gemaakt worden
        float dist = Vector2.Distance(lr.GetPosition(lr.positionCount - 1), lr.GetPosition(0));
        if (lr.positionCount < 4 || dist > pointDistance)
        {
            lr.positionCount = 0;
            controller.inkLevel = controller.lastInkLevel;
        }

        //vorm wordt gemaakt
        else
        {
            lr.loop = true;

            float xMean = 0;
            float yMean = 0;

            for (int i = 0; i < lr.positionCount; i++)
            {
                xMean += lr.GetPosition(i).x;
                yMean += lr.GetPosition(i).y;
            }

            xMean /= lr.positionCount;
            yMean /= lr.positionCount;

            Vector2 spawnPoint = new Vector2(xMean, yMean);

            points = new List<Vector2>();

            for (int i = 0; i < lr.positionCount; i++)
            {
                points.Add((Vector2)lr.GetPosition(i) - spawnPoint);
            }

            GameObject shape = Instantiate(shapePrefab, spawnPoint, transform.rotation);
            PolygonTester pol = shape.GetComponent<PolygonTester>();
            shape.transform.parent = ShapesObject.transform;
            pol.vertices2D = points;
            pol.inkUsed = controller.lastInkLevel - controller.inkLevel;

            lr.positionCount = 0;
        }
    }

    public void deleteShapes()
    {
        float numChild = ShapesObject.transform.childCount;

        for (int i = ShapesObject.transform.childCount - 1; i > -1; i--)
        {
            Destroy(ShapesObject.transform.GetChild(i).gameObject);
        }
        /*
        foreach (Transform child in ShapesObject.transform)
        {
            Destroy(child);
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = controller.mousePos;
    }
}
