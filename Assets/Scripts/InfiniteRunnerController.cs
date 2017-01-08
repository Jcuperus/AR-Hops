using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfiniteRunnerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.right;
    private bool isRunning = false;
    private Vector3 startPosition;
    private GameObject gameOverPanel;

    public float speed = 1;
    public float jumpSpeed = 2;
    public float gravity = 8;

    // Use this for initialization
    void Start () {
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
        controller = gameObject.GetComponent<CharacterController>();
        startPosition = transform.position;
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

    public void restart()
    {
        gameOverPanel.SetActive(false);
        transform.position = startPosition;
        setIsRunning(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            setIsRunning(false);
            gameOverPanel.SetActive(true);
            print("Game over");
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
