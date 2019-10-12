﻿using UnityEngine;
using System.Collections.Generic;

namespace Project
{
    public class StraightBottle : MonoBehaviour
    {
        #region ------------------------------dependencies
        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interfaces
        public void Throw(Vector3 direction, List<Collider> ownerColliders)
        {
            _ownerColliders = ownerColliders;
            _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
        }
        #endregion

        #region ------------------------------unity messages
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(_transform.right * _angularSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_ownerColliders.Contains(other))
            {
                if (other.CompareTag("Character"))
                {
                    CharacterController character = other.GetComponent<CharacterController>();
                    character.TakeDamage();
                }

                Destroy(gameObject);
            }
        }
        #endregion

        #region ------------------------------details
        [SerializeField] float _speed;
        [SerializeField] float _angularSpeed;
        List<Collider> _ownerColliders;
        #endregion
    }
}