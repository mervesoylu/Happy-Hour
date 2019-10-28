using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




namespace Project
{
    public class ScoreMoniter : MonoBehaviour
    {
        #region ------------------------------dependencies
        [SerializeField] List<Image> _playerScores;
        //[SerializeField] List<TextMeshProUGUI> _playerScores;
        #endregion

        #region ------------------------------interface
        public void Display(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                _playerScores[i].sprite = players[i].Sprite;
                _playerScores[i].GetComponentInChildren<TextMeshProUGUI>().text = players[i].Score.ToString();

                //_playerScores[i].text = players[i].Score.ToString();
            }
            gameObject.SetActive(true);
        }

        public void Hidden()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}