using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour {

    // Misc Variables
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
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = (Time.time / period); // Defines framerate unbound cycle of the wave
        float rawSinWave = Mathf.Sin(cycles * tau); // Defines Sine wave based on 1 cycle * tau = complete rotation.
        objMovFactor = rawSinWave / 2f + 0.5f; // sets the obj move factor as the upper half of the sine wave
        Vector3 offset = objMovVector * objMovFactor; // Defines offset variable
        transform.position = startPos + offset; // Actually moves the object
	}
}
