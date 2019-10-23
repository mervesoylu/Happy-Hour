using UnityEngine;
using System.Collections.Generic;

namespace Project
{
    public class StraightBottleController : MonoBehaviour
    {
        #region ------------------------------dependencies
        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interfaces
        public void Fly(Vector3 direction, List<Collider> ownerColliders)
        {
            _ownerColliders = ownerColliders;
            _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
            _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
        }
        #endregion

        #region ------------------------------unity messages
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_ownerColliders.Contains(other))
            {
                if (other.CompareTag("Character"))
                {
                    CharacterController character = other.GetComponent<CharacterController>();
                    character.TakeDamage();
                    character.HitEffect(_rigidbody.velocity.normalized * _knockbackForce);
                }

                Destroy(gameObject);
            }
        }
        #endregion

        #region ------------------------------details
        [SerializeField] float _speed;
        [SerializeField] float _angularSpeed;
        [SerializeField] float _knockbackForce;
        List<Collider> _ownerColliders;
        #endregion
    }
}