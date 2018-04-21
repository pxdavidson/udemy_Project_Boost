using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Components
    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
    {
        ProcessInput();
	}

    // Process users input
    private void ProcessInput()
    {
        ThrustAudio();
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }
        if (Input.GetKey("a"))
        {
            RotatePort();
        }
        else if (Input.GetKey("d"))
        {
            RotateStarboard();
        }
    }

    // Thrust the rocket
    private void Thrust()
    {
        rigidBody.AddRelativeForce(Vector3.up);
    }

    // Rotate counter clockwise
    private void RotatePort()
    {
        transform.Rotate(Vector3.forward);
    }

    // Rotate clockwise
    private void RotateStarboard()
    {
        transform.Rotate(-Vector3.forward);
    }

    // Play Thrust SFX
    private void ThrustAudio()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}
