using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public int ChunkSize = 10;
    public int NumGrassInChunk = 1000;


    private void Awake()
    {
        Game.mainController = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Game { 
    public static MainController mainController;
    public static ProceduralGrass proceduralGrass;

}