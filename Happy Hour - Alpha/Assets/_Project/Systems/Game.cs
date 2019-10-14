#define KEYBOARD
#undef KEYBOARD

using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using XboxCtrlrInput;

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
        /// It checks for winning condition before starting the next round.
        /// </summary>
        public void OnRoundFinished(int winnerPlayerNumber)
        {
            _isRoundBegan = false;
            _isFirstRound = false;

            _players
                .Find(p => (int)p.Controller == winnerPlayerNumber)
                .Score++;

            _boardController.Show(_players);

            //check for game over condition
            Player winner = _players.FirstOrDefault(p => p.Score == 3);

            if (winner != null)
            {
                _isGameEnded = true;
                _isFirstRound = true;
            }
        }
        #endregion

        #region ------------------------------details
        public void Initialize()
        {
            SetupPlayers();
            _isFirstRound = true;
        }

        public void Tick()
        {
            if (_isRoundBegan)
                return;

#if KEYBOARD
            if (Input.GetKeyDown(KeyCode.Space))
#else
            if (XCI.GetButtonDown(XboxButton.A))
#endif
                if (_isGameEnded)
                {
                    _isGameEnded = false;
                    SetupPlayers();
                    return;
                }
                else
                    beginRound();
        }
        bool _isRoundBegan;
        bool _isGameEnded;

        /// <summary>
        /// It assigns character models to players randomly and starts the first round.
        /// </summary>
        void SetupPlayers()
        {
            _characters.Shuffle();
            for (int i = 0; i < _players.Count; i++)
            {
                _characters[i].GetComponent<CharacterInput>().SetXBoxController(_players[i].Controller);
                _characters[i].GetComponent<CharacterController>().PlayerID = (int)_players[i].Controller;
                _players[i].Sprite = _characters[i].GetComponent<CharacterController>().Sprite;
                _players[i].Score = 0;
            }

            _boardController.Show(_players);
        }

        void beginRound()
        {
            _isRoundBegan = true;
            _boardController.Hide();
            _round.Begin(_characters.Select(c => c.GetComponent<CharacterController>()).ToList(), _isFirstRound);
        }
        bool _isFirstRound;
        #endregion
    }
}
