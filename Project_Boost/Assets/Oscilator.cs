using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour {

    // Misc Variables
    [SerializeField] Vector3 objMovVector;
    [Range(0, 1)] [SerializeField] float objMovFactor;
    Vector3 startPos;

	// Use this for initialization
	void Start ()
    {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 offset = objMovVector * objMovFactor;
        transform.position = startPos + offset;
	}
}
