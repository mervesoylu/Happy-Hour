using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Project
{
    public class Game : IInitializable, ITickable
    {
        #region ------------------------------dependencies
        [Inject] List<Player> _players;
        [Inject] List<GameObject> _characters;
        [Inject] BoardController _boardController;
        [Inject] Round _round;
        #endregion

        #region ------------------------------interface
        /// <summary>
        /// It assigns character models to players randomly and starts the first round.
        /// </summary>
        public void Setup()
        {
            _characters.Shuffle();
            for (int i = 0; i < _players.Count; i++)
            {
                _characters[i].GetComponent<CharacterInput>().SetXBoxController(_players[i].Controller);
                _players[i].Sprite = _characters[i].GetComponent<CharacterController>().Sprite;
                //_players[i].Score = 0;
            }

            _boardController.Show(_players);
        }

        /// <summary>
        /// It checks for winning condition before starting the next round.
        /// </summary>
        public void OnRoundFinished(int winnerPlayerNumber)
        {
            _players
                .Find(p => (int)p.Controller == winnerPlayerNumber)
                .Score++;

            //check for game over condition
        }
        #endregion

        #region ------------------------------details
        public void Initialize()
        {
            Setup();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                beginRound();
        }

        void beginRound()
        {
            _boardController.Hide();
            _round.Begin(_characters.Select(c => c.GetComponent<CharacterController>()).ToList());
        }

        #endregion
    }
}
