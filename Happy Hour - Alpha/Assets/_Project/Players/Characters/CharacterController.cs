using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CharacterController : MonoBehaviour
    {
        #region ------------------------------dependencies
        [Inject(Id = "defaultCharacterSettings")] CharacterSettings _defaultSettings;
        [Inject(Id = "happyHourCharacterSettings")] CharacterSettings _happyHourSettings;
        [Inject] Round _round;

        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interface
        public void Move(Vector3 direction)
        {
            if (_isStunned)
                return;

            var velocity = direction * _currentSettings.Speed;
            _rigidbody.velocity = velocity;
        }

        public void Aim(Vector3 direction)
        {
            if (_isStunned)
                return;

            if (direction == Vector3.zero)
                return;

            _facing = direction;
            orientate(_facing);
            //_rigidbody.MoveRotation(Quaternion.LookRotation(_facing, _transform.up));
        }

        public void Throw()
        {
            if (_isStunned)
                return;

            StraightBottleController bottle = Instantiate(_currentSettings.StraightBottle, _transform.position, Quaternion.identity);
            bottle.Fly(_facing, _colliders);
        }

        public void Toss()
        {
            if (_isStunned)
                return;

            var bottle = Instantiate(_currentSettings.ArcBottle, _transform.position, Quaternion.identity).GetComponent<ArcBottleController>();
            bottle.Fly(_facing, _colliders);
        }

        public void TakeDamage()
        {
            if (_hp > 0)
                _hp--;

            if (_hp <= 0)
                die();

            print(gameObject.name + ": " + _hp);
        }
        public void GetStunned()
        {
            _isStunned = true;
            _rigidbody.velocity = Vector3.zero;
            Invoke(nameof(removeStun), _currentSettings.StunDuration);
        }

        public int PlayerID { get; set; }

        public Sprite Sprite; // can't be a property, because it needs to show up in the inspector.

        public void Restart()
        {
            _hp = 4;
            gameObject.SetActive(true);
        }
        public void OnHappyHourRan()
        {
            throw new System.NotImplementedException();
        }

        public void OnHappyHourStopped()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            _colliders = new List<Collider>(GetComponentsInChildren<Collider>(false));
        }

        void Start()
        {
            _hp = 4;
            _facing = _transform.forward;
        }
        #endregion

        #region ------------------------------details
        CharacterSettings _currentSettings;
        bool _isStunned;
        Vector3 _facing;
        int _hp;
        List<Collider> _colliders;

        void orientate(Vector3 direction)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
            _transform.rotation = Quaternion.Euler(Vector3.up * angle);
        }

        void removeStun()
        {
            _isStunned = false;
        }

        void die()
        {
            print("die");
            gameObject.SetActive(false);
            _round.OnPlayerDied(PlayerID);
        }
        #endregion
    }
}