using UnityEngine;
using System.Collections;
using System.IO;

public class fileClient : MonoBehaviour
{
    private fileRequester _fileRequester;
    public JPGScreenSaver _JPGScreenSaver;
    public Camera mainCam;
    public RenderTexture camView;
    private void Start()
    {
        _fileRequester = new fileRequester();
        Debug.Log("Instance of fileRequester created");
        _fileRequester._fileClient = this;
        Debug.Log("FileClient connected to fileRequester");
        _fileRequester.Start();
        Debug.Log("fileRequester started");



    }
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            print("Taking screenshot");
            _JPGScreenSaver.takeSS();
            Debug.Log(_JPGScreenSaver.bytes.Length);
            print("Tetete");

        }
    }
    private void OnDestroy()
    {
        _fileRequester.Stop();
    }
    
    //public void toTheSocket()
    //{
    //    //hmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
    //    _fileRequester.image_byte = _JPGScreenSaver.bytes;
    //}
    
}