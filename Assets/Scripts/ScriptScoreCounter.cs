using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptScoreCounter : MonoBehaviour
{
    public GameObject barrel;
    public float offsetY;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(barrel.transform.position.x, barrel.transform.position.y + offsetY, barrel.transform.position.z);
    }
}
