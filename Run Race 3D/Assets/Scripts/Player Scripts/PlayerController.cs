using System.Diagnostics;
using System.Net.WebSockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 move;

    public float speed,jumpforce,gravity,verticalVelocity;

    private bool wallSlide;
    private CharacterController charController; 

    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        move = Vector3.zero;
        move = transform.forward;

        if(charController.isGrounded){
            wallSlide = false;
            verticalVelocity = 0;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                verticalVelocity = jumpforce;

            }
        }
        if(!wallSlide)
        {
            gravity=30;
            verticalVelocity-= gravity*Time.deltaTime;
        }
        else{
            gravity = 15;
            verticalVelocity-= gravity*Time.deltaTime;


        }

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move*  Time.deltaTime);
        print(wallSlide);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
{
    if(!charController.isGrounded)
    {
        

        if(hit.collider.tag == "wall")
        {
            if (verticalVelocity<-0.7f)
        {
            wallSlide=true;
        }

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                verticalVelocity = jumpforce;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180, transform.eulerAngles.z);
                 wallSlide=false;
            
            }
        }
           


    }
}



}

