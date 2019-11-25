using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace Project
{
    public class EndgameMenuController : MonoBehaviour
    {
        [SerializeField] List<Image> _winnerID;

        public void Show(List<Player> winner)
        {
            for (int i = 0; i < winner.Count; i++)
            {
                _winnerID[i].sprite = winner[i].CharacterSprites;
            }
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

        internal void Show(Player winner)
        {
            throw new NotImplementedException();
        }
    }
}