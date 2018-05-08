using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
