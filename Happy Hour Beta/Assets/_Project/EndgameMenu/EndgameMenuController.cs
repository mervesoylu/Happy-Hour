using System;
using UnityEngine;
using TMPro;

namespace Project
{
    public class EndgameMenuController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _winnerID;

        public void Show(Player winner)
        {
            _winnerID.text = string.Format("Player {0}", winner.Controller);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}