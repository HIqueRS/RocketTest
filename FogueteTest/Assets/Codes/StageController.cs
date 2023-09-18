using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private float _fuel;
    [SerializeField]
    private AudioSource _sound;

    private float _seconds;

    [SerializeField]
    private GameObject _particle;

    private Rigidbody _body;

    private bool _initiate;

    // Start is called before the first frame update
    void Start()
    {
        _initiate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_initiate)
        {
            PassTime();
            UsingFuel();
        }
    }

    private void PassTime()
    {
        _seconds += Time.deltaTime;
    }

    public float GetFuel()
    {
        return _fuel;
    }

    private void UsingFuel()
    {
        if(_fuel >= 0 )
        {
            _fuel -= _seconds;
        }
    }

    private void EnableParticle(bool active)
    {
        _particle.SetActive(active);
    }

    private void InitiateSound()
    {
        _sound.Play();
    }

    private void EndSound()
    {
        _sound.Stop();
    }

    private void AddingBody()
    {
        
        _body = gameObject.AddComponent<Rigidbody>();

        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void SetBodyVelocity(Vector3 vel)
    {
        _body.velocity = vel * 0.9f;
    }

    private void OutParent()
    {
        transform.SetParent(null);
    }

    public void InitializeStage()
    {
        _initiate = true;

        InitiateSound();
        EnableParticle(true);
    }

    public void DetachStage(Vector3 vel)
    {
        AddingBody();
        SetBodyVelocity(vel);

        EnableParticle(false);
        EndSound();
        OutParent();
    }



    //função pra passar quanto do fuel ja gastou

    //função pra ligar particulas

    //função pra ligar som

    //função para adicionar rigidbody

    //função para trocar o pai
}
