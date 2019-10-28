using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrowSound : MonoBehaviour
{
    public GameObject throwSound;
    public GameObject breakSound;

    void Start()
    {
        throwSound.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Character")
        {
            breakSound.SetActive(true);
        }
    }
}
