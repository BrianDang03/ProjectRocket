using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    //Get the Inputs from the User
    void GetUserInput()
    {
        //If Space Bar or W is pressed
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //Boost Rocketship up
            Debug.Log("Pressed Space - Boosting");
        }

        //If A and D are pressed
        GetUserRotate();
    }

    //Get the Rotate Input
    void GetUserRotate()
    {
        //If A is pressed
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //Rotate Rocketship Left
            Debug.Log("Pressed A - Rotating Left");
        }
        //If D is pressed
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //Rotate Rocketship Rigth
            Debug.Log("Pressed D - Rotating Right");
        }
    }

}
