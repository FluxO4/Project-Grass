using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base_Controller : MonoBehaviour
{
    public Player_ControllerCC playerController;

    private void Start()
    {
        Debug.Log("player base exists");
    }
    void OnTriggerStay(Collider other){

        //if (other.tag != "Player")
        if (!playerController.isplayergrounded)
        {
            playerController.isplayergrounded = true;
        }
        Debug.Log("GROUNDED");
    }

    void OnTriggerExit(Collider other){

        playerController.isplayergrounded = false;

        Debug.Log("IN AIR");
    }

}
