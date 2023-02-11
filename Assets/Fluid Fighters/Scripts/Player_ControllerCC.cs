using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ControllerCC : MonoBehaviour
{
    public bool isplayergrounded
    {
        get
        {
            //anim.SetBool("Falling", false);

            return _isplayergrounded;
        }
        set
        {
            //anim.SetBool("Grounded", value);
            _isplayergrounded = value;
        }

    }

    public void groundImpact()
    {
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


    //Rigidbody player;
    public CharacterController playerCC;

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
    bool walking = false;
    bool sprinting = false;

    //bool fightingstance = false;
    // string backgroundanim = "breathing";

    //bool groundedfirstframe = false;

    private Vector3 moveForce = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private CharacterController controller;

    //float mousex;
    //float pmousex;
    bool rightorleft = true;
    float jumpSetTime;
    int hitDirection = 0;
    int hitDirection2 = 0;

    IEnumerator jumpReset()
    {
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
        if (fighting || (Weapon == 2 && fighting))
        {
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

    /*bool mouseButton0Pressed = false;
    bool jumpButtonPressed = false;
    bool drawSwordPressed = false;*/
    void Update()
    {
        //Debug.Log(playerCC.isGrounded);
        //moveForce = Vector3.zero;
        //Debug.Log(isplayergrounded);
        //RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.down * 0.5f, Vector3.down * 0.1f, Color.red);
        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, 0.08f))
        {
            
            isplayergrounded = true;
        }
        else
        {
            isplayergrounded = false;
        }

        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, 0.05f))
        {

            if (playerVelocity.y < 0)
                playerVelocity = Physics.gravity * Time.deltaTime;
        }


        if (isplayergrounded)
        {
            moveForce *= 0.8f;
            //moveForce = 
            

            if (anim.GetBool("Grounded") == false)
                anim.SetBool("Grounded", true);

            if (anim.GetBool("Falling") == true)
                anim.SetBool("Falling", false);


            //movement
            //print(Input.GetKeyDown(jumpkey));
            //Debug.Log(fighting);
            if (fighting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //if (!mouseButton0Pressed)
                    {
                       // mouseButton0Pressed = true;
                        float delta = -(Screen.width * 0.5f - Input.mousePosition.x) / (Screen.width * 1.0f);
                        if (delta > 0.15f)
                        {
                            hitDirection = 1;
                        }
                        else
                        if (delta < -0.15f)
                        {
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
                }
               /* else { 
                    if(mouseButton0Pressed)
                    mouseButton0Pressed = false;
                }*/


                if (Input.GetKeyDown(drawswordkey) || Input.GetKeyDown(drawswordkey2))
                {
                   // if (!drawSwordPressed)
                    {
                       // drawSwordPressed = true;
                        if (Weapon == 1)
                        {
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
                /*else { 
                    if(drawSwordPressed)
                        drawSwordPressed= false;
                }*/
            }
            else
            {
                if (Input.GetKeyUp(jumpkey))
                {
                    //if (jumpButtonPressed)
                    {
                       // jumpButtonPressed = false;
                        //Debug.Log("Jump UP");
                        float strength = Time.realtimeSinceStartup - jumpSetTime;
                        strength = Mathf.Min(strength, 1);
                        playerVelocity += Vector3.up * jumpSpeed * 0.5f * (1+strength);
                        if (strength > 0.2f)
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
                }
                /*else {
                    if (!jumpButtonPressed)
                    {
                        jumpButtonPressed = true;
                        Debug.Log("Jump Down");
                        jumpSetTime = Time.realtimeSinceStartup;
                    }
                }*/

                if ((Input.GetKey(forwardkey) || Input.GetKey(forwardkey2)))
                {
                    moveForce = playerCC.transform.forward * maxspeed * 0.2f;
                    if (!walking)
                    {
                        walking = true;
                        anim.SetBool("Walking", true);
                    }
                }
                else
                {
                    if (walking)
                    {
                        walking = false;
                        anim.SetBool("Walking", false);
                    }
                }

                if (Input.GetKey(sprintkey))
                {
                    if (!sprinting)
                    {
                        sprinting = true;
                        maxspeed = maxsprintspeed;
                        anim.SetBool("Sprinting", true);
                    }
                }
                else
                {
                    if (sprinting)
                    {
                        sprinting = false;
                        maxspeed = maxwalkspeed;
                        anim.SetBool("Sprinting", false);
                    }
                }

            }

            if (Input.GetKeyDown(jumpkey))
            {

                //Debug.Log("Jump Down");
                jumpSetTime = Time.realtimeSinceStartup;
            }



            if (Input.GetMouseButton(1))
            {
                //Debug.Log("Mouse 1 down");
                if (!fighting)
                {
                    anim.SetTrigger("Fighting Stance");
                    anim.SetBool("Fighting", true);

                    if (Weapon == 2)
                    {

                        StartCoroutine(holdSword());
                    }

                    fighting = true;
                }
            }
            else
            {
                if (fighting)
                {
                    anim.ResetTrigger("Fighting Stance");
                    anim.SetBool("Fighting", false);

                    if (Weapon == 2)
                    {
                        StartCoroutine(sheathSword());
                    }

                    fighting = false;
                }
            }

        }
        else
        {
            //moveForce *= 0.99f;

            if (anim.GetBool("Grounded") == true)
                anim.SetBool("Grounded", false);

            if (walking)
            {
                walking = false;
                anim.SetBool("Walking", false);

            }

            if (sprinting)
            {
                sprinting = false;
                anim.SetBool("Sprinting", false);

            }

            if (playerCC.velocity.y >= 0)
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

        //moveForce *= 0.99f;
        
        playerVelocity += Physics.gravity * Time.deltaTime;
        
        playerCC.Move((playerVelocity+moveForce) * Time.deltaTime);
        //playerCC.Move(moveForce * Time.deltaTime);
    }

    private void Start()
    {
        anim.SetInteger("Weapon", Weapon);
        Physics.gravity = down * gravity;
        playerCC = GetComponent<CharacterController>();
        

    }


}