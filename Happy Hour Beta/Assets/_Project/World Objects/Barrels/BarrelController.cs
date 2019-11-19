using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    #region ----------------------------dependencies
    Rigidbody _rigidbody;
    [SerializeField] ParticleSystem _barrelBreak;
    [SerializeField] GameObject _dust;
    public Renderer _rend;
    #endregion

    #region ----------------------------interfaces
    public void Roll(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
        _dust.SetActive(true);
    }
    #endregion

    #region ----------------------------unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _barrelBreak.Stop();
        _barrelBreak = GetComponent<ParticleSystem>();
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
        }

        _rend.enabled = false;
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