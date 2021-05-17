using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 4.5f;
    private float jumpHeight = 2.5f;
    private float gravityValue = -9.81f;
    Vector3 move;

    private Vector2 startPos;
    public int pixelDistToDetect = 25;
    private bool fingerDown;

    public Animator animator;
    public RopeSpawn ropeSpawn;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (fingerDown == false && Input.GetMouseButton(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }

        if (fingerDown && !GameManager.Instance.endGame)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            move = new Vector3(0, 0, 1);
            controller.Move(move * Time.deltaTime * playerSpeed);
            GameManager.Instance.isRunning = true;
            gameObject.transform.forward = move;

            if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                //Go Left
               // fingerDown = false;
                move = new Vector3(-0.50f, 0, 0);
                controller.Move(move * Time.deltaTime * playerSpeed);
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                //Go right
             //   fingerDown = false;
                move = new Vector3(0.50f, 0, 0);
                controller.Move(move * Time.deltaTime * playerSpeed);
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.isRunning = false;
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }

        // Don't go outside of the map

        Vector3 pos = transform.position;

        if (transform.position.x < -2.37f)
        {
            pos.x = -2.37f;
        }
        else if (transform.position.x > 2.37f)
        {
            pos.x = 2.37f;
        }

        transform.position = pos;

        if (GameManager.Instance.endGame)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "endGame")
        {
            Destroy(other.gameObject);
            animator.SetBool("endGame", true);
            GameManager.Instance.endGame = true;
            ropeSpawn.Delete();
        }
    }
}
