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
                return;

            var velocity = direction * _settings.Speed;
            _rigidbody.velocity = velocity;
        }

        public void Aim(Vector3 direction)
        {
            if (_isStunned)
                return;

            if (direction == Vector3.zero)
                return;

            _facing = direction;

            _rigidbody.MoveRotation(Quaternion.LookRotation(_facing, _transform.up));
        }

        public void SetSpeed(float speed)
        {
            _settings.Speed = speed;
        }

        public void Throw()
        {
            if (_isStunned)
                return;

            StraightBottleController bottle = Instantiate(_settings.StraightBottle, _transform.position, Quaternion.identity);
            bottle.Fly(_facing, _colliders);
        }

        public void Toss()
        {
            if (_isStunned)
                return;

            var bottle = Instantiate(_settings.ArcBottle, _transform.position, Quaternion.identity).GetComponent<ArcBottleController>();
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
            Invoke(nameof(removeStun), _settings.StunDuration);
        }

        public int PlayerID { get; set; }

        public Sprite Sprite
        {
            get { return _settings.Sprite; }
        }

        public void Restart()
        {
            _hp = 4;
            gameObject.SetActive(true);
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
        bool _isStunned;
        Vector3 _facing;
        int _hp;
        List<Collider> _colliders;

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