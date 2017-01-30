using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour {

    public GameObject ground;

    private void Awake()
    {
        Mesh m = new Mesh();
        m.name = "Scripted_Plane_New_Mesh";
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-1, -1, 0);
        vertices[1] = new Vector3(1, -1, 0);
        vertices[2] = new Vector3(1, 1, 0);
        vertices[3] = new Vector3(-1, 1, 0);
        m.vertices = vertices;

        //m.uv = new Vector2[4];
        //m.uv[0] = new Vector2(0, 0);
        //m.uv[1] = new Vector2(0, 1);
        //m.uv[2] = new Vector2(1, 1);
        //m.uv[3] = new Vector2(1, 0);

        int[] triangles = new int[] { 2, 1, 0, 3, 2, 0 };
        m.triangles = triangles;

        //m.RecalculateNormals();

        ground.AddComponent<MeshFilter>();
        ground.AddComponent<MeshRenderer>();
        ground.GetComponent<MeshFilter>().mesh = m;
        ground.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
