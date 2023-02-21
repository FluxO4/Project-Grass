using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

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
        takeImage();
    }
    public void takeImage()
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


        var encoder = new TextureAsTensorData(tex);

        // Convert the input texture to a TensorData object
        //TensorData tensorData = TextureAsTensorData(tex);

        // Define the shape of the tensor
        int tens_height = tex.height;
        int tens_width = tex.width;
        int channels = 3; // RGBA
        int[] shape = new int[] { 1, tens_height, tens_width, channels };

        // Create a Tensor object from the TensorData and metadata
        Tensor inputTensor = new Tensor(shape, encoder, "input");

        //Tensor inputTensor = new Tensor(TextureAsTensorData(tex));
        print(inputTensor);
        Destroy(tex);
        mainCam.targetTexture = null;

        //File.WriteAllBytes(path, Bytes);
    }
}
