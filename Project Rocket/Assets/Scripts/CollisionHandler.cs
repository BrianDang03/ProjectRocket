using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float respawnDelay = 0f;
    [SerializeField] AudioClip successNoise;
    [SerializeField] AudioClip crashNoise;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioS;
    bool collisionDisabled = false;

    bool isTransitioning = false;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetUserCheats();
    }

    void GetUserCheats()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        //If Rocketship Collides
        switch (other.gameObject.tag)
        {
            case "Safe":
                Debug.Log("Safe Zone");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        //Stop Thrust Audio, Start Crash Audio, Start Crash Particles, Stop Player Movement, and Start Reload
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(crashNoise);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", respawnDelay);
    }

    void StartNextLevelSequence()
    {
        //Stop Thrust Audio, Start Successs Audio, Start Success Particles, Stop Player Movement, and Start Next Level
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(successNoise);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", respawnDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Restart Scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //Go to Level 1
            nextSceneIndex = 0;
        }
        //Next Level
        SceneManager.LoadScene(nextSceneIndex);
    }
}
