using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;


    public Material grassMaterial;
    public Material landMaterial;

    Vector3[] vertices; 
    int[] triangles;



    public float xpos = 0;
    public float zpos = 0;
    public int step = 2;
    //public int xSize = 20, zSize = 20;
    public int cSize = 20;
    public float scale = 0.39f;

    void Start(){
        xpos = transform.position.x;
        zpos = transform.position.z;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Generate() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        CreateShape();  //make this changeable form UI??
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }

    public void EnableGrass() {
        Material[] t = { grassMaterial, landMaterial };
        meshRenderer.materials = t;
    }

    public void DisableGrass() {
        Material[] t = { landMaterial };
        meshRenderer.materials = t;
    }

    private void Update(){
        //UpdateMesh();
    }

    // void-return function made coroutine for the delay thingy 
    void CreateShape(){
        int numx = cSize / step;
        int numz = cSize / step;


        vertices = new Vector3[(numx +1)* (numz+1)];
        

        // creates vertices array of the given size 
        for(int i = 0, z = 0; z <= numz; z++){
            for(int x = 0; x <= numx; x++){
                float y = Mathf.PerlinNoise((x*step+xpos)*scale,(z*step + zpos)*scale) * 3f + Mathf.PerlinNoise((x*step + xpos) * scale *0.5f, (z*step + zpos) * scale*0.5f) * 10f; 
                
                // float y = GenerateNoiseMap(x, z, scale);
                vertices[i] = new Vector3(x*step, y, z*step);
                i++;
            }
        }

        // makes 2 triangle meshes in each run using the vertices in clockwise direction
        int m = 0, tris=0;
        triangles = new int[(numx) * (numz) * 6];
        for(int z = 0; z< numz; z++){
            for(int x=0; x< numx; x++){  
            
            triangles[tris] = m;
            triangles[tris+1] = numx + m +1;
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