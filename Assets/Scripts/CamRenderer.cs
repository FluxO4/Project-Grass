using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using Unity.Barracuda;

public class CamRenderer : MonoBehaviour
{
    // Start is called before the first frame update

    public NNModel grassGenerator;
    public Model runtimeModel;
    // public NNModel concat;
    IWorker worker;


    void Start()
    {
        runtimeModel = ModelLoader.Load(grassGenerator);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var inputs = new Tensor(source, 3);

        worker.Execute(inputs);

        Tensor output = worker.PeekOutput();
        destination = output.ToRenderTexture();

        inputs.Dispose();
        output.Dispose();
    }
}
