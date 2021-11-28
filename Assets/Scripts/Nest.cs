using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public bool Occupied
    {
        get { return occupied; }
    }
    private bool occupied;

    private void Start()
    {
        occupied = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            occupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        occupied = false;
    }
}
