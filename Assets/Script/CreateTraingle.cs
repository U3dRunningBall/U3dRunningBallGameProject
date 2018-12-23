using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class triangle_mesh : MonoBehaviour
{
    /*
    creat a triangle by using Mesh 
     2016/11/19
                 ————Carl    
    */

    void Start()
    {
        MeshFilter meshfilter = new MeshFilter();
        meshfilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 0, 1);
        vertices[2] = new Vector3(1, 0, 0);
        int[] tris = new int[3];
        tris[0] = 0; tris[1] = 1; tris[2] = 2;
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = Vector2.zero;
        }

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.uv = uvs;
        meshfilter.mesh = mesh;
    }
}