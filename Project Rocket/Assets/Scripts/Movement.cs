using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float boostStr = 0;
    [SerializeField] float rotationStr = 0;
    [SerializeField] AudioClip thrusterNoise;
    Rigidbody rb;
    AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    //Get the Inputs from the User
    void GetUserInput()
    {
        //If Space or W
        GetUserBoost();

        //If A and D are pressed
        GetUserRotate();
    }

    //Get the Space and W Input
    void GetUserBoost()
    {
        //If Space Bar or W is pressed
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //Relative Force for Rockect
            rb.AddRelativeForce(Vector3.up * boostStr * Time.deltaTime);

            //If SFX is not playing
            if (!audioS.isPlaying)
            {
                //Play the SFX
                audioS.PlayOneShot(thrusterNoise);
            }
        }
        else
        {
            //Stop the SFX
            audioS.Stop();
        }
    }

    //Get the Rotate Input
    void GetUserRotate()
    {
        //If A is pressed
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //Rotate Rocketship Left
            ApplyRotation(rotationStr);
        }
        //If D is pressed
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //Rotate Rocketship Rigth
            ApplyRotation(-rotationStr);
        }
    }

    ////Rotate Rocketship
    void ApplyRotation(float rotationThisFrame)
    {
        //Freeze Rotation
        rb.freezeRotation = true;

        //Rotate Rocketship based on + or - 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        //Unfreeze Rotation
        rb.freezeRotation = false;
    }
}
