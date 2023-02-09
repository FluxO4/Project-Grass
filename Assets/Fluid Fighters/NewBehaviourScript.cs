using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")){
            Physics.gravity = new Vector3(0, -1, 0);
        } else
        if(Input.GetKeyDown("2")){
            Physics.gravity = new Vector3(0, 1, 0);
        } else
        if(Input.GetKeyDown("3")){
            Physics.gravity = new Vector3(-1, 0, 0);
        } else
        if(Input.GetKeyDown("4")){
            Physics.gravity = new Vector3(1, 0, 0);
        } else
        if(Input.GetKeyDown("5")){
            Physics.gravity = new Vector3(0, 0, 1);
        } else
        if(Input.GetKeyDown("6")){
            Physics.gravity = new Vector3(0, 0, -1);
        }
    }
}
