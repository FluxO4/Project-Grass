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

    public int saveCounter = 4531;
    public string header = "Index,Input Image Paths,Depth Image Paths,Normal Image Paths,Output Image Paths,Px,Py,Pz,Vx,Vy,Vz,Rx,Ry,Rz,RVx,RVy,RVz,Lx,Ly,Lz";

    RenderTexture trainingDataRenderTexture;

    public string savePath = "D:/TrainingData/";

    void Start()
    {
        mainCam = Camera.main;
        //File.AppendAllText(savePath + "train.csv", header+'\n');
        //Time.timeScale = 1.0f / 6.0f;
        //Application.targetFrameRate = 5;

        //Time.timeScale = 1.0f / 6.0f;
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void LateUpdate()
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
            //print(_inputVectors.position[0]);
            //mainCam.Render();

            // Below comment saves as system date/time string format, to be checked if sequentially retrievable
            // SaveCurrentView(Application.dataPath + "/TrainingData/Inputs/"+ System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_input.jpg");
            
            //string synthPath = "./Assets/TrainingData/";
            
            //string pathTag =  "./Tag/" + saveCounter + "_synth_tag.jpg";  
            string pathDepth =  "./Depth/" + saveCounter + "_synth_depth.jpg";
            string pathNormal = "./Normal/" + saveCounter + "_synth_normals.jpg";

            string inputImageWithPath = savePath + "Input/" + saveCounter + "_input.jpg";
            string outputImageWithPath = savePath + "Output/" + saveCounter + "_output.jpg";

            string inputImageRelativePath =  "./Input/" + saveCounter + "_input.jpg";
            string outputImageRelativePath =  "./Output/" + saveCounter + "_output.jpg";

            // Save the current view, identified as 'input'
            SaveCurrentView(inputImageWithPath);

            // Save the depth map and optical flow map
            mainCam.GetComponent<ImageSynthesis>().Save(saveCounter+"_synth", 800, 480, savePath);
        
            // Append player input values to a csv file in the dataSavePath
            string values = "" + saveCounter + "," + inputImageRelativePath + "," + pathDepth + "," + pathNormal + "," + outputImageRelativePath + "," + _inputVectors.position[0] + "," + _inputVectors.position[1] + "," + _inputVectors.position[2] + "," + _inputVectors.velocity[0] + "," +_inputVectors.velocity[1]+ "," +_inputVectors.velocity[2]+ "," +_inputVectors.rotation[0]+ "," +_inputVectors.rotation[1]+ "," + _inputVectors.rotation[2] + "," + _inputVectors.rotationalVelocity[0]+ "," + _inputVectors.rotationalVelocity[1]+ "," + _inputVectors.rotationalVelocity[2]+"," + _inputVectors.lightingDifference[0]+ "," + _inputVectors.lightingDifference[1]+ "," + _inputVectors.lightingDifference[2]; 
            File.AppendAllText(savePath + "train.csv", values+'\n');

            terrainGenerator.EnableGrass();
            //mainCam.Render();

            // Save the current view again, identified as 'output'

            SaveCurrentView(outputImageWithPath);

            terrainGenerator.DisableGrass();

            if (saveCounter % 50 == 0)
            {
                Debug.Log("Saved "+ saveCounter);
            }
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

        var Bytes = tex.EncodeToJPG(80);
        Destroy(tex);
        mainCam.targetTexture = null;

        File.WriteAllBytes(path, Bytes);
    }
}
