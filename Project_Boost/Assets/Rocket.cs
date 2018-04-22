using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Variables
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 100f;
    enum State { Alive, Dead, Transcend };
    State state = State.Alive;
    
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
        ThrustAudio();
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    // Detect collision
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case ("Respawn"):
                //
                break;
            case ("Finish"):
                state = State.Transcend;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dead;
                Invoke("LoadStartLevel", 1f);
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
        if (Input.GetKeyUp(KeyCode.Space) || state != State.Alive)
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

    // Load Level 1
    private void LoadStartLevel()
    {
        SceneManager.LoadScene(0);
    }

    // Load Level 2
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
