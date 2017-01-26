using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour {

    public float size;

    private void Awake()
    {
        Mesh m = new Mesh();
        m.name = "Scripted_Plane_New_Mesh";
        m.vertices = new Vector3[4];
        m.vertices[0] = new Vector3(-size, -size, .01f);
        m.vertices[1] = new Vector3(size, -size, .01f);
        m.vertices[2] = new Vector3(size, size, .01f);
        m.vertices[3] = new Vector3(-size, size, .01f);

        m.uv = new Vector2[4];
        m.uv[0] = new Vector2(0, 0);
        m.uv[1] = new Vector2(0, 1);
        m.uv[2] = new Vector2(1, 1);
        m.uv[3] = new Vector2(1, 0);

        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        m.RecalculateNormals();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
