using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Controller : MonoBehaviour
{

    public Vector2 moveForwardTimeRange = new Vector2(1,10);
    public Vector2 stopTimeRange = new Vector2(1,3);

    public Vector2 cameraUpRange = new Vector2(-1, 2);
    public Vector2 cameraRightRange = new Vector2(-1, 2);

    public Vector2 cameraUpTimeRange = new Vector2(0, 2);
    public Vector2 cameraRightTimeRange = new Vector2(0, 2);

    public float CameraUp = 0;
    public float CameraRight = 0;
    float CameraUpDelta = 0;
    float CameraRightDelta = 0;

    public bool movingForward = false;
    public bool running = false;

    IEnumerator moveForwardTimer()
    {
        for (; ; )
        {
            movingForward = true;
            yield return new WaitForSeconds(Random.Range(moveForwardTimeRange.x, moveForwardTimeRange.y));
            movingForward = false;
            yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

        }

    }

    IEnumerator cameraRightTimer()
    {
        for (; ; )
        {
            CameraRight = Random.Range(cameraRightRange.x, cameraRightRange.y);
            yield return new WaitForSeconds(Random.Range(cameraRightTimeRange.x, cameraRightTimeRange.y));
            CameraRight = 0;
            yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

        }

    }

    IEnumerator cameraUpTimer()
    {
        for (; ; )
        {
            CameraUp = Random.Range(cameraUpRange.x, cameraUpRange.y);
            yield return new WaitForSeconds(Random.Range(cameraUpTimeRange.x, cameraUpTimeRange.y));
            CameraUp = 0;
            yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

        }

    }

    public void StartMe()
    {
        running = true;
        StartCoroutine(moveForwardTimer());
        StartCoroutine(cameraRightTimer());
        StartCoroutine(cameraUpTimer());
    }

    public void StopMe()
    {
        running = false;
        movingForward = false;
        CameraUp = 0;
        CameraRight = 0;
        StopAllCoroutines();
    }


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (!running)
            {
                StartMe();

            }
            else
            {
                StopMe();
            }
        }

       /* if (running)
        {
            CameraUp += CameraUpDelta;
            CameraRight += CameraRightDelta;

        }*/
    }
}
