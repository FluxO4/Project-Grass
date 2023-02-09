using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    ComputeBuffer grassPositions;

    private void OnEnable()
    {
        grassPositions = new ComputeBuffer(Game.mainController.NumGrassInChunk,3*4);
    }

    private void OnDisable()
    {
        grassPositions.Release();
        grassPositions=null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
