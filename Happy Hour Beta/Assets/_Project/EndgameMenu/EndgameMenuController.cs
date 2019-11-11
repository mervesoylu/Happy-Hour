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
            _winnerID.text = string.Format("{0} Player", winner.Controller);
            _winnerID.color = new Color(winner.Color.r, winner.Color.g, winner.Color.b, 1f);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}