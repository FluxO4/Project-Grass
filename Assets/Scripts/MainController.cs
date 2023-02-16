using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player_ControllerCC playerController;
    public TerrainGenerator terrainGenerator;
    public Camera mainCam;
    public RenderTexture saveHelper;

    //public Vector2Int saveResolution = new Vector2Int(800, 480);

    public string dataSavePath = "D:/TrainingData/";

    public bool saveTrainingdata = false;

    public int saveCounter = 0;

    RenderTexture trainingDataRenderTexture;

    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("g"))
        {
            if (!terrainGenerator.grassEnabled)
            {
                terrainGenerator.EnableGrass();
            }
            else
            {
                terrainGenerator.DisableGrass();
            }
        }

        if (Input.GetKeyUp("t"))
        {
            saveTrainingdata = ! saveTrainingdata;
        }


        if (Input.GetKeyUp("l") || saveTrainingdata)
        {

            mainCam.Render();

            // Save the current view, identified as 'input'   , maybe replace saveCounter by by date-time with this: System.DateTime.Now.ToString() , but problem is that it contains the '/' character which will need to be removed
            SaveCurrentView(dataSavePath + saveCounter + "_input.jpg");

            // Save the depth map
            // Save the optical flow map
            // Get player input values (using a function in playerController that Quartermaster Shell Rock will provide soon)
            // Append player input values to a csv file in the dataSavePath

            terrainGenerator.EnableGrass();
            mainCam.Render();

            // Save the current view again, identified as 'output'
            SaveCurrentView(dataSavePath + saveCounter + "_output.jpg");

            terrainGenerator.DisableGrass();

            saveCounter++;
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

        var Bytes = tex.EncodeToJPG();
        Destroy(tex);
        mainCam.targetTexture = null;

        File.WriteAllBytes(path, Bytes);
    }
}
