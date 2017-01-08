using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunnerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.right;
    private bool isRunning = false;

    public float speed = 1;
    public float jumpSpeed = 2;
    public float gravity = 8;

    // Use this for initialization
    void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        startRunning();
	}

    void Update()
    {
        //Button input
        if (Input.GetButton("Jump"))
        {
            jump();
        }
        if (Input.GetButtonDown("Pause"))
        {
            setIsRunning(!isRunning);
        }

        if (isRunning)
        {
            //Applying gravity to the controller
            moveDirection.y -= gravity * Time.deltaTime;
            //Making the character move
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
    }

    public void startRunning()
    {
        moveDirection.x = speed;
    }

    public void setIsRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            setIsRunning(false);
            print("Game over");
        }
    }
}
