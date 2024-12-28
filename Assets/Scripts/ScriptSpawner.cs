using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawner : MonoBehaviour
{
    public GameObject spawnObj;
    public float minTime;
    public float maxTime;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(spawnObj, transform.position, Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
