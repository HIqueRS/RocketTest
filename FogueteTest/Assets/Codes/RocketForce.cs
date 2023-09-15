using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketForce : MonoBehaviour
{

    private Rigidbody _body;
    [SerializeField]
    private float _force;

    private float _seconds;

    [SerializeField]
    private float _timeLimit;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PassTime();
    }

    private void FixedUpdate()
    {

        TestFuel();
    }

    private void PassTime()
    {
        _seconds += Time.deltaTime;
    }

    private void AddingForce()
    {
        _body.AddForce(Vector3.up * _force);
    }

    private void TestFuel()
    {
        if (_seconds < _timeLimit)
        {
            AddingForce();
        }
    }
}
