﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private sheepSpawner shSpawner;  // Reference to the sheepSpawner script

    public float heartOffset;
    public GameObject heartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        shSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        tweenScale twScale = gameObject.AddComponent<tweenScale>();
        twScale.targetScale = 0; 
        twScale.timeToReachTarget = gotHayDestroyDelay; 

        SoundManager.Instance.PlaySheepHitClip();

        if(gameObject.CompareTag("YellowSheep")) 
        {
            GameStateManager.Instance.SavedYellowSheep();
        }
        else
        {
            GameStateManager.Instance.SavedSheep();
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if(other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {
        shSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        SoundManager.Instance.PlaySheepDroppedClip();

        GameStateManager.Instance.DroppedSheep();
    }

    public void SetSpawner(sheepSpawner spawner)
    {
        shSpawner = spawner;
    }
}
