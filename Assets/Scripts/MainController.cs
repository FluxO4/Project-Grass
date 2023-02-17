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
            InputVectors _inputVectors = playerController.getInputVectors();
            print(_inputVectors.position[0]);
            mainCam.Render();

            // Below comment saves as system date/time string format, to be checked if sequentially retrievable
            // SaveCurrentView(Application.dataPath + "/TrainingData/Inputs/"+ System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_input.jpg");
            
            string synthPath = "./Assets/TrainingData/";
            string pathOFlow = synthPath + "OFlow/" + saveCounter + "_synth_oflow.png";  
            string pathDepth = synthPath + "Depth/" + saveCounter + "_synth_depth.png"; 

            string inputImageWithPath = Application.dataPath + "/TrainingData/Inputs/" + saveCounter + "_input.jpg";
            string outputImageWithPath = Application.dataPath + "/TrainingData/Outputs/" + saveCounter + "_output.jpg";

            // Save the current view, identified as 'input'
            SaveCurrentView(inputImageWithPath);

            // Save the depth map and optical flow map
            mainCam.GetComponent<ImageSynthesis>().Save(saveCounter+"_synth", -1, -1, synthPath);
        
            // Append player input values to a csv file in the dataSavePath
            string values = "" + saveCounter +"," + inputImageWithPath + "," + pathDepth + "," + pathOFlow + "," + outputImageWithPath + "," + _inputVectors.position[0] + "," + _inputVectors.position[1] + "," + _inputVectors.position[2] + "," + _inputVectors.velocity[0] + "," +_inputVectors.velocity[1]+ "," +_inputVectors.velocity[2]+ "," +_inputVectors.rotation[0]+ "," +_inputVectors.rotation[1]+ "," + _inputVectors.rotation[2] + "," + _inputVectors.rotationalVelocity[0]+ "," + _inputVectors.rotationalVelocity[1]+ "," + _inputVectors.rotationalVelocity[2]+"," + _inputVectors.lightingDifference[0]+ "," + _inputVectors.lightingDifference[1]+ "," + _inputVectors.lightingDifference[2]; 
            File.AppendAllText(Application.dataPath + "/TrainingData/InputVectors.csv", values+'\n');

            terrainGenerator.EnableGrass();
            mainCam.Render();

            // Save the current view again, identified as 'output'
            SaveCurrentView(outputImageWithPath);

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
