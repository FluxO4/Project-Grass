using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

using System.IO;


public class barracuda : MonoBehaviour
{

    public Camera mainCam;
    public RenderTexture saveHelper;
    
    // public NNModel modelAsset;
    // private Model m_RuntimeModel;
    // var worker = WorkerFactory.CreateWorker(< WorkerFactory.Type >, m_RuntimeModel);
    void Start()
    {
        mainCam = Camera.main;
        // m_RuntimeModel = ModelLoader.Load(modelAsset);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("p"))
            takeDepth();
        if (Input.GetKeyUp("o")){
            takeView();
            
        }
        if(Input.GetKeyUp("i"))
            takeNormals();
    }

    
    public void takeView()
    {
        mainCam.targetTexture = saveHelper;
        RenderTexture.active = saveHelper;

        mainCam.Render();

        int width = saveHelper.width;
        int height = saveHelper.height;
        //Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        //// Read the screen contents into the texture
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //tex.Apply();

        //print(saveHelper.depth);
        // Create a Tensor object from the TensorData and metadata
        Tensor inputTensor = new Tensor(saveHelper);

        TensorShape tens_shape = new TensorShape(1, 10, 10, 4);

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));

        //WARNING! DO NOT RUN THIS THING BELOW UNLESS YOU HAVE SET THE RENDER TEXTURE TO BE 10x10 size, or it will crash unity
        /*var t = inputTensor.data.Download(tens_shape);
        int ind = 0;


        for (int r = 0; r < 10; r++)
        {
            for (int i = 0; i < 10; i++)
            {
                string currentline = "";
                for (int ii = 0; ii < 4; ii++)
                {
                    currentline += "  " + ((int)(t[ind]*100)).ToString();

                    ind++;
                }
                print(currentline);
            }
            print("");
        }
                    */

        //Destroy(tex);
        mainCam.targetTexture = null;

        //File.WriteAllBytes(path, Bytes);
    }
    public void takeNormals(){
        Tensor normalMapTensor = mainCam.GetComponent<normalmap_maker>().NormalMapSave();
        print(normalMapTensor);

        TensorShape tens_shape = new TensorShape(1, 100, 100, 4);

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));

        //WARNING! DO NOT RUN THIS THING BELOW UNLESS YOU HAVE SET THE RENDER TEXTURE TO BE 10x10 size, or it will crash unity
        var t = normalMapTensor.data.Download(tens_shape);
        int ind = 0;


        for (int r = 0; r < 100; r++)
        {
            for (int i = 0; i < 100; i++)
            {
                string currentline = "";
                for (int ii = 0; ii < 4; ii++)
                {
                    currentline += "  " + ((int)(t[ind]*100)).ToString();

                    ind++;
                }
                print(currentline);
            }
            print("");
        }
    }
    public void takeDepth()
    {
        Tensor _in_depth=mainCam.GetComponent<depth_maker>().Save();
        print(_in_depth);

        TensorShape tens_shape = new TensorShape(1, 100, 100, 4);

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));

        //WARNING! DO NOT RUN THIS THING BELOW UNLESS YOU HAVE SET THE RENDER TEXTURE TO BE 10x10 size, or it will crash unity
        var t = _in_depth.data.Download(tens_shape);
        int ind = 0;


        for (int r = 0; r < 100; r++)
        {
            for (int i = 0; i < 100; i++)
            {
                string currentline = "";
                for (int ii = 0; ii < 4; ii++)
                {
                    currentline += "  " + ((int)(t[ind]*100)).ToString();

                    ind++;
                }
                print(currentline);
            }
            print("");
        }
                    

        //Destroy(tex);


        mainCam.targetTexture = null;

        //mainCam.depthTextureMode = DepthTextureMode.Depth;
        //RenderTexture.active = saveHelper;
        //mainCam.Render();

        //int width = saveHelper.width;
        //int height = saveHelper.height;
        //Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        //// Read the screen contents into the texture
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //tex.Apply();
        ////RenderTexture.active = currentRT;
        //print(width);
        //print(height);
        //var Bytes = tex.EncodeToPNG();
        //Destroy(tex);
        //mainCam.targetTexture = null;
        //print(Bytes);
        ////File.WriteAllBytes(@"./Assets/TrainingData", Bytes);
        //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", Bytes);
    }
}