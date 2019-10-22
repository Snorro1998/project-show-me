using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCreator : MonoBehaviour
{
    public GameObject shapePrefab;
    public GameObject currentShape = null;
    //public ShapeScript currentShapeScript = null;

    public float pointDistance = 0.5f;
    //public float inkLevel = 100.0f;

    public float maxInk = 100.0f;
    public float inktUsing = 0;

    LineRenderer lr;

    LevelController controller;

    public List<Vector2> points;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        controller = FindObjectOfType<LevelController>();
        points = new List<Vector2>();
    }

    void addPoint()
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, controller.mousePos);
        points.Add(lr.GetPosition(lr.positionCount - 1));
    }

    public void createNewShape()
    {
        lr.loop = false;
        inktUsing = 0;
        lr.positionCount = 0;
        points = new List<Vector2>();
        addPoint();
    }

    public void updateShape()
    {
        if (lr.positionCount > 0 && inktUsing < maxInk)
        {
            float dist = Vector2.Distance(controller.mousePos, lr.GetPosition(lr.positionCount - 1));

            if (dist > pointDistance)
            {
                inktUsing++;
                print("Voegt punt toe");
                addPoint();
            }
        }
    }

    public void finishShape()
    {
        if (lr.positionCount > 0)
        {
            float dist = Vector2.Distance(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1));

            if (dist <= pointDistance)
            {
                lr.loop = true;
                lr.positionCount = 0;

                float xMean = 0;
                float yMean = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    xMean += points[i].x;
                    yMean += points[i].y;
                }
                xMean /= points.Count;
                yMean /= points.Count;

                Vector2 instantPos = new Vector2(xMean, yMean);
                Vector2 ownPos = transform.position;
                Vector2 translatie = ownPos - instantPos;

                for (int i = 0; i < points.Count; i++)
                {
                    points[i] += translatie;
                }

                currentShape = Instantiate(shapePrefab, instantPos, transform.rotation);
                currentShape.GetComponent<PolygonCollider2D>().SetPath(0, points);

                currentShape.transform.position = Vector2.zero;
                currentShape.GetComponent<TestKubus>().spawnPunt = instantPos;
            }
            else
            {
                inktUsing = 0;
                lr.positionCount = 0;
            }
        }
    }
    /*
    public GameObject shapePrefab;
    public GameObject currentShape;
    public LineRenderer currentLine;

    LevelController levelController;

    public float pointDistance = 2f;

    Vector2 lastPos = Vector2.zero;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void createNewShape()
    {
        lastPos = levelController.mousePos;
        currentShape = Instantiate(shapePrefab, lastPos, transform.rotation);
    }
    
    public void updateShape()
    {
        if (currentShape == null) return;

        float dis = Vector2.Distance(levelController.mousePos, lastPos);

        if (dis > pointDistance)
        {
            lastPos = levelController.mousePos;
            currentShape.GetComponent<ShapeScript>().addPoint(lastPos);
        }

        else
        {
            currentShape.GetComponent<ShapeScript>().updatePos();
        }

        //currentLine.SetPosition(currentLine.positionCount - 1, levelController.mousePos);
    }*/
}
