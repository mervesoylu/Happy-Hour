using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBottleController : MonoBehaviour
{
    #region ------------------------------dependencies
    Transform _transform;
    [SerializeField] GameObject _spillPrefab;
    #endregion

    #region ------------------------------interface
    public void Fly(Vector3 direction, List<Collider> ownerColliders)
    {
        _ownerColliders = ownerColliders;
        StartCoroutine(nameof(flyOverTime), direction);
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

        if (other.gameObject.tag == "Floor")
        {
            Instantiate(_spillPrefab, _transform.position, Quaternion.identity);
            StopCoroutine(nameof(flyOverTime));
            Destroy(gameObject);
        }
    }
    #endregion

    #region ------------------------------details
    IEnumerator flyOverTime(Vector3 direction)
    {
        Vector3 startPos = _transform.position;
        Vector3 endPos = startPos + direction * _arcSize.x;
        endPos.y = -0.1f; // to make sure bottle reaches the ground
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
