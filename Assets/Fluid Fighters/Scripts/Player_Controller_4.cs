
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_4 : MonoBehaviour
{

    public bool isplayergrounded {
        get {
            anim.SetBool("Falling", false);
            
            return _isplayergrounded;
        }
        set {
            anim.SetBool("Grounded", value);
            _isplayergrounded = value;
        }
    
    }

    public void groundImpact() {
        //anim.SetFloat("Impact Speed", impactspeedsq);
        isplayergrounded = true;
    }

    bool _isplayergrounded = false;


    string forwardkey = "w";
    string forwardkey2 = ",";

    string jumpkey = "space";
    string sprintkey = "left ctrl";
    //string supersprintkey = "f";
    //string backwardkey = "o";
    //string leftkey = "a";
    //string rightkey = "e";
    string drawswordkey = "e";
    string drawswordkey2 = ".";


    Rigidbody player;

    public Animator anim;

    public GameObject sword;
    public GameObject sheathedsword;


    public static Vector3 down = new Vector3(0, -1, 0);
    //private Vector3 playerorientation = new Vector3 (0, 1, 0);

    public float speed = 5f;
    public float jumpSpeed = 7f;
    public float gravity = 7.0f; // 5.0f

    public float maxwalkspeed = 10;
    public float maxsprintspeed = 20;
    public float maxspeed = 10;

    public int Weapon = 1;

    //public Animation anim;
    public GameObject head;
    public GameObject playerbase;

    public Camera Maincam;

    public bool fighting = false;
    bool holdHit = false;

    //bool fightingstance = false;
   // string backgroundanim = "breathing";

    //bool groundedfirstframe = false;

    private Vector3 moveForce = Vector3.zero;
    private CharacterController controller;

    //float mousex;
    //float pmousex;
    bool rightorleft = true;
    float jumpSetTime;
    int hitDirection = 0;
    int hitDirection2 = 0;

    IEnumerator jumpReset() {
        yield return new WaitForSeconds(0.2f);
        anim.ResetTrigger("Jump");
    }

    IEnumerator frontFlipReset()
    {
        yield return new WaitForSeconds(0.2f);
        anim.ResetTrigger("FrontFlip");
    }

    IEnumerator hitCooldown()
    {
        yield return new WaitForSeconds(0.01f);
        anim.ResetTrigger("Hit");
    }

    IEnumerator swithWeaponReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("SwitchWeapon");
    }

    IEnumerator holdSword(float waitTime = 0.88f)
    {
        yield return new WaitForSeconds(waitTime);
        if (fighting || (Weapon == 2 && fighting)) { 
            sword.SetActive(true);
            sheathedsword.SetActive(false);
        }
    }

    IEnumerator sheathSword()
    {
        yield return new WaitForSeconds(0.6f);
        if (!fighting || Weapon == 1)
        {
            sword.SetActive(false);
            sheathedsword.SetActive(true);
        }

    }


        void Update()
    {

        /*   if (Input.GetKeyDown("1")) {
               down = new Vector3(1, 0, 0);
               Physics.gravity = down * gravity;
           }

           if (Input.GetKeyDown("2")) {
               down = new Vector3(0,-1,0);
               Physics.gravity = down * gravity;
           }*/
        moveForce = Vector3.zero;
        if (isplayergrounded)
        {
            //moveForce = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));


            /*if (Input.GetKey(sprintkey) && !Input.GetMouseButton(1))
            {
                if (Input.GetKey(forwardkey))
                {
                    moveForce += Vector3.forward * 1.2f;
                    if (Input.GetKey(supersprintkey))
                    {
                        moveForce = Vector3.forward * 5.2f;
                    }
                }
            }*/






            // moveForce = transform.TransformDirection(moveForce);
            //moveForce = moveForce * speed;


            //Debug.Log(player.velocity.sqrMagnitude);


            if (player.velocity.sqrMagnitude > maxspeed)
            {
                //friction
                //moveForce = -player.transform.forward * 0.1f;


            }
            else
            {
                //movement
                if ((Input.GetKey(forwardkey) || Input.GetKey(forwardkey2)) && !fighting)
                {
                    moveForce = player.transform.forward * speed * 10;
                }

                
            }
            if (Input.GetKeyDown(sprintkey) && !fighting)
            {
                maxspeed = maxsprintspeed;
                anim.SetBool("Sprinting", true);
            }

            if (Input.GetKeyUp(sprintkey) && !fighting)
            {
                maxspeed = maxwalkspeed;
                anim.SetBool("Sprinting", false);
            }

            if (player.velocity.sqrMagnitude < 0.01f)
            {
                //moveForce = Vector3.zero;
                anim.SetBool("Walking", false);
                anim.SetBool("Sprinting", false);
                maxspeed = maxwalkspeed;

            }
            else
            {
                if (anim.GetBool("Walking") == false)
                    anim.SetBool("Walking", true);
            }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("Fighting Stance");
                anim.SetBool("Fighting", true);
   
                if (Weapon == 2) {

                    StartCoroutine(holdSword());
                }

                fighting = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                anim.ResetTrigger("Fighting Stance");
                anim.SetBool("Fighting", false);

                if (Weapon == 2)
                {
                    StartCoroutine(sheathSword());
                }

                fighting = false;
            }

            if (fighting)
            {
                if (Input.GetMouseButtonDown(0)) {

                    float delta = -(Screen.width * 0.5f - Input.mousePosition.x) / (Screen.width * 1.0f);
                    if (delta > 0.15f)
                    {
                        hitDirection = 1;
                    }
                    else
                    if (delta < -0.15f) {
                        hitDirection = -1;
                    }
                    else
                    {
                        hitDirection = 0;

                    }
                    anim.SetInteger("Hit Direction", hitDirection);
                    //Debug.Log("Hit in direction"+ hitDirection);

                    if (Weapon == 1)
                    {
                        anim.SetTrigger("Hit");
                        StartCoroutine(hitCooldown());
                    }
                    else if (Weapon == 2) 
                    {
                        float delta2 = -(Screen.height * 0.5f - Input.mousePosition.y) / (Screen.height * 1.0f);
                        if (delta2 > 0.15f)
                        {
                            hitDirection2 = 1;
                        }
                        else
                        if (delta2 < -0.15f)
                        {
                            hitDirection2 = -1;
                        }
                        else
                        {
                            hitDirection2 = 0;

                        }

                        anim.SetInteger("Hit Direction 2", hitDirection2);
                        //Debug.Log("Hit in direction" + hitDirection);
                        //anim.SetTrigger("Hold Hit");
                        //holdHit = true;

                        anim.SetTrigger("Hit");
                        StartCoroutine(hitCooldown());
                    }

                    rightorleft = !rightorleft;
                    anim.SetBool("Right Hand", rightorleft);
                }

               /* if (Input.GetMouseButtonUp(0)) {
                    if (holdHit) {




                        anim.ResetTrigger("Hold Hit");
                        anim.SetTrigger("Hit");
                        StartCoroutine(hitCooldown());
                        holdHit = false;


                    }
                
                }*/

                if (Input.GetKeyDown(drawswordkey) || Input.GetKeyDown(drawswordkey2)) {
                    if (Weapon == 1) {
                        Weapon = 2;
                        anim.SetInteger("Weapon", 2);
                        anim.SetTrigger("SwitchWeapon");
                        StartCoroutine(swithWeaponReset());
                        StartCoroutine(holdSword(0.45f));
                    }
                    else
                    {
                        Weapon = 1;
                        anim.SetInteger("Weapon", 1);
                        anim.SetTrigger("SwitchWeapon");
                        StartCoroutine(swithWeaponReset());
                        StartCoroutine(sheathSword());
                    }
                }
            }

            if (Input.GetKeyUp(jumpkey) && !fighting)
            {
                float strength = Time.time - jumpSetTime;
                strength = Mathf.Min(strength, 1);
                moveForce = moveForce - down * jumpSpeed * strength * 500;
                if (strength > 0.5f)
                {
                    anim.SetBool("High Speed Impact", true);
                }
                else
                {
                    anim.SetBool("High Speed Impact", false);
                }
                anim.SetTrigger("Jump");
                StartCoroutine(jumpReset());
            }

            //gravit
        }
        else
        {
            moveForce = Physics.gravity;
            if (player.velocity.y >= 0)
            {
                if (anim.GetBool("Falling") == true)
                    anim.SetBool("Falling", false);

                if (Input.GetKeyDown(jumpkey))
                {
                    anim.SetTrigger("FrontFlip");
                    StartCoroutine(frontFlipReset());

                }
            }
            else
            {
                if (anim.GetBool("Falling") == false)
                    anim.SetBool("Falling", true);
            }

        }



        player.AddForce(moveForce * 50 * Time.deltaTime);
    }

    private void Start()
    {
        anim.SetInteger("Weapon", Weapon);
        Physics.gravity = down * gravity;
        player = GetComponent<Rigidbody>();

    }

    
 }
