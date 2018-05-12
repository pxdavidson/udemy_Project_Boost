using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Misc Variables
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 10000f;
    float levelLoad = 2f;
    bool debug;

    // Audio Variable
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip levelUp;

    // Particle Variables
    [SerializeField] ParticleSystem thrustVFX;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem levelUpVFX;

    // Game States
    enum State { Alive, Dead, Transcend,};
    State state = State.Alive;
    
    // Components
    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        print("llama"); // todo remove this
    }
	
	// Update is called once per frame
	void Update()
    {
        if (state == State.Alive)
        {
            ThrustRocket();
            RotateRocket();
        }
        if (Debug.isDebugBuild)
        {
            DebugMode();
        }
    }

    // Puts game into debug mode
    private void DebugMode()
    {
        if (Input.GetKeyDown(KeyCode.N) && debug == true)
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            debug = !debug;
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
                // Makes the start pad safe
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
        float thrustSpeed = (mainThrust); // todo Do I need this? Could I just use mainThrust direct?
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
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
        rigidBody.freezeRotation = false; // todo What does this actually do?
    }

    // Crash rocket
    private void CrashRocket()
    {
        if (debug == true)
        {
            return;
        }
        state = State.Dead;
        Invoke("LoadStartLevel", levelLoad);
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        thrustVFX.Play();
        crashVFX.Play();

    }
    
    // Load Level 1
    private void LoadStartLevel()
    {
        SceneManager.LoadScene(0);
    }

    // Complete level
    void LevelUp()
    {
        state = State.Transcend;
        Invoke("LoadNextLevel", levelLoad);
        audioSource.Stop();
        audioSource.PlayOneShot(levelUp);
        levelUpVFX.Play();
    }

    // Load next level
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = (currentScene + 1);
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            LoadStartLevel();
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
