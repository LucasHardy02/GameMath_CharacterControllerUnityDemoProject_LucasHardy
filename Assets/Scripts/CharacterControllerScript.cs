using System;
using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed;
    public float basespeed = 1f;
    public float maxSpeed = 6f;

    public float maxSprintSpeed = 11f;

    public float maxCrouchSpeed = 3f;

    public float acceleration = 10f;
    public float deceleration = 10f;
    public float hop = 3;
    public float gravity = -9.81f;

    public Transform camera;

    private CharacterController controller;
    private bool isGrounded = false;
    private Vector3 velocity;
    private Vector3 direction;
    private Vector3 lastDirection;
    private bool Sprinting = false;
    private bool Crouching = false;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = 0f;
        maxSpeed = 6f;

        camera = camera.transform;


    }

    void Update()
    {

        // Character Rotation

        Vector3 mousePos = Input.mousePosition;

        float yPos = Mathf.Clamp(mousePos.y, -89, 89);

        camera.localRotation = Quaternion.Euler(-yPos, 0, 0);
        transform.rotation = Quaternion.Euler(0, mousePos.x, 0);

        // WASD Movement

        direction = Vector3.zero;

        Vector3 localForward = camera.forward;
        Vector3 localLeft = -camera.right;
        Vector3 localRight = camera.right;
        Vector3 localBack = -camera.forward;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += localForward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += localLeft;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += localRight;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += localBack;

        }
        

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = hop;

        }
        if (isGrounded == false)
        {
            velocity.y += gravity * Time.deltaTime;

        }
        


        direction = direction.normalized;

        bool isMoving = direction.magnitude > 0.1f;

        if (isMoving)
        {
            lastDirection = direction;

        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouching = true;

        }
        else
        {
            Crouching = false;

        }



        if (Input.GetKey(KeyCode.LeftShift) && isMoving)
        {
            Sprinting = true;

        }
        else
        {
            Sprinting = false;
        }






        if (Crouching)
        {
            speed = Mathf.MoveTowards(speed, maxCrouchSpeed, acceleration * Time.deltaTime);
        }
        else if (Sprinting)
        {
            speed = Mathf.MoveTowards(speed, maxSprintSpeed, acceleration * Time.deltaTime);
        }
        else if (isMoving)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);

        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);

        }


        Vector3 movement = lastDirection * speed * Time.deltaTime;


        controller.Move(movement);
        controller.Move(velocity * Time.deltaTime);

        Debug.Log(movement);

    }
}
