using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosiiton;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosiiton = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; // Growing Over Time

        const float tau = Mathf.PI * 2; // Const Value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //Recalculated from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosiiton + offset;
    }
}
