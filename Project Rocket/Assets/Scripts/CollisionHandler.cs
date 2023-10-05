using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float respawnDelay = 0f;
    [SerializeField] AudioClip successNoise;
    [SerializeField] AudioClip crashNoise;

    AudioSource audioS;

    bool isTransitioning = false;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

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
        //Crash Audio, Stop Player Movement, Thrust Audio and Reload
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(crashNoise);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", respawnDelay);
    }

    void StartNextLevelSequence()
    {
        //Success Audio, Stop Player Movement, Thrust Audio, and Next Level
        isTransitioning = true;
        audioS.Stop();
        audioS.PlayOneShot(successNoise);
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
