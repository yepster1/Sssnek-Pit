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

        //Back face
        new Vector3(1.0f,1.0f,-1.0f),//Right top back 4
        new Vector3(-1.0f,1.0f,-1.0f),//LEft top back 5
        new Vector3(1.0f,-1.0f,-1.0f),//right bottom back 6
        new Vector3(-1.0f,-1.0f,-1.0f),//left bottom back 7

        //Left face

        new Vector3(-1.0f,1.0f,-1.0f),//Left top back 8
        new Vector3(-1.0f,1.0f,1.0f),//left top front 9
        new Vector3(-1.0f,-1.0f,-1.0f),//left bottom back 10
        new Vector3(-1.0f,-1.0f,1.0f),//left bottom front 11

        //Right face
        new Vector3(1.0f,1.0f,1.0f),//Right top front 12
        new Vector3(1.0f,1.0f,-1.0f),//right top back 13
        new Vector3(1.0f,-1.0f,1.0f),//right bottom front 14
        new Vector3(1.0f,-1.0f,- 1.0f),//right bottom back 15

        //Top face
        new Vector3(-1.0f,1.0f,-1.0f),//left top bacl, 16
        new Vector3(1.0f,1.0f,-1.0f),//right top back 17
        new Vector3(-1.0f,1.0f,1.0f),//left top front 18
        new Vector3(1.0f,1.0f,1.0f),//right top front 19

        //Bottom front face
        new Vector3(-1.0f,-1.0f,1.0f),//left bottom front
        new Vector3(1.0f,-1.0f,1.0f),//right bottom front
        new Vector3(-1.0f,-1.0f,-1.0f),//left bottom back
        new Vector3(1.0f,-1.0f,-1.0f)//right bottom back

        };



        //Draw trianges clockwise to determine visibilities
        int[] triangles = new int[]{
            //front face
            0,2,3,//First triangle
            3,1,0,//Second triangle

             //back face
            4,6,7,//First triangle
            7,5,4,//Second triangle

             //left face
            8,10,11,//First triangle
            11,9,8,//Second triangle

             //right face
            12,14,15,//First triangle
            15,13,12, //Second triangle
 
             //top face
            16,18,19,//First triangle
            19,17,16, //Second triangle

            //bottom face
            20,22,23,//First triangle
            23,21,20 //Second triangle
            
        };
        
        //UVs
         Vector2[] uvs = new Vector2[]{
             //Front face 0,0 is bottom left, 1,1 is top right
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

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
        transform.Rotate(new Vector3(1,5,3)*Time.deltaTime);
    }
}
