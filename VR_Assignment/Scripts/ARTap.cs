using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems; 
using System;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class ARTap : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public GameObject objectToPlace2;
    public ARRaycastManager raycastManager;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI objectPlacedText;
	// public GameObject winTextObject;

    private AudioSource audioSource;
    // private AudioSource audioSource2;

	public AudioClip tapSound;
	// public AudioClip gameBeginClip;

    public bool useCursor = true;
    private int tapCount;
    private string objectType;

    void Start()
    {
        cursorChildObject.SetActive(useCursor);

        // Set the tapCount to zero 
		tapCount = 0;
        objectType = null;

        //Getting the sound sources
		audioSource = GetComponent<AudioSource>();
        // audioSource2 = GetComponent<AudioSource>();


		//Load an AudioClip (Assets/Resources/Audio/audioClip01.mp3)
        // scoreClip = Resources.Load<AudioClip>("Audio/SuctionSound");
		tapSound = Resources.Load("Audio/SuctionSound") as AudioClip;

		//Load an AudioClip (Assets/Resources/Audio/audioClip01.mp3)
        // gameEndClip = Resources.Load<AudioClip>("Audio/gameBegin");
		// gameBeginClip = Resources.Load("Audio/gameBegin") as AudioClip;

		SetText(objectType);

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        // winTextObject.SetActive(false);
    }

    void Update()
    {
        
        if (useCursor)
        {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                if(tapCount == 0 || tapCount%2 == 0)
                {
                    GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
                    objectType = "Pot" ;
                }
                else
                {
                    GameObject.Instantiate(objectToPlace2, transform.position , transform.rotation);
                    objectType = "Tolima" ;
                }
                
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    // GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    if(tapCount == 0 || tapCount%2 == 0)
                    {
                        GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                        objectType = "Pot" ;
                    }
                    else
                    {
                        GameObject.Instantiate(objectToPlace2, hits[0].pose.position, hits[0].pose.rotation);
                        objectType = "Tolima" ;

                    }
                }
            }
            // Add one to the score variable 'tapCount'
			tapCount = tapCount + 1;

            
            // play audioSource sound
            // audioSource.Play();
            audioSource.PlayOneShot(tapSound, 0.7F);
            // Console.WriteLine("tapSound");

        }

        SetText(objectType);
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    void SetText(string objectType)
    {
        countText.text = "Tap Count : " + tapCount.ToString();
        objectPlacedText.text = "Object : " + objectType ;

        // audioSource2.PlayOneShot(gameBeginClip, 0.7F);
        // Console.WriteLine("gameBeginClip");
       
    }
}
