using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketForce : MonoBehaviour
{

    private Rigidbody _body;
    [SerializeField]
    private float _force;

   

    [SerializeField]
    private float _timeLimit;
    [SerializeField]
    private float _dragForce;


    private float _maxHeight;

    [SerializeField]
    private GameObject _parachute;

    private int _currentStage;

    [SerializeField]
    private StageController[] _stagesC;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        
        _maxHeight = 0;
        _currentStage = 0;

        _stagesC[_currentStage].InitializeStage();

        
    }

    // Update is called once per frame
    void Update()
    {
       
        DetectMaxHeight();
    }

    private void FixedUpdate()
    {
        if(_stagesC[_stagesC.Length - 1] != null)
        {
            TestFuel();
        }
        
    }

    private void DetectMaxHeight()
    {
        if(_stagesC[_stagesC.Length-1] == null)
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

   

    private void AddingForce()
    {
        _body.AddRelativeForce(Vector3.up * _force);
    }

    private void TestFuel()
    {

        if(_stagesC[_currentStage].GetFuel() > 0)
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
        if(_currentStage < _stagesC.Length -1)
        {
            DetachStage();

            NextStage();
        }
        else
        {
            DetachStage();


        }
        
    }

    private void NextStage()
    {
        _currentStage++;

        _stagesC[_currentStage].InitializeStage();
    }

    private void DetachStage()
    {
        _stagesC[_currentStage].DetachStage(_body.velocity);

        _stagesC[_currentStage] = null;

        _body.mass -= 1;
    }

    
}
