using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CharacterController : MonoBehaviour
    {
        #region ------------------------------dependencies
        [SerializeField] CharacterSettings _settings;
        [Inject] Round _round;

        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interface
        public void Move(Vector3 direction)
        {
            if (_isStunned)
            { return; }

            Vector3 velocity = direction * _settings.MovementSettings.Speed;
            _rigidbody.velocity = velocity;

            if (velocity != Vector3.zero)
            {
                _facingDirection = velocity.normalized;
                Orientate(_facingDirection);
            }
        }

        public void Aim(Vector3 direction)
        {
            if (direction != Vector3.zero)
            { _aimDirection = direction; }
        }

        public void SetSpeed(float speed)
        {
            _settings.MovementSettings.Speed = speed;
        }

        public void Throw()
        {
            StraightBottle bottle = Instantiate(_settings.StraightBottle, _transform.position, Quaternion.identity);
            bottle.Throw(_aimDirection, _colliders);
        }

        public void Toss()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage()
        {
            if (_hitpoints > 0)
            {
                _hitpoints--;
                Debug.Log(string.Format("{0} took damage, {0} is now at {1} hitpoints!", gameObject.name, _hitpoints));
            }
        }

        public void GetStunned()
        {
            _isStunned = true;
            _rigidbody.velocity = Vector3.zero;
            Invoke(nameof(RemoveStun), _settings.MovementSettings.StunDuration);
        }

        public int PlayerID { get; set; }
        public Sprite Sprite
        {
            get { return _settings.Sprite; }
        }
        #endregion

        #region ------------------------------Unity messages
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            _colliders = new List<Collider>(GetComponentsInChildren<Collider>(false));
        }
        #endregion

        #region ------------------------------details
        private void Orientate(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.x, direction.z);
            transform.rotation = Quaternion.Euler(Vector3.up * Mathf.Rad2Deg * angle);
        }

        void RemoveStun()
        {
            _isStunned = false;
        }

        Vector3 _aimDirection = Vector3.forward;
        Vector3 _facingDirection = Vector3.forward;
        bool _isStunned = false;
        int _hitpoints;
        List<Collider> _colliders;
        #endregion
    }
}