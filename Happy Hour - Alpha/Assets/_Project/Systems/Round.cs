using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Round
    {
        #region ------------------------------dependencies
        [Inject] Game _game;
        [Inject] List<Transform> _spawnPoints;
        List<CharacterController> _characters;
        #endregion

        #region ------------------------------interface
        public void Begin(List<CharacterController> characters)
        {
            _characters = characters;
            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].transform.position = _spawnPoints[i].position;
            }
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// It handles player death.
        /// </summary>
        /// <param name="playerID"> It is the Player's Controller attribute</param>
        public void OnPlayerDied(int playerID)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region ------------------------------details
        void spawnBarrel()
        {
            throw new System.NotImplementedException();
        }

        void runHappyHour()
        {
            throw new System.NotImplementedException();
        }

        void finishRound()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
