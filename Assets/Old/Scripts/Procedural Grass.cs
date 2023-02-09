using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGrass : MonoBehaviour
{
    public GameObject landPrefab;

    private void Awake()
    {
        Game.proceduralGrass = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void fillChuckWithGrass(int x, int y, int numGrass = 1000) /// (x,y) = Chuck coordinates
    {
        Vector3 chuckCentre = new Vector3(x * Game.mainController.ChunkSize, 0, y * Game.mainController.ChunkSize);

        Vector3 randPos = (new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f))) * Game.mainController.ChunkSize * 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
