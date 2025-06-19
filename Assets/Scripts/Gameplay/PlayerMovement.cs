using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // CharacterController Ref
    public CharacterController controller;

    // Player Movement
    private float speed = 12.0f;
    private float gravity = -25.0f;
    private float jumpHeight = 5.0f;

    // Ground
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Velocity
    Vector3 velocity;

    void Update() // Update is called once per frame
    {
        // Checks the child groundcheck object on the player to see if the player is standing on the ground 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) // Bring player to a stop when landing on ground
        { 
            velocity.y = -5.0f; // High negitive value required to stick player to platform when platform fall speed picks up
        }

        // Gets horizontal and vertical movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // Moves player forward and backwards

        controller.Move(move * speed * Time.deltaTime); // Controlls move speed

        if (Input.GetButtonDown("Jump") && isGrounded) // Makes player jump and play SFXs
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        // Allows player to fall
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
