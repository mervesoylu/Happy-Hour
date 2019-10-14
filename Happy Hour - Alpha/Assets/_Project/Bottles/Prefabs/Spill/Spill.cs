using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    #region ----------------------dependencies
    #endregion

    #region ----------------------interfaces
    #endregion

    #region ----------------------unity message
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifetime)
        { Destroy(gameObject); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.GetComponent<Project.CharacterController>().GetStunned();
        }
    }
    #endregion

    #region ----------------------details
    [SerializeField] float _lifetime;
    float _timer;
    #endregion
}
