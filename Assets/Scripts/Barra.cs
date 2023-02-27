using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using Unity.Barracuda;

public class Barra : MonoBehaviour
{
    // Start is called before the first frame update
    
	public Shader uberReplacementShader;
    public Camera mainCam;
    public Camera depthCam;

    //public Tensor[] inputs = new Tensor[2];

    public RenderTexture viewTexture;
    public RenderTexture depthTexture;
    public RenderTexture outputTexture;

    public NNModel grassGenerator;
    public Model runtimeModel;
    // public NNModel concat;
    IWorker worker;

    void Start()
    {
        var cb = new CommandBuffer();
        cb.SetGlobalFloat("_OutputMode", 2);
        depthCam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
        depthCam.AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);
        depthCam.SetReplacementShader(uberReplacementShader, "");


        runtimeModel = ModelLoader.Load(grassGenerator);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);
    }

    public void takeView()
    {
        mainCam.Render();
        //var inputs = new Dictionary<string, Tensor>();

        //inputs["onnx::Concat_1"] = new Tensor(viewTexture, 3);
        //print(inputs["onnx::Concat_1"]);

        //inputs["onnx::Concat_0"] = new Tensor(depthTexture, 1);
        //print(inputs["onnx::Concat_0"]);

        var inputs = new Tensor(viewTexture, 3);

        worker.Execute(inputs);

        Tensor output = worker.PeekOutput();
        print(output);

        output.ToRenderTexture(outputTexture);

    }


    // Update is called once per frame
    void LateUpdate()
    {
        //if (Input.GetKeyDown("=")) 
        {
            takeView();
        }
    }

    

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
    }
}
