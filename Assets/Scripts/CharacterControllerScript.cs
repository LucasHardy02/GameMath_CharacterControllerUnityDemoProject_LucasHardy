using System;
using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.LookDev;
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

    private float standingHeight;
    private Vector3 standingCenter;

    private float heightDifference;

    private float cameraStartY;
    private float cameraCrouchY;

    private float crouchHeight;
    private Vector3 crouchCenter;

    private float targetCamY;
    
    void Start()
    {

        controller = GetComponent<CharacterController>();

        standingHeight = controller.height;
        standingCenter = controller.center;

        crouchHeight = standingHeight * 0.6f;
        heightDifference = standingHeight - crouchHeight;
        crouchCenter = standingCenter - new Vector3(0, (heightDifference) / 2f, 0);

        cameraStartY = camera.localPosition.y;
        cameraCrouchY = cameraStartY - heightDifference / 2;

        speed = 0f;
        maxSpeed = 6f;

        



        

        


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
        localForward.y = 0;
        localForward.Normalize();

        Vector3 localLeft = -camera.right;
        localLeft.y = 0;
        localLeft.Normalize();

        Vector3 localRight = camera.right;
        localRight.y = 0;
        localRight.Normalize();

        Vector3 localBack = -camera.forward;
        localBack.y = 0;
        localBack.Normalize();

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
            targetCamY = cameraCrouchY;

            

        }
        else if (Sprinting)
        {
            speed = Mathf.MoveTowards(speed, maxSprintSpeed, acceleration * Time.deltaTime);
            targetCamY = cameraStartY;

        }
        else if (isMoving)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);
            targetCamY = cameraStartY;

        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);
            targetCamY = cameraStartY;
        }

        

        Vector3 cameraPos = camera.localPosition;
        cameraPos.y = Mathf.Lerp(cameraPos.y, targetCamY, Time.deltaTime * 10f);
        camera.localPosition = cameraPos;

        Vector3 movement = direction * speed * Time.deltaTime;


        controller.Move(movement);
        controller.Move(velocity * Time.deltaTime);

        Debug.Log(movement);
        

    }
}
