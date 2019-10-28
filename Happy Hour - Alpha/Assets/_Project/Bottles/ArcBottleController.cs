using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ArcBottleController : MonoBehaviour
    {
        #region ------------------------------dependencies
        Transform _transform;
        [SerializeField] GameObject _spillPrefab;
        [SerializeField] AudioClip _flyAudioClip;
        [SerializeField] AudioClip _hitAudioClip;
        [Inject] SoundManager _soundManager;
        #endregion

        #region ------------------------------interface
        public void Fly(Vector3 direction, List<Collider> ownerColliders)
        {
            _ownerColliders = ownerColliders;
            StartCoroutine(nameof(flyOverTime), direction);
            _soundManager.PlayAudioClip(_flyAudioClip);
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _transform = transform;
        }

        void OnTriggerEnter(Collider other)
        {
            if (_ownerColliders.Contains(other))
                return;

            if (other.gameObject.tag == "Spill")
                return;

            if (other.gameObject.tag == "Floor")
            {
                Instantiate(_spillPrefab, _transform.position, Quaternion.identity);
                StopCoroutine(nameof(flyOverTime));
                _soundManager.PlayAudioClip(_hitAudioClip);
                Destroy(gameObject);
            }
        }
        #endregion

        #region ------------------------------details
        IEnumerator flyOverTime(Vector3 direction)
        {
            Vector3 startPos = _transform.position;
            Vector3 endPos = startPos + direction * _arcSize.x;
            endPos.y = 1f; // to make sure bottle reaches the ground
            float trajectoryHeight = _arcSize.y;
            var startTime = Time.time;

            while (true)
            {
                var currentTime = Time.time - startTime;
                float t = currentTime * _speed;
                Vector3 currentPos = Vector3.Lerp(startPos, endPos, _trajectory.Evaluate(t));

                // give a curved trajectory in the Y direction
                currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);

                _transform.position = currentPos;
                yield return null;
            }
        }
        [SerializeField] float _speed;
        [SerializeField] Vector2 _arcSize;
        [SerializeField] AnimationCurve _trajectory;
        List<Collider> _ownerColliders;
        #endregion
    } 
}
