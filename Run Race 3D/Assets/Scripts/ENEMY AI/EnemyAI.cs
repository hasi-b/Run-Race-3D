using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Vector3 move;

    public float speed, jumpforce, gravity, verticalVelocity;
    public float raycast_Distance = 10f;
    private bool wallSlide, turn,jump;
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

       /* if (GameManager.instance.finish) //if the finish line is reached, the ai will not execute its controller and switch to dance animation
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                anim.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up*180;
            }
            return;
        }
        */

        move = Vector3.zero;
        move = transform.forward;



        if (turn)
        {
            turn = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);

        }

        if (!wallSlide)
        {
            gravity = 30;
            verticalVelocity -= gravity * Time.deltaTime; 
        }
        else
        {
            gravity = 15;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        anim.SetBool("Grounded", charController.isGrounded);
        anim.SetBool("Wallslide", wallSlide);



        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);
    }


    private void FixedUpdate()
    {

       /* if (GameManager.instance.finish)
        {
            return;
            // this is, if the race is finished, rest of the code will not execute as it doesnt need to execute
        } */

        if (charController.isGrounded)
        {
            jump = true;
            wallSlide = false;
            verticalVelocity = 0;
            Raycasting();
            Debug.Log("true,grounded");
        }
    }




    void Raycasting()
    {
        RaycastHit hit;

        Debug.Log("ENNTEEEEERRRRED");
        if (Physics.Raycast(transform.position, transform.forward, out hit,raycast_Distance))
        {
            Debug.DrawLine(transform.position , hit.point,Color.red);

            Debug.Log("hit  "+ hit.collider.tag);

            if (hit.collider.tag == "wall")
            {
                
                verticalVelocity = jumpforce;
                anim.SetTrigger("Jump");
                Debug.Log("It jumped");
            }


            Debug.Log("raycast");
        }
    }

    IEnumerator Latejump(float time)
    {
        jump = false;
        yield return new WaitForSeconds(time);

        if (!charController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            verticalVelocity = jumpforce;
            anim.SetTrigger("Jump");
            
        }
        jump = true;
        wallSlide = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "wall")
        {
            if (jump)
            {
                StartCoroutine(Latejump(Random.Range(0.2f, 0.5f)));
                if (verticalVelocity < 0)
                {
                    wallSlide = true;
                }
            }
        }
        if(hit.collider.tag == "slide" && charController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }

        else if(hit.collider.tag == "slide")
        {
            wallSlide = true;
        }




        if (transform.forward != hit.collider.transform.up && hit.collider.tag == "Ground" && !turn)
        {
            turn = true;

        }
    }
}
