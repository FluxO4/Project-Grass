using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class fileRequester : RunAbleThread
{
    
    public fileClient _fileClient;
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:8888");
            Debug.Log("Connected to localhost 8888");
            string message = null;
            for (int i = 0; i < 10 && Running; i++)
            {
                //Debug.Log("Sending file");
                //File.WriteAllBytes("D:/Project Grass/SavedScreen.jpg", _fileClient._JPGScreenSaver.bytes);

                ////client.SendFrame(_fileClient._JPGScreenSaver.height.ToString());
                ////client.SendFrame(_fileClient._JPGScreenSaver.width.ToString());
                ////client.SendFrame(_fileClient._JPGScreenSaver.bytes);
                //client.SendFrame("hello");
                // ReceiveFrameString() blocks the thread until you receive the string, but TryReceiveFrameString()
                // do not block the thread, you can try commenting one and see what the other does, try to reason why
                // unity freezes when you use ReceiveFrameString() and play and stop the scene without running the server
                //                string message = client.ReceiveFrameString();
                //                Debug.Log("Received: " + message);
                
                bool gotMessage = false;

                if (message == "GIB")
                {
                    Debug.Log("Sending image");
                    //_fileClient._JPGScreenSaver.takeSS();
                    //File.WriteAllBytes("D:/Project Grass/SavedScreen.jpg", _fileClient._JPGScreenSaver.bytes);

                    //client.SendFrame(_fileClient._JPGScreenSaver.height.ToString());
                    //client.SendFrame(_fileClient._JPGScreenSaver.width.ToString());
                    //client.SendFrame(_fileClient._JPGScreenSaver.bytes);
                    

                    client.SendFrame(_fileClient._JPGScreenSaver.bytes);
                    
                }
                else
                {
                    Debug.Log("Sending 999");
                    client.SendFrame("999");

                }

                Debug.Log("Waiting for response");
                while (Running)
                {
                    gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                    if (gotMessage) break;
                }
                


                if (gotMessage) Debug.Log("Received " + message);
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}