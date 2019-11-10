using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour
{
    GameObject _confetti;

    void Disabled()
    {
        _confetti.SetActive(false);
    }

    void Enabled()
    {
        _confetti.SetActive(true);
    }

}
