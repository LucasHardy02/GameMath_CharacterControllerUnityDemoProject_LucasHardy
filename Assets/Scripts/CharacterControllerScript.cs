using System;
using Unity.Hierarchy;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed;
    public float basespeed = 0f;
    public float maxSpeed = 6f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float hop = 3;
    public float gravity = -9.81f;

    private CharacterController controller;
    private bool isGrounded = false;
    private Vector3 velocity;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = 0f;
        maxSpeed = 6f;
    }

    void Update()
    {
        

        Vector3 direction = Vector3.zero;


        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; 
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;

        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;

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
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);

        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);

        }


        Vector3 movement = direction * speed * Time.deltaTime;

        controller.Move(movement);
        controller.Move(velocity * Time.deltaTime);

       


    }
  
}
