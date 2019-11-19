using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] SoundManager _soundManager;
        [SerializeField] AudioClip _deathAudioClip;
        [SerializeField] Animator _animator;
        [SerializeField] GameObject _dust;

        public List<GameObject> hps;
        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interface
        public void Move(Vector3 direction)
        {
            if (_isImmobilised || _isDead)
                return;

            var velocity = direction * _currentSettings.Speed;
            _rigidbody.velocity = velocity;

            _dust.SetActive(true);
        }

        public void Aim(Vector3 direction)
        {
            if (_isImmobilised || _isDead)
                return;

            if (direction == Vector3.zero)
                return;

            _facing = direction;
            _rigidbody.MoveRotation(Quaternion.LookRotation(_facing, _transform.up));
        }

        public void Throw()
        {
            if (_isImmobilised || _isDead)
                return;

            StraightBottleController bottle = Instantiate(_currentSettings.StraightBottle, _transform.position, Quaternion.identity);
            bottle.Fly(deviateDirection(_facing), _colliders);
            _animator.SetTrigger("isSThrow");
        }

        public void Toss()
        {
            if (_isImmobilised || _isDead)
                return;

            var bottle = Instantiate(_currentSettings.ArcBottle, _transform.position, Quaternion.identity).GetComponent<ArcBottleController>();
            bottle.Fly(_facing, _colliders);
            _animator.SetTrigger("isAThrow");

        }

        public void TakeDamage()
        {
            if (_isInvincible)
                return;

            _animator.SetBool("isHit", true);

            if (_hp > 0)
            {
                _hp--;
                hps[_hp].SetActive(false);

                if (_hp <= 0)
                    die();
                else
                {
                    makeInvincible();
                    Invoke(nameof(makeVincible), _currentSettings.Recovery);
                }
            }

        }
        bool _isInvincible;

        public void HitEffect(Vector3 force)
        {
            if (_isInvincible)
                return;

            _isImmobilised = true;
            Invoke(nameof(removeImmobility), _currentSettings.ImmobilityDuration);

            _rigidbody.AddForce(force, ForceMode.Impulse);
            Invoke(nameof(stopHitEffect), _currentSettings.ImmobilityDuration);
        }

        void stopHitEffect()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        public void GetStunned()
        {
            _isImmobilised = true;
            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("isStun", true);
            Invoke(nameof(removeImmobility), _currentSettings.StunDuration);
        }

        public int PlayerID { get; set; }

        public Sprite Sprite; // can't be a property, because it needs to show up in the inspector.
        public Sprite CharacterSprite;
        public Color Color;

        public void Restart()
        {

            _isDead = false;
            _hp = 3;
            gameObject.SetActive(true);
            foreach (var hp in hps)
            {
                hp.SetActive(true);
            }
        }

        public void OnHappyHourRan()
        {
            _currentSettings = _happyHourSettings;
        }

        public void OnHappyHourStopped()
        {
            _currentSettings = _defaultSettings;
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            _colliders = new List<Collider>(GetComponentsInChildren<Collider>(false));
            _material = findMainMaterial();
            //_animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            _hp = 3;
            _facing = _transform.forward;
            _currentSettings = _defaultSettings;
        }

        void OnEnable()
        {
            makeVincible();
        }

        void Update()
        {
            if (_rigidbody.velocity == Vector3.zero)
            {
                _dust.SetActive(false);
                _animator.SetFloat("Horizontal", 0);
                _animator.SetFloat("Vertical", 0);
                return;
            }


            Quaternion relativeRotation = Quaternion.FromToRotation(_transform.forward, _rigidbody.velocity);

            Vector3 rotatedAxis = relativeRotation * Vector3.forward;

            if (relativeRotation.eulerAngles.y > 90 + forwardBackwardThreshold && relativeRotation.eulerAngles.y < 270 - forwardBackwardThreshold)
                rotatedAxis.x = -rotatedAxis.x;


            _animator.SetFloat("Vertical", rotatedAxis.z);
            _animator.SetFloat("Horizontal", rotatedAxis.x);


            _animator.SetBool("isHit", false);
        }
        [SerializeField] float forwardBackwardThreshold;
        #endregion

        #region ------------------------------details
        CharacterSettings _currentSettings;
        bool _isImmobilised;
        Vector3 _facing;
        int _hp;
        List<Collider> _colliders;

        Material findMainMaterial()
        {
            return new List<Material>(GetComponentInChildren<Renderer>().sharedMaterials).FirstOrDefault(m => m.name.Contains("Character"));
        }

        void removeImmobility()
        {
            _isImmobilised = false;
            _animator.SetBool("isStun", false);
        }

        void die()
        {
            _isDead = true;
            _soundManager.PlayAudioClip(_deathAudioClip);
            _animator.SetTrigger("isDead");
            _rigidbody.velocity = Vector3.zero;
            Invoke(nameof(dieOverTime), deadTime);
        }
        float deadTime = 3.0f;
        bool _isDead;

        void dieOverTime()
        {
            gameObject.SetActive(false);
            _round.OnPlayerDied(PlayerID);
        }

        Vector3 deviateDirection(Vector3 direction)
        {
            Vector3 result;

            float randomAngle = Random.Range(-_currentSettings.MaxDeviationAmount, _currentSettings.MaxDeviationAmount);
            result = Quaternion.AngleAxis(randomAngle, Vector3.up) * direction;

            return result;
        }

        void makeInvincible()
        {
            _isInvincible = true;
            StartCoroutine(nameof(showInvincibility));
        }

        void makeVincible()
        {
            _isInvincible = false;
            StopCoroutine(nameof(showInvincibility));
            setMaterialAlphaTo(1.0f);
        }

        IEnumerator showInvincibility()
        {
            while (true)
            {
                setMaterialAlphaTo(0.0f);
                yield return new WaitForSeconds(0.15f);
                setMaterialAlphaTo(1.0f);
                yield return new WaitForSeconds(0.15f);
            }
        }

        void setMaterialAlphaTo(float value)
        {
            Color color = _material.color;
            color.a = value;
            _material.color = color;
        }
        Material _material;
        #endregion
    }
}