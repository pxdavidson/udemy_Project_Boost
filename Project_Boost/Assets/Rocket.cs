using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        ProcessInput();
	}

    // Process users input
    private void ProcessInput()
    {
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
        print("Space pressed");
    }

    // Rotate counter clockwise
    private void RotatePort()
    {
        print("A pressed");
    }

    // Rotate clockwise
    private void RotateStarboard()
    {
        print("D pressed");
    }
}
