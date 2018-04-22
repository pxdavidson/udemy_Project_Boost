using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Variables
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 15f;
    
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

    // Detect collision
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("Friendly"):
                print("Safe");
                break;
            default:
                print("Dead");
                break;
        }
    }

    // Thrust the rocket
    private void Thrust()
    {
        float thrustSpeed = (mainThrust);
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
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
        float rotationSpeed = (rcsThrust * Time.deltaTime);
        rigidBody.freezeRotation = true;
        if (Input.GetKey("a"))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }
}
