using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Zenject;

namespace Project
{
    public class Round : MonoBehaviour
    {
        #region ------------------------------dependencies
        [Inject] Game _game;
        [Inject] List<Transform> _spawnPoints;
        List<CharacterController> _characters;
        [SerializeField] Bar _bar;
        [SerializeField] TextMeshProUGUI _happyHourTextUI;
        #endregion

        #region ------------------------------interface
        public void Begin(List<CharacterController> characters)
        {
            _characters = characters;

            _roundCounter++;
            StartCoroutine(nameof(spawnBarrelTracker));

            if (_roundCounter == 1)
                _spawnPoints.Shuffle();

            for (int i = 0; i < _characters.Count; i++)
                _characters[i].transform.position = _spawnPoints[i].position;

            _characters.ForEach(ch => ch.Restart());
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

        public void ResetRoundCounter()
        {
            _roundCounter = 0;
        }

        #endregion

        #region ------------------------------Unity messages
        void Start()
        {
            _happyHourTextUI.gameObject.SetActive(false);
        }
        #endregion

        #region ------------------------------details
        IEnumerator spawnBarrelTracker()
        {
            _spawnedBarrelsCounter = 0;

            while (true)
            {
                yield return new WaitForSeconds(_barrelSpawnInterval);
                spawnBarrel();
                if (_spawnedBarrelsCounter == _numberOfBarrelsToInitiateHappuHour)
                {
                    _spawnedBarrelsCounter = 0;
                    runHappyHour();
                    yield return new WaitForSeconds(_happyHourDuration);
                    stopHappyHour();
                }
            }
        }

        private void spawnBarrel()
        {
            _bar.SpawnBarrel();
            _spawnedBarrelsCounter++;
        }
        [SerializeField] float _barrelSpawnInterval;
        [SerializeField] int _numberOfBarrelsToInitiateHappuHour;
        [SerializeField] float _happyHourDuration;
        int _spawnedBarrelsCounter;

        void runHappyHour()
        {
            _happyHourTextUI.gameObject.SetActive(true);

            foreach (var character in _characters)
            {
                character.OnHappyHourRan();
                character.GetComponent<CharacterInput>().OnHappyHourRan();
            }
        }

        void stopHappyHour()
        {
            _happyHourTextUI.gameObject.SetActive(false);

            foreach (var character in _characters)
            {
                character.OnHappyHourStopped();
                character.GetComponent<CharacterInput>().OnHappyHourStopped();
            }
        }

        void finishRound()
        {
            StopCoroutine(nameof(spawnBarrelTracker));
            stopHappyHour();
            _game.OnRoundFinished(_characters[0].PlayerID);
        }

        int _roundCounter;
        #endregion
    }
}
