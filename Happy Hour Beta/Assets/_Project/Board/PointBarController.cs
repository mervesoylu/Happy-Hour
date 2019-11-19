using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Project
{

    public class PointBarController : MonoBehaviour
    {
        [SerializeField] List<GameObject> _bars;
        [SerializeField] List<Image> _barImages;

        public void DisplayBar(Player player)
        {
            foreach (var image in _barImages)
            {
                image.color = player.Color;
            }

            _bars[0].SetActive(false);
            _bars[1].SetActive(false);

            if (player.Score == 1)
            {
                _bars[0].SetActive(true);
            }

            if (player.Score == 2)
            {
                _bars[1].SetActive(true);
            }
        }
    }

}
