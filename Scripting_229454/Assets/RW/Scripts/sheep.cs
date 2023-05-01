﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
    }
}
