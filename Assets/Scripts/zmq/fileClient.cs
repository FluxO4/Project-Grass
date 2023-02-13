using UnityEngine;
using System.Collections;
using System.IO;

public class fileClient : MonoBehaviour
{
    private fileRequester _fileRequester;
    public JPGScreenSaver _JPGScreenSaver;

    private void Start()
    {
        _JPGScreenSaver.takeSS();
        _fileRequester = new fileRequester();
        _fileRequester._fileClient = this;
        _fileRequester.Start();
    }
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            print("working??????");
            _JPGScreenSaver.takeSS();
            _fileRequester.Start();

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