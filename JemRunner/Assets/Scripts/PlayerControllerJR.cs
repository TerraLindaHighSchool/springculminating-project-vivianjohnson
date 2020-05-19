﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJR : MonoBehaviour
{
    private Rigidbody playerRb;
    public float horizontalInput;
    public float forwardInput; 
    public float jumpForce = 10.0f; 
    public float turnSpeed; 
    public float speed = 10.0f; 
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical"); 

        if(Input.GetKeyDown(KeyCode.Space)&& isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; 
        }
        if(!gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);  
        }
    }
}
