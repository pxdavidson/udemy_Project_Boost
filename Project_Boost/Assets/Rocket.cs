using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Misc Variables
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 10000f;
    float levelLoad = 2f;

    // Audio Variable
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip levelUp;

    // Particle Variables
    [SerializeField] ParticleSystem thrustVFX;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem levelUpVFX;

    // Game States
    bool alive = true;
    bool debug = false;

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
        if (alive == true)
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
        if (alive != true)
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
        float thrustSpeed = (mainThrust * Time.deltaTime);
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
        rigidBody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
    }

    // Crash rocket
    private void CrashRocket()
    {
        if (debug == true)
        {
            return;
        }
        alive = !alive;
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
        rigidBody.angularVelocity = Vector3.zero;
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
