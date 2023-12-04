using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePhysic : MonoBehaviour
{
    Rigidbody rid;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rid = GetComponent<Rigidbody>();

            rid.isKinematic = false;
            rid.useGravity = true;
            Invoke("Disable",Random.Range(7,10));
        }
    }

    void Disable(){

        this.gameObject.SetActive(false);
    }
}
