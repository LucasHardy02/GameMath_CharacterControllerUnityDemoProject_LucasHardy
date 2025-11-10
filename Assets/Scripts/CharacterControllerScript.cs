using System;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{

    public int speed;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if(keyInfo.Key == ConsoleKey.W)
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (keyInfo.Key == ConsoleKey.A)
        {
            transform.Translate(Vector3.left * speed);
        }
        if (keyInfo.Key == ConsoleKey.D)
        {
            transform.Translate(Vector3.right * speed);
        }
        if (keyInfo.Key == ConsoleKey.S)
        {
            transform.Translate(Vector3.back * speed);
        }

    }
    void PlayerMovement()
    {
        
    }
}
