using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

using System.IO;


public class barracuda : MonoBehaviour
{

    public Camera mainCam;
    public RenderTexture saveHelper;
    public Tensor inputTensorView;
    public Tensor inputTensorDepth;
    public Tensor inputTensorNormal;
    public Tensor inputTensor;
    private int width;
    private int height; 
    // public NNModel modelAsset;
    // private Model m_RuntimeModel;
    // var worker = WorkerFactory.CreateWorker(< WorkerFactory.Type >, m_RuntimeModel);

    //private void OnRenderImage() {
    //    takeView();
    //    takeDepth();
    //    concat_Tensor();
    //}
    
    void Start()
    {
        width = saveHelper.width;
        height = saveHelper.height;
        inputTensor = new Tensor(1,height, width,5);
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
        if (Input.GetKeyUp("u"))
            concat_Tensor();
    }

    
    public void takeView()
    {
        mainCam.targetTexture = saveHelper;
        RenderTexture.active = saveHelper;

        mainCam.Render();

        inputTensorView = new Tensor(saveHelper,3);
        print(inputTensorView);
        //TensorShape tens_shape = new TensorShape(1, 10, 10, 4);

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
        inputTensorNormal = mainCam.GetComponent<normalmap_maker>().NormalMapSave(width,height);
        print(inputTensorNormal);

        //TensorShape tens_shape = new TensorShape(1, 100, 100, 4);

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));

        //WARNING! DO NOT RUN THIS THING BELOW UNLESS YOU HAVE SET THE RENDER TEXTURE TO BE 10x10 size, or it will crash unity
        // var t = normalMapTensor.data.Download(tens_shape);
        // int ind = 0;


        // for (int r = 0; r < 100; r++)
        // {
        //     for (int i = 0; i < 100; i++)
        //     {
        //         string currentline = "";
        //         for (int ii = 0; ii < 4; ii++)
        //         {
        //             currentline += "  " + ((int)(t[ind]*100)).ToString();

        //             ind++;
        //         }
        //         print(currentline);
        //     }
        //     print("");
        // }
    }
    public void takeDepth()
    {
        inputTensorDepth = mainCam.GetComponent<depth_maker>().Save(width,height);
        print(inputTensorDepth);

        //TensorShape tens_shape = new TensorShape(1, 100, 100, 4);

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));

        ////WARNING! DO NOT RUN THIS THING BELOW UNLESS YOU HAVE SET THE RENDER TEXTURE TO BE 10x10 size, or it will crash unity
        //var t = _in_depth.data.Download(tens_shape);
        //int ind = 0;


        //for (int r = 0; r < 100; r++)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        string currentline = "";
        //        for (int ii = 0; ii < 4; ii++)
        //        {
        //            currentline += "  " + ((int)(t[ind]*100)).ToString();

        //            ind++;
        //        }
        //        print(currentline);
        //    }
        //    print("");
        //}


        //Destroy(tex);

        mainCam.targetTexture = null;
    }

    public void concat_Tensor()
    {
        print("depth: "+ inputTensorDepth);
        print("view: "+ inputTensorView);
        print("normal: "+ inputTensorNormal);
        //print("wtf"+TensorExtensions.Concat(new Tensor[] { inputTensorDepth, inputTensorView },2));
        //inputTensor = inputTensorView.Concat(inputTensorDepth, 2);
    }
}