using System;
using System.Collections.Generic;
using UnityEngine;
public class CreateMesh : MonoBehaviour
{
    
    public float m_updateRate = 0.5F;
    private Vector3 m_mousePosition;
    //private Vector3 m_lastPosition;
    private float m_time = 0.0F;
    private Vector2 m_bottomPoint;
    private List<GameObject> ground;
    private GameObject m_currentGroundPiece;

    private void Awake()
    {
        ground = new List<GameObject>();

        Mesh m = new Mesh();
        m.name = "groundPiece";
        Vector2[] vertices2D = new Vector2[4];
        vertices2D[0] = new Vector2(-1, -1);
        vertices2D[1] = new Vector2(-1, 1);
        vertices2D[2] = new Vector2(1, -1);
        vertices2D[3] = new Vector2(1, 1);

        // Use the triangulator to get indices for creating triangles
        //Triangulator tr = new Triangulator(vertices2D);
        //int[] indices = tr.Triangulate();
        //System.Array.Reverse(indices);
        int[] indices = { 0, 1, 3, 0, 3, 2 };

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        m.vertices = vertices;
        m.triangles = indices;

        //m.RecalculateNormals();
        GameObject groundPiece = new GameObject();

        groundPiece.AddComponent<MeshFilter>();
        groundPiece.AddComponent<MeshRenderer>();
        groundPiece.GetComponent<MeshFilter>().mesh = m;
        groundPiece.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));

        m_currentGroundPiece = groundPiece;
        ground.Add(groundPiece);
    }

    // Use this for initialization
    void Start()
    {
        m_bottomPoint = new Vector2(1, -1);
    }

    // Update is called once per frame
    void Update()
    {

        m_time += Time.deltaTime;

        moveGroundLeft();

        if (Input.GetButton("Fire1") && m_time > m_updateRate)
            // Function seems not to be called when we press fire1 but don't move the mouse
        {
            //TODO: if mouse goes back left triangles are built backwards and are not visible
            //    so always keep center of the screen as x position


            m_mousePosition = Input.mousePosition;
            m_mousePosition.z = -GetComponent<Transform>().position.z;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            //Debug.Log(m_mousePosition);

            buildMesh();
            m_time = 0;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            GameObject groundPiece = new GameObject();
            groundPiece.AddComponent<MeshFilter>();
            groundPiece.AddComponent<MeshRenderer>();

            Mesh m = new Mesh();
            m.name = "groundPiece";

            groundPiece.GetComponent<MeshFilter>().mesh = m;
            groundPiece.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));

            ground.Add(groundPiece);
            m_currentGroundPiece = groundPiece;
        }
    }

    private void moveGroundLeft()
    {
        for (int i = 0; i < ground.Count; i++)
        {
            ground[i].GetComponent<Transform>().Translate(Vector2.left * Time.deltaTime);
        }
    }

    private void buildMesh()
    {
        m_bottomPoint.x = m_mousePosition.x;

        // copy vertices
        int verticesLength = m_currentGroundPiece.GetComponent<MeshFilter>().mesh.vertices.Length; // previous vertices array length
        Vector3[] vertices = new Vector3[verticesLength + 2]; // new vertices array

        Array.Copy(m_currentGroundPiece.GetComponent<MeshFilter>().mesh.vertices,
                   //m_currentGroundPiece.GetComponent<MeshFilter>().mesh.vertices.GetLowerBound(0),
                   vertices,
                   //vertices.GetUpperBound(0),
                   verticesLength);
        vertices[verticesLength] = new Vector3(m_mousePosition.x, -1, 0);
        vertices[verticesLength + 1] = new Vector3(m_mousePosition.x, m_mousePosition.y, 0);

        // copy indices
        MeshFilter meshFilter = m_currentGroundPiece.GetComponent<MeshFilter>();
        int indicesLength = meshFilter.mesh.triangles.Length; // previous indices array length
        int[] indices = new int[indicesLength + 6]; // new vertices array

        Array.Copy(m_currentGroundPiece.GetComponent<MeshFilter>().mesh.triangles,
                   //m_currentGroundPiece.GetComponent<MeshFilter>().mesh.triangles.GetLowerBound(0),
                   indices,
                   //indices.GetUpperBound(0),
                   indicesLength);
        try
        {
            if (indicesLength > 0)
            {
                indices[indicesLength] = indices[indicesLength - 1];
                indices[indicesLength + 1] = indices[indicesLength - 1] + 1;
                indices[indicesLength + 2] = indices[indicesLength - 1] + 3;
                indices[indicesLength + 3] = indices[indicesLength - 1];
                indices[indicesLength + 4] = indices[indicesLength - 1] + 3;
                indices[indicesLength + 5] = indices[indicesLength - 1] + 2;
            }
            else
            {
                indices[indicesLength] = 0;
                indices[indicesLength + 1] = 1;
                indices[indicesLength + 2] = 3;
                indices[indicesLength + 3] = 0;
                indices[indicesLength + 4] = 3;
                indices[indicesLength + 5] = 2;
            }
            
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("zizi");
        }







        m_currentGroundPiece.GetComponent<MeshFilter>().mesh.vertices = vertices;
        try
        {
            m_currentGroundPiece.GetComponent<MeshFilter>().mesh.triangles = indices;
        }
        catch
        {
            Debug.Log("zizi");
        }

        m_currentGroundPiece.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        m_currentGroundPiece.GetComponent<MeshFilter>().mesh.RecalculateBounds();
    }
}
