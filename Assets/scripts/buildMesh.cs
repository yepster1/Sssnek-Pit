 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        //Create verteces
        Vector3 [] verteces = new Vector3[]{//front face
        new Vector3(-1.0f,1.0f,1.0f), //Left top front 0
        new Vector3(1.0f,1.0f,1.0f), //right top front 1
        new Vector3(-1.0f, -1.0f,1.0f), //left bottom front 2
        new Vector3(1.0f,-1.0f,1.0f),  //right bottom frnt 3

        // //Back face
        // new Vector3(1.0f,1.0f,-1.0f),//Right top back 4
        // new Vector3(-1.0f,1.0f,-1.0f),//LEft top back 5
        // new Vector3(1.0f,-1.0f,-1.0f),//left bottom front 6
        // new Vector3(-1.0f,-1.0f,-1.0f),//right bottom front 7

        // //Left face

        // new Vector3(1.0f,1.0f,-1.0f),//Left top back
        // new Vector3(1.0f,1.0f,-1.0f),//left top front
        // new Vector3(1.0f,1.0f,-1.0f),//left bottom back
        // new Vector3(1.0f,1.0f,-1.0f),//left bottom front
        };



        //Draw trianges clockwise to determine visibilities
        int[] triangles = new int[]{
            0,2,3,//First triangle
            3,1,0//Second triangle
        };
        
        //UVs
         Vector2[] uvs = new Vector2[]{
             //Front face 0,0 is bottom left, 1,1 is top right
             new Vector2(0,1),
             new Vector2(0,0),
             new Vector2(1,1),
             new Vector2(1,0)
         };

        //Set up mesh
         mesh.Clear();
         mesh.vertices = verteces;
         mesh.triangles = triangles;
         mesh.uv = uvs;
         mesh.Optimize();
         mesh.RecalculateNormals();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
