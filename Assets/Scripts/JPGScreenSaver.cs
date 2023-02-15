// Saves screenshot as JPG file.
using UnityEngine;
using System.Collections;
using System.IO;

public class JPGScreenSaver : MonoBehaviour
{
    public byte[] bytes;
    //public int width = Screen.width;
    public int width = 200;
    //public int height = Screen.height;
    public int height = 200;
    public fileClient _fileClient;
    // Take a shot immediately
    //IEnumerator Start()
    //{
    //    yield return SaveScreenJPG();
    //}
    void Start()
    {
        //StartCoroutine(SaveScreenJPG());
    }
    void Update()
    {
        //if (Input.GetKeyDown("c"))
        //{
        //    print("working??????");
        //    StartCoroutine(SaveScreenJPG());

        //}
    }
  
    IEnumerator SaveScreenJPG()
    {
        // Read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture in RGB24 format the size of the screen
        width = Screen.width;
        height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read the screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode the texture in raw format , not JPG format
        bytes = tex.GetRawTextureData();

        Object.Destroy(tex);
        // Write the returned byte array to a file in the project folder
        Debug.Log("Finished taking screenshot");
        //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
    }

    public void takeSS()
    {
        StartCoroutine(SaveScreenJPG());
    }
}