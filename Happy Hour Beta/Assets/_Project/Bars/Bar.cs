using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    #region --------------------------dependencies
    [SerializeField] BarrelController _barrelPrefab;
    #endregion

    #region --------------------------interfaces
    public void SpawnBarrel()
    {
        int side = Random.Range(0, _sides);

        if (side == _previousSide)
        {
            if (side == _sides - 1)
                side--;
            else
                side++;
        }

        _previousSide = side;

        float unitCircle = (360f / _sides * side) * Mathf.Deg2Rad;
        Vector3 launchDirection = new Vector3(Mathf.Cos(unitCircle), 0f, Mathf.Sin(unitCircle));
        Vector3 spawnPosition = _barOrigin.position + _barRadius * launchDirection;
        Quaternion initialRotation = _transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(launchDirection, Vector3.up);

        StopCoroutine("SpawnBarrelCoroutine");
        StartCoroutine(SpawnBarrelCoroutine(initialRotation, targetRotation, spawnPosition, launchDirection));
    }
    int _previousSide = 0;
    #endregion

    #region --------------------------unity messages
    private void Awake()
    {
        _transform = transform;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    { SpawnBarrel(); }
    //}
    #endregion

    #region --------------------------details
    private IEnumerator SpawnBarrelCoroutine(Quaternion initialRotation, Quaternion targetRotation, Vector3 spawnPosition, Vector3 launchDirection)
    {
        float startTime = Time.time;

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float delta = elapsedTime / _rotationDuration;

            if (delta > 1f)
                break;

            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, delta);

            yield return null;
        }

        yield return new WaitForSeconds(_launchInterval);

        Instantiate(_barrelPrefab, spawnPosition, Quaternion.LookRotation(launchDirection, Vector3.up)).Roll(launchDirection);
    }

    [SerializeField] Transform _barOrigin;
    [SerializeField] float _barRadius;
    [SerializeField] int _sides;
    [SerializeField] float _rotationDuration;
    [SerializeField] float _launchInterval;

    public float LaunchDuration { get { return _rotationDuration + _launchInterval; } }

    Transform _transform;
    #endregion
}