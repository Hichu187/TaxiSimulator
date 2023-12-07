using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNAv : MonoBehaviour
{
    public Transform destination;
    bool test = false;
    void Start()
    {
        EventController.instance.carStop += CarStop;
    }

    // Update is called once per frame
    void Update()
    {
        if (test == true)
        {

        }

    }
    void CarStop()
    {
        GetComponent<NavMeshAgent>().SetDestination(destination.position);
    }
}
