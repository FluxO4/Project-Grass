using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base_Controller : MonoBehaviour
{
    public Player_Controller_4 playerController;

    void OnTriggerStay(Collider other){
        if (!playerController.isplayergrounded)
        {
            playerController.groundImpact();
        }
        //Debug.Log("GROUNDED");}
    }

    void OnTriggerExit(Collider other){

        playerController.isplayergrounded = false;

        //Debug.Log("IN AIR");
    }

}
