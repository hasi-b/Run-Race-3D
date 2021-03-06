//using System.Threading.Tasks.Dataflow;
using System.Web;
using System.Net.Http;
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

    private bool wallSlide,turn;
    private CharacterController charController; 
    private Animator anim;

   
    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.finish)
        {
            move = Vector3.zero;
            if (!charController.isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = 0;

            }


            move.y = verticalVelocity;

            charController.Move(new Vector3(0, move.y * Time.deltaTime, 0));


            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                anim.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }
            return;
        }


        if (!GameManager.instance.start)
        {
            return;
        }

    

        move = Vector3.zero;
        move = transform.forward;

        if(charController.isGrounded){
            wallSlide = false;
            verticalVelocity = 0;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                
                Jump();
            }
            if(turn){
                    turn = false;
                    
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180, transform.eulerAngles.z);

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
        anim.SetBool("Wallslide",wallSlide);
        anim.SetBool("Grounded", charController.isGrounded);

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move*  Time.deltaTime);
        print(wallSlide);
    }

    void Jump(){
        verticalVelocity = jumpforce;
        anim.SetTrigger("Jump");

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
                Jump();
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180, transform.eulerAngles.z);
                 wallSlide=false;
            
            }
        }
           


    }
    else
{
    if(transform.forward != hit.collider.transform.up && hit.collider.tag == "Ground" && !turn){
        turn = true;
        
    }
}

}


}

