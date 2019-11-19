using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Project
{


    public class BoardController : MonoBehaviour
    {
        #region ------------------------------dependencies
        [SerializeField] List<Image> _playerImages;
        [SerializeField] List<PointBarController> _pointbarContoller;
        #endregion

        #region ------------------------------interface
        public void Show(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                _playerImages[i].sprite = players[i].CharacterSprites;
                _pointbarContoller[i].DisplayBar(players[i]);
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        #endregion
        void Start()
        {
            Hide();
        }
    }
}
