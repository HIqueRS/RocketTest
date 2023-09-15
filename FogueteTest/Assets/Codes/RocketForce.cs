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
    [SerializeField]
    private float _dragForce;


    private float _maxHeight;

    [SerializeField]
    private GameObject _parachute;

    [SerializeField]
    private GameObject[] _stages;

    private int _currentStage;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _seconds = 0;
        _maxHeight = 0;
        _currentStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PassTime();
        DetectMaxHeight();
    }

    private void FixedUpdate()
    {

        TestFuel();
    }

    private void DetectMaxHeight()
    {
        if(_stages[_stages.Length-1] == null)
        {
            if (_body.velocity.y <= 0)
            {
                if (transform.position.y > _maxHeight)
                {
                    _maxHeight = transform.position.y;

                    Debug.Log(_maxHeight);

                    OpenParachute();
                }
            }
        }
        
    }

    private void OpenParachute()
    {
        _parachute.SetActive(true);
        _body.drag = _dragForce;
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
        else
        {
            TestToDetachStage();
        }
    }

    private void TestToDetachStage()
    {
        if(_currentStage < _stages.Length -1)
        {
            DetachStage(_currentStage);
            _currentStage++;
            _seconds = 0;
        }
        else
        {
            DetachStage(_currentStage);
           
           
        }
    }

    private void DetachStage(int i)
    {
        if(_stages[i] != null)
        {
            Rigidbody _stageBody;

            _stages[i].transform.SetParent(null);
            _stageBody = _stages[i].AddComponent<Rigidbody>(); 
            _stageBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _stageBody.velocity = _body.velocity * 0.9f;

            _body.mass -= 1;

            _stages[i] = null;
        }
    }
}
