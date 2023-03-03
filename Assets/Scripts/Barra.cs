using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using Unity.Barracuda;

public class Barra : MonoBehaviour
{
    // Start is called before the first frame update

    public Shader depthShader;
    public Shader normalsShader;
    public Camera mainCam;
    public Camera depthCam;
    public Camera normalscam;

    //public Tensor[] inputs = new Tensor[2];

    public RenderTexture viewTexture;
    public RenderTexture depthTexture;
    public RenderTexture outputTexture;
    public RenderTexture normalsTexture;

    public NNModel grassGenerator;
    public Model runtimeModel;
    // public NNModel concat;
    IWorker worker;

    void Start()
    {
        depthCam.SetReplacementShader(depthShader, "");
        depthCam.backgroundColor = Color.white;

        /*cb.SetGlobalFloat("_OutputMode", 4);
        normalscam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
        normalscam.AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);*/
        normalscam.SetReplacementShader(normalsShader, "");
        normalscam.backgroundColor = Color.black;


        runtimeModel = ModelLoader.Load(grassGenerator);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Compute, runtimeModel);
        foreach (var layer in runtimeModel.layers)
            Debug.Log(layer.name + " does " + layer.type);
    }

    public void takeView()
    {
        mainCam.Render();
        depthCam.Render();
        normalscam.Render();
        var inputs = new Dictionary<string, Tensor>();

        inputs["onnx::Concat_0"] = new Tensor(viewTexture, 3);
        //print(inputs["onnx::Concat_0"]);

        inputs["onnx::Concat_1"] = new Tensor(depthTexture, 1);
        inputs["onnx::Concat_2"] = new Tensor(normalsTexture, 3);
        //print(inputs["onnx::Concat_1"]);
        //print(inputs["onnx::Concat_2"]);
        //Debug.Break();

        //var inputs = new Tensor(viewTexture, 3);

        worker.Execute(inputs);

        Tensor output = worker.PeekOutput();
        output.ToRenderTexture(outputTexture);

        inputs["onnx::Concat_0"].Dispose();
        inputs["onnx::Concat_1"].Dispose();
        inputs["onnx::Concat_2"].Dispose();

        output.Dispose();

    }


    // Update is called once per frame
    void LateUpdate()
    {
        //if (Input.GetKeyDown("=")) 
        {
            takeView();
        }
    }


}
