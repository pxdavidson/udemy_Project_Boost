using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour {
        // todo: Add this scipt to other objects

    // Misc Variables
        // todo: why can't I see all the serialized fields?
    [SerializeField] Vector3 objMovVector;
    [SerializeField] float period = (2f);
    [SerializeField] [Range(0, 1)] float objMovFactor;
    Vector3 startPos;

    // Sine Variables
    const float tau = (Mathf.PI * 2); // Defines "Tau" as a constant

	// Use this for initialization
	void Start ()
    {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float cycles = (Time.time / period); // Defines framerate unbound period of time (s)
        float rawSinWave = Mathf.Sin(cycles * tau); // Defines Sine wave based on 1 cycle * tau = complete rotation.

        objMovFactor = rawSinWave / 2f + 0.5f; // todo: investigate why this is "/2f + 0.5f" instead of "/1". What does this gain?
        Vector3 offset = objMovVector * objMovFactor; // Defines offset variable
        transform.position = startPos + offset; // Actually moves the object
	}
}
