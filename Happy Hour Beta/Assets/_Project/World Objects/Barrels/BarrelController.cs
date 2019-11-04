using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    #region ----------------------------dependencies
    Rigidbody _rigidbody;
    #endregion

    #region ----------------------------interfaces
    public void Roll(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
    }
    #endregion

    #region ----------------------------unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
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
            other.GetComponent<Project.CharacterController>().TakeDamage();

        Destroy(gameObject);
    }
    #endregion

    #region ----------------------------details
    [SerializeField] float _speed;

    Transform _transform;
    #endregion
}
