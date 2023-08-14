using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        //If Rocketship Collides
        switch (other.gameObject.tag)
        {
            case "Safe":
                Debug.Log("Safe Zone");
                break;
            case "Finish":
                Debug.Log("Finished");
                break;
            case "Fuel":
                Debug.Log("Refueled");
                break;
            default:
                Debug.Log("Rocketship Exploded");
                break;
        }
    }
}
