using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        // Nice rotation for the pickups
        transform.Rotate(new Vector3(Random.Range(0, 15), Random.Range(15, 30), Random.Range(30, 45)) * Time.deltaTime);
    }
}
