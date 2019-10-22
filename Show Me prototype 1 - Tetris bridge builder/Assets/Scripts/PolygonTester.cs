using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTester : MonoBehaviour
{
    public List<Vector2> vertices2D;
    public float inkUsed;

    LevelController controller;

    void Start()
    {
        controller = FindObjectOfType<LevelController>();
        // Create Vector2 vertices
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Count];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        gameObject.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;

        PolygonCollider2D poly = gameObject.AddComponent<PolygonCollider2D>();
        poly.SetPath(0, vertices2D);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            print("vorm raakt de grond!");
            controller.player.walking = true;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        controller.inkLevel += inkUsed;
    }
}