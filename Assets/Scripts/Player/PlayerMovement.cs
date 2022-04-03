using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Set an instance of PlayerMovement
    public static PlayerMovement instance;

    //Get rigidbody and serialize it
    public Rigidbody rb;

    [Title("Movement Variables")]
    //Set normal speed of player
    [SerializeField]
    private float walkSpeed = 5f;

    //Set the run Speed of the player
    [SerializeField] private float runSpeed = 200f;

    [Title("Position Variables")] [SerializeField]
    //The Direction the player will move
    private Vector3 movePos;
    //Are we running?
    public bool running;

    [Title("Rotation Variables")] [SerializeField]
    float rotationSpeed = 5f;
    float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    [SerializeField] private Transform cam;
    
    [Title("Animator Variables")]
    //Get animator component
    public Animator anim;

    
    
    [Title("Particles")]
    public ParticleSystem dirtParticles;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input.getaxisraw for Horizontal and Vertical
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Set the movePos to the input and normalize it so its between -1 and 1
        movePos = new Vector3(horizontal, 0, vertical).normalized;

        if (movePos != Vector3.zero && Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("sprinting",true);
            running = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || movePos.Equals(Vector3.zero))   
        {
            if (running.Equals(true))
                running = false;
            anim.SetBool("sprinting", false);
        }

        if (!running)
        {
            if(anim.GetBool("sprinting")) 
                anim.SetBool("sprinting",false);
        }

        //If we are moving, rotate the player based in the direction we are moving
        if (movePos != Vector3.zero)
        {
            anim.SetBool("walking",true);
        }
        else
        {
            if(anim.GetBool("walking"))
                anim.SetBool("walking",false);
        }
    }

    private void FixedUpdate()
    {
        //Equal the velocity to the direction the player is going * the speed * Time.deltaTime

        if (movePos != Vector3.zero)
        {
            var targetAngle = Mathf.Atan2(movePos.x, movePos.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            var velocity = moveDir * (running ? runSpeed : walkSpeed) * Time.deltaTime;
        
            rb.velocity = velocity;
        }

       

        if (running)
        {
            dirtParticles.Play();
        }
    }
}