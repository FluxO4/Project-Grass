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

    // public string dataSavePath = "D:/TrainingData/";

    public bool saveTrainingdata = true;

    public int saveCounter = 0;
    public string header = "Index,Input Image Paths,Depth Image Paths,Optical Flow Image Paths,Output Image Paths,Px,Py,Pz,Vx,Vy,Vz,Rx,Ry,Rz,RVx,RVy,RVz,Lx,Ly,Lz";

    RenderTexture trainingDataRenderTexture;

    void Start()
    {
        mainCam = Camera.main;
        File.AppendAllText(Application.dataPath + "/TrainingData/InputVectors.csv", header+'\n');

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

            // Below comment saves as system date/time string format, to be checked if sequentially retrievable
            // SaveCurrentView(Application.dataPath + "/TrainingData/Inputs/"+ System.DateTime.Now.ToString("yyyyMMddHHmmss") + "_input.jpg");
            SaveCurrentView(Application.dataPath + "/TrainingData/Inputs/" + saveCounter + "_input.jpg");

            // Save the depth map
            mainCam.GetComponent<ImageSynthesis>().Save(saveCounter+"_synth", -1, -1, Application.dataPath + "/TrainingData/");
            // Save the optical flow map
            // Get player input values (using a function in playerController that Quartermaster Shell Rock will provide soon)

            string values = ""; 
            // Append player input values to a csv file in the dataSavePath
            File.AppendAllText(Application.dataPath + "/TrainingData/InputVectors.csv", values+'\n');

            terrainGenerator.EnableGrass();
            mainCam.Render();

            // Save the current view again, identified as 'output'
            SaveCurrentView(Application.dataPath + "/TrainingData/Outputs/" + saveCounter + "_output.jpg");

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
