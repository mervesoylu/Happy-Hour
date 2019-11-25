using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace Project
{
    public class EndgameMenuController : MonoBehaviour
    {
        [SerializeField] Image _winnerID;
        [SerializeField] Image _winnerImage;

        public void Show(Player winner)
        {
             _winnerID.sprite = winner.VictorySprites;
             _winnerImage.sprite = winner.CharacterSprites;
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        void Start()
        {
            Hide();
        }
    }
}