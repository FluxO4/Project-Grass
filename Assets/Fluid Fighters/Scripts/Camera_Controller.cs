using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject head;
    public Random_Controller ran;
    //float pi = 3.141592653589793f;
    public int cameraspeed = 10;

    void Start()
    {
        Input.ResetInputAxes();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape")){
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(1)){
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButtonUp(1)){
            Cursor.lockState = CursorLockMode.Locked;
        }

        float h = 5f * Input.GetAxis("Mouse X") * cameraspeed * 0.001f;



        float v = -2.5f * Input.GetAxis("Mouse Y") * cameraspeed * 0.001f;
        //if (Input.GetKey("f")){h = 0; v = 0;}

        if (ran) {
            if (ran.running)
            {
                h = 5f * ran.CameraRight * cameraspeed * 0.001f;
                v = -2.5f * ran.CameraUp * cameraspeed * 0.001f;
            }
        }



      if(Input.GetMouseButton(1)){
        player.transform.Rotate(Vector3.up, h * 5, Space.Self);
          //head.transform.localEulerAngles = new Vector3(head.transform.localEulerAngles.x, head.transform.localEulerAngles.y + h * Time.deltaTime * 5, head.transform.localEulerAngles.z);
      }
      else{
          //player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y + h * Time.deltaTime * 20, player.transform.localEulerAngles.z);
          //head.transform.localEulerAngles = new Vector3(head.transform.localEulerAngles.x, head.transform.localEulerAngles.y + h * Time.deltaTime * 20, head.transform.localEulerAngles.z);
          player.transform.Rotate(Vector3.up, h  * 20, Space.Self);
      }

      //head.transform.localEulerAngles = new Vector3(head.transform.localEulerAngles.x + v * Time.deltaTime * 20, head.transform.localEulerAngles.y , head.transform.localEulerAngles.z);

      if(!Input.GetMouseButton(1)){
            /*if(head.transform.localEulerAngles.x + v * 20 > 70 && head.transform.localEulerAngles.x + v * 20 <= 180){
                head.transform.localEulerAngles = new Vector3(70, head.transform.localEulerAngles.y , head.transform.localEulerAngles.z);
            }
            else
            if(head.transform.localEulerAngles.x + v  * 20 < 280 && head.transform.localEulerAngles.x + v * 20 > 180){
                head.transform.localEulerAngles = new Vector3(280, head.transform.localEulerAngles.y , head.transform.localEulerAngles.z);
            }
            else{
                head.transform.localEulerAngles = new Vector3(head.transform.localEulerAngles.x + v * 20, head.transform.localEulerAngles.y , head.transform.localEulerAngles.z);
            }*/
            Debug.Log(head.transform.localEulerAngles.x);
            float a = head.transform.localEulerAngles.x;
            a-= Mathf.Floor(a / 360.0f + 0.5f) * 360;

            if ((a+ v * 20) > 30)
            {
                v = 0;
            }

            if ((a + v * 20) < -30)
            {
                v = 0;
            }

            head.transform.Rotate(Vector3.right, v * 20, Space.Self);
        }
    }
}
