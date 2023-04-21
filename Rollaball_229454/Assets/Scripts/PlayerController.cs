using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject introTextObject;

    private Rigidbody rb;
    private int count;
    private int numPicksUp; // To count the number of yellow PickUp
    private float movementX;
    private float movementY;

    private AudioSource pickUpAudio;
    public AudioClip pickUpSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickUpAudio = GetComponent<AudioSource>(); 

        // Initiate counters to 0
        count = 0;
        numPicksUp = 0;

        SetCountText(); // To active the Count text
        winTextObject.SetActive(false); 
        loseTextObject.SetActive(false);
        introTextObject.SetActive(true); // Activate Intro text during all game
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // If there are no yellow PickUp's left and the count has a value >= 9
        if(numPicksUp == 20 & count >= 10)
        {
            // Player wins
            winTextObject.SetActive(true);
            pickUpAudio.PlayOneShot(winSound, 1.0f);
        }
        
        // If there are no yellow PickUp's left and the count has a value < 9
        if(numPicksUp == 20 & count < 10)
        {
            // Player loses
            loseTextObject.SetActive(true);
            pickUpAudio.PlayOneShot(loseSound, 1.0f);
        }
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        pickUpAudio.PlayOneShot(pickUpSound, 1.0f);
        // If it's a PickUp object
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); // To make it disappear
            count += 1; // Increase the counter of points
            numPicksUp += 1; // Increase the counter of yellow PickUp's
            SetCountText(); // Update the counter in the UI
        }
        // If it's a NoPickUp object
        if(other.gameObject.CompareTag("NoPickUp"))
        {
            other.gameObject.SetActive(false); // To make it disappear
            count -= 1; // Decrease the counter of points
            SetCountText(); // Update the counter in the UI
        }
    }
}
