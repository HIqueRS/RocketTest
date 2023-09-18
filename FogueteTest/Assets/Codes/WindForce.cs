using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private float _force;

    private void OnTriggerStay(Collider other)
    {
        

        if(other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().AddForce(_direction * _force);
        }
        
    }
}
