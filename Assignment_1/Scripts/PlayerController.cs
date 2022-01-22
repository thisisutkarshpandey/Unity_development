using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count;

	private AudioSource audioSource;
	public AudioClip scoreClip;
	public AudioClip gameEndClip;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
		
		//Getting the sound sources
		audioSource = GetComponent<AudioSource>();

		//Load an AudioClip (Assets/Resources/Audio/audioClip01.mp3)
        // scoreClip = Resources.Load<AudioClip>("Audio/SuctionSound");
		scoreClip = Resources.Load("Audio/SuctionSound") as AudioClip;

		//Load an AudioClip (Assets/Resources/Audio/audioClip01.mp3)
        // gameEndClip = Resources.Load<AudioClip>("Audio/gameBegin");
		gameEndClip = Resources.Load("Audio/gameBegin") as AudioClip;

		SetCountText ();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
	}

	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// play audioSource sound
            // audioSource.Play();
			audioSource.PlayOneShot(scoreClip, 0.7F);

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
			
		}
	}

        void OnMove(InputValue value)
        {
        	Vector2 v = value.Get<Vector2>();

        	movementX = v.x;
        	movementY = v.y;
        }

		void SetCountText()
		{
			countText.text = "Count: " + count.ToString();

			if (count >= 12) 
			{
				// Set the text value of your 'winText'
				winTextObject.SetActive(true);
				audioSource.PlayOneShot(gameEndClip, 0.7F);
			}
		}
}
