using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Vector3 move;

    public float speed, jumpforce, gravity, verticalVelocity;

    private bool wallSlide, turn;
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
        move = Vector3.zero;
        move = transform.forward;
       

       


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
        if (charController.isGrounded)
        {
            verticalVelocity = 0;
            Raycasting();
            Debug.Log("true,grounded");
        }
    }




    void Raycasting()
    {
        RaycastHit hit;

        Debug.Log("ENNTEEEEERRRRED");
        if (Physics.Raycast(transform.position, transform.forward, out hit))
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
}
