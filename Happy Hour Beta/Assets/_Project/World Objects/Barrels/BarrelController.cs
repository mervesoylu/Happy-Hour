using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    #region ----------------------------dependencies
    Rigidbody _rigidbody;
    [SerializeField] ParticleSystem _barrelBreak;
    [SerializeField] ParticleSystem _dust;
    public Renderer _rend;
    #endregion

    #region ----------------------------interfaces
    public void Roll(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
        _dust.Play();
    }
    #endregion

    #region ----------------------------unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _barrelBreak.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
            return;

        if (other.CompareTag("Spill"))
            return;

        if (other.CompareTag("Bottle"))
            return;

        if (other.CompareTag("Character"))
        {
            other.GetComponent<Project.CharacterController>().TakeDamage();
            _barrelBreak.Play();
            _dust.Stop();
        }

        _rend.enabled = false;
        _dust.Stop();
        _barrelBreak.Play();
        Invoke(nameof(Break), _break);
    }
    float _break = 2.0f;


    void Break()
    {
        Destroy(gameObject);
    }
    #endregion

    #region ----------------------------details
    [SerializeField] float _speed;

    Transform _transform;
    #endregion
}