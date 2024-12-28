using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDrunkCamera : MonoBehaviour
{
    // Outer scripts
    public ScriptGameManager gameManager;

    // Public vars
    public float waveSpeed = 1.5f;
    public float waveAmplitude;
    public float shakeIntensity;
    public float shakeFrequency = 8f;

    private Vector3 originalPosition;
    private float waveOffset;

    private void Awake()
    {
        gameManager = GetComponent<ScriptGameManager>();
    }

    private void Start()
    {
        originalPosition = transform.position;  
        waveOffset = Random.value * Mathf.PI * 2; // Randomize wave start
    }

    private void Update()
    {
        // Wave effect 
        float waveX = Mathf.Sin(Time.time * waveSpeed + waveOffset) * waveAmplitude;
        float waveY = Mathf.Cos(Time.time * waveSpeed + waveOffset) * waveAmplitude; // Cos for Y
        Vector3 waveOffsetVector = new Vector3(waveX, waveY, 0);

        // Shake effect 
        float shake = Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) * 2 - 1;
        Vector3 shakeOffsetVector = new Vector3(shake, shake, 0f) * shakeIntensity;

        // Combine and apply offsets 
        transform.position = originalPosition + waveOffsetVector + shakeOffsetVector;
    }
}