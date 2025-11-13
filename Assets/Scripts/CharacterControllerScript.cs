using System;
using Unity.Hierarchy;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float playerSpeed;
    public int hop;

    private Rigidbody rb;
    private bool isGrounded = false;
    public GameObject player;
    public GameObject Camera;
    void Start()
    {
        playerSpeed = 0.05f;
    }

    void FixedUpdate()
    {
        
        if(Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector3.forward * playerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(Vector3.left * playerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(Vector3.right * playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector3.back * playerSpeed);
        }
        if (isGrounded == true || Input.GetKey(KeyCode.Space))
        {
            player.transform.Translate(Vector3.up * hop);

        }

    }
    void PlayerMovement()
    {
        
    }
}
