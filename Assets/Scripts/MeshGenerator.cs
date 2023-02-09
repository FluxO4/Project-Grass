using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices; 
    int[] triangles;
    public int xSize = 20, zSize = 20;
    public float scale = 0.39f;

    void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();  //make this changeable form UI??
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }

    private void Update(){
        //UpdateMesh();
    }

    // void-return function made coroutine for the delay thingy 
    void CreateShape(){
        vertices = new Vector3[(xSize+1)* (zSize+1)];

        // creates vertices array of the given size 
        for(int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                float y = Mathf.PerlinNoise(x*.3f,z*.3f) * 2f; 
                // float y = GenerateNoiseMap(x, z, scale);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // makes 2 triangle meshes in each run using the vertices in clockwise direction
        int m = 0, tris=0;
        triangles = new int[xSize * zSize * 6];
        for(int z = 0; z< zSize; z++){
            for(int x=0; x< xSize; x++){  
            
            triangles[tris] = m;
            triangles[tris+1] = xSize + m +1;
            triangles[tris+2] = ++m;

            // yield return new WaitForSeconds(.1f);

            triangles[tris+3] = triangles[tris+2];
            triangles[tris+4] = triangles[tris+1];        
            triangles[tris+5] = triangles[tris+4]+1;

            tris+=6;

                //delay for placing the triangles
// yield return new WaitForSeconds(.005f);
            }

            m++;
        }
        
    }

    // puts spheres on the coordinates
    private void OnDrawGizmos(){
        /*if (vertices == null){
            return;
        }

        for(int i = 0; i<vertices.Length; i++)
            Gizmos.DrawSphere(vertices[i], .1f);*/
    }

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    // TBD: will contain the Perlin noise generated Y-values with layered octaves
    float GenerateNoiseMap(int x, int z, float scale) {
	    // float[,] noiseMap = new float[mapWidth,mapHeight];
	    float perlinY;
	    if (scale <= 0) {
		    scale = 0.0001f;
	    }

	
	    float sampleX = x / scale;
	    float sampleZ = z / scale;

	    perlinY = Mathf.PerlinNoise (sampleX, sampleZ);
	    // noiseMap [x, y] = perlinValue;


	    return perlinY;
    }

}