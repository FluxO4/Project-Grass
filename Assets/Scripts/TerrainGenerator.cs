using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject player;

    int xPos
    {
        get {
            return (int)(player.transform.position.x / cSize)-1;
        }
    }

    int yPos
    {
        get
        {
            return (int)(player.transform.position.z / cSize)-1;
        }
    }



    public GameObject chunkPrefab;
    public int cSize= 20;

    public int generateRadius = 5;


    public Dictionary<Vector2Int,MeshGenerator> generatorList = new Dictionary<Vector2Int,MeshGenerator>();

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(terrainUpdate());
    }

    void GenerateInRadius(int radius) {
        for (int x = xPos-radius; x <= xPos + radius; x++) {
            for (int z = yPos-radius; z <= yPos + radius; z++)
            {

                if (Mathf.Pow(x-xPos, 2) + Mathf.Pow(z - yPos, 2) <= radius * radius) {
                    if (generatorList.ContainsKey(new Vector2Int(x, z)))
                    {


                    }
                    else {
                        if (generatorList.Count < 1000)
                        {
                            GameObject t = Instantiate(chunkPrefab, new Vector3(x * cSize, 0, z * cSize), Quaternion.identity);

                            t.name = x.ToString() + " " + z.ToString() + " Chunk";
                            MeshGenerator tmg = t.GetComponent<MeshGenerator>();
                            tmg.cSize = cSize;
                            tmg.xpos = x * cSize;
                            tmg.zpos = z * cSize;
                            tmg.Generate();
                            generatorList[new Vector2Int(x, z)] = tmg;
                        }
                    }
                }
            }
        }

    }

    void ClearFromOutsideRadius(int radius)
    {
        List<Vector2Int> removeKeys = new List<Vector2Int>();
        foreach (Vector2Int key in generatorList.Keys) {
            if ((key - new Vector2Int(xPos, yPos)).sqrMagnitude > radius * radius) {
                GameObject t = generatorList[key].gameObject;
                
                removeKeys.Add(key);
// generatorList.Remove(key);
                Destroy(t);
            }
        }

        foreach (Vector2Int key in removeKeys)
        {
            generatorList.Remove(key);
        }

    }

    public bool grassEnabled = false;
    public void EnableGrass()
    {
        Debug.Log("Enabled Grass");
        grassEnabled = true;
        foreach (Vector2Int key in generatorList.Keys)
        
        {
            generatorList[key].EnableGrass();
        }

    }

    public void DisableGrass()
    {
        Debug.Log("Disabled Grass");
        grassEnabled = false;
        foreach (Vector2Int key in generatorList.Keys)

        {
            generatorList[key].DisableGrass();
        }

    }

    IEnumerator terrainUpdate()
    {
        for(; ; )
        {
            ClearFromOutsideRadius(generateRadius);
            GenerateInRadius(generateRadius);

            yield return new WaitForSeconds(2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
