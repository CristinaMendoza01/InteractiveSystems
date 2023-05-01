﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hayMachine : MonoBehaviour
{
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1

        if (horizontalInput < 0) // 2
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0) // 3
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

}
