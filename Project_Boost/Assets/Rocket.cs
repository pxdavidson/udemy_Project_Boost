using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Variables
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 100f;

    // Audio Variables
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip levelUp;

    // Particle Variables
        // todo: USE THESE!!
    [SerializeField] ParticleSystem thrustVFX;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem levelUpVFX;

    // Game States
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
        if (state == State.Alive)
        {
            ThrustRocket();
            RotateRocket();
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
                LevelUp();
                break;
            default:
                CrashRocket();
                break;
        }
    }

    // Thrust the rocket
    private void ThrustRocket()
    {
        float thrustSpeed = (mainThrust);
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(thrustSFX);
            thrustVFX.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            thrustVFX.Stop();
        }
    }

    // Rotate rocket
    private void RotateRocket()
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

    // Crash rocket
    private void CrashRocket()
    {
        state = State.Dead;
        Invoke("LoadStartLevel", 2f);
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashVFX.Play();
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

    // Complete level
    private void LevelUp()
    {
        state = State.Transcend;
        Invoke("LoadNextLevel", 2f);
        audioSource.Stop();
        audioSource.PlayOneShot(levelUp);
        levelUpVFX.Play();
    }
}
