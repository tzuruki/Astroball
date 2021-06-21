using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvHazardScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /*Debug.Log("Colliding with player");

            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            playerRb.AddForce(Vector3.up * 15f, ForceMode.Impulse);

            playerRb.AddForce(new Vector3(1f, 0.0f, Random.Range(-1, 2)) * 3f, ForceMode.Impulse);*/

            PlayerStats.Health--;
        }
    }
}
