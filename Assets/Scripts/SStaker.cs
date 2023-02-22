using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SStaker : MonoBehaviour
{
    // Start is called before the first frame update

    Camera mainCam;
    public RenderTexture saveHelper;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){

            SaveCurrentView("D:\\ttt.jpg");
        }
    }

    void SaveCurrentView(string path)
    {
        mainCam.targetTexture = saveHelper;
        RenderTexture.active = saveHelper;

        mainCam.Render();

        int width = saveHelper.width;
        int height = saveHelper.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read the screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        //RenderTexture.active = currentRT;

        var Bytes = tex.EncodeToJPG(100);
        Destroy(tex);
        mainCam.targetTexture = null;

        File.WriteAllBytes(path, Bytes);
    }
}
