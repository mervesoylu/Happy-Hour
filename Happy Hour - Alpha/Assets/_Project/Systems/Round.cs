﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Round : MonoBehaviour
    {
        #region ------------------------------dependencies
        [Inject] Game _game;
        [Inject] List<Transform> _spawnPoints;
        [SerializeField] Bar _bar;
        List<CharacterController> _characters;
        #endregion

        #region ------------------------------interface
        public void Begin(List<CharacterController> characters, bool isFirstRound)
        {
            StartCoroutine(nameof(spawnBarrelTracker));

            _characters = characters;

            if (!isFirstRound)
                _spawnPoints.Shuffle();


            for (int i = 0; i < _characters.Count; i++)
                _characters[i].transform.position = _spawnPoints[i].position;

            _characters.ForEach(ch => ch.Restart());
        }
        bool _isFirstRound = true;

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
            for (int i = 0; i < _characters.Count; i++)
                if (_characters[i].PlayerID == playerID)
                    _characters.RemoveAt(i);

            if (_characters.Count == 1)
                finishRound();
        }
        #endregion

        #region ------------------------------details
        IEnumerator spawnBarrelTracker()
        {
            while (true)
            {
                yield return new WaitForSeconds(_barrelSpawnInterval);
                _bar.SpawnBarrel();
            }
        }
        [SerializeField] float _barrelSpawnInterval;

        void runHappyHour()
        {
            throw new System.NotImplementedException();
        }

        void finishRound()
        {
            StopCoroutine(nameof(spawnBarrelTracker));
            _game.OnRoundFinished(_characters[0].PlayerID);
        }
        #endregion
    }
}
