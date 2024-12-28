using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDrunkCamera : MonoBehaviour
{
    public float rotationJitter = 0.5f;
    public float positionJitter = 0.1f;
    public float jitterSpeed = 5f;

    public float swayAmount = 2f; // Strength of the sway
    public float swayFrequency = 0.5f; // How often it sways


    private Vector3 initialPosition;
    private Quaternion initialRotation;


    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }

    void Update()
    {
        // Swaying motion (using sine waves)
        float swayX = Mathf.Sin(Time.time * swayFrequency) * swayAmount;
        float swayY = Mathf.Cos(Time.time * swayFrequency * 0.75f) * swayAmount * 0.5f; // Slightly different frequency and amplitude

        // Rotation Jitter
        float xRot = Mathf.PerlinNoise(Time.time * jitterSpeed, 0) * rotationJitter;
        float yRot = Mathf.PerlinNoise(0, Time.time * jitterSpeed) * rotationJitter;



        transform.rotation = initialRotation * Quaternion.Euler(xRot + swayX, yRot + swayY, 0f);


        float xPos = Mathf.PerlinNoise(Time.time * jitterSpeed * 2, 0) * positionJitter;
        float yPos = Mathf.PerlinNoise(0, Time.time * jitterSpeed * 2) * positionJitter;



        transform.position = initialPosition + new Vector3(xPos, yPos, 0f) + new Vector3(swayX * 0.2f, swayY * 0.2f, 0); // Added sway influence

    }
}
