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
        Thrust();
        ThrustAudio();
        Rotate();
    }

    // Thrust the rocket
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }
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

    // Rotate rocket
    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey("a"))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(-Vector3.forward);
        }
        rigidBody.freezeRotation = false;
    }
}
