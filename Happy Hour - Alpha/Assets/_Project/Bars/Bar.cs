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
        int hatch = Random.Range(0, _barHatches);
        float unitCircle = (360f / _barHatches * hatch) * Mathf.Deg2Rad;
        _currentDirection = new Vector3(Mathf.Cos(unitCircle), 0f, Mathf.Sin(unitCircle));
        _initialRotation = _transform.rotation;
        transform.rotation = Quaternion.LookRotation(_currentDirection, Vector3.up);
        Vector3 spawnPosition = _barOrigin.position + _barRadius * _currentDirection;

        Instantiate(_barrelPrefab, spawnPosition, Quaternion.identity).Roll(_currentDirection);
    }
    #endregion

    #region --------------------------unity messages
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { SpawnBarrel(); }
    }
    #endregion

    #region --------------------------details
    [SerializeField] Transform _barOrigin;
    [SerializeField] float _barRadius;
    [SerializeField] int _barHatches;
    Vector3 _currentDirection;
    Quaternion _initialRotation;

    Transform _transform;
    #endregion
}
