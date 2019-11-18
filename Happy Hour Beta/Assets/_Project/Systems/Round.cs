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
        [Inject] PostProcessVolumeController _postProcessVolumeController;
        List<CharacterController> _characters;
        [SerializeField] Bar _bar;
        [SerializeField] TextMeshProUGUI _happyHourTextUI;
        [SerializeField] GameObject _confetti;
        #endregion

        #region ------------------------------interface
        public void Setup(List<CharacterController> characters)
        {
            _characters = characters;

            _characterInputs.Clear();
            _characters.ForEach(ch => _characterInputs.Add(ch.GetComponent<CharacterInput>()));

            _roundCounter = 0;
        }

        public void Begin()
        {
            _roundCounter++;
            _postProcessVolumeController.OnRoundBegan();

            if (_roundCounter == 1)
                _spawnPoints.Shuffle();

            for (int i = 0; i < _characters.Count; i++)
                _characters[i].transform.position = _spawnPoints[i].position;

            _characters.ForEach(ch => ch.Restart());
            _characterInputs.ForEach(ci => ci.OnRoundBegan());

            StartCoroutine(nameof(spawnBarrelTracker));
        }

        /// <summary>
        /// It handles player death.
        /// </summary>
        /// <param name="playerID"> It is the Player's Controller attribute</param>
        public void OnPlayerDied(int playerID)
        {
            int alivePlayersCount = _characters.Where(ch => ch.gameObject.activeSelf).Count();

            if (alivePlayersCount <= 1)
                finishRound();
        }

        #endregion

        #region ------------------------------Unity messages
        void Start()
        {
            _happyHourTextUI.gameObject.SetActive(false);
            _confetti.gameObject.SetActive(false);
        }
        #endregion

        #region ------------------------------details
        List<CharacterInput> _characterInputs = new List<CharacterInput>();

        IEnumerator spawnBarrelTracker()
        {
            _spawnedBarrelsCounter = 0;

            while (true)
            {
                yield return new WaitForSeconds(_barrelSpawnInterval + _bar.LaunchDuration);
                if (_spawnedBarrelsCounter == _numberOfBarrelsToInitiateHappuHour)
                {
                    _spawnedBarrelsCounter = 0;
                    runHappyHour();
                    yield return new WaitForSeconds(_happyHourDuration);
                    stopHappyHour();
                }
                spawnBarrel();
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
            _confetti.gameObject.SetActive(true);
            _postProcessVolumeController.OnHappyHourRan();
            _bar.OnHappyHourRan();

            foreach (var character in _characters)
            {
                character.OnHappyHourRan();
                character.GetComponent<CharacterInput>().OnHappyHourRan();
            }
        }

        void stopHappyHour()
        {
            _happyHourTextUI.gameObject.SetActive(false);
            _confetti.gameObject.SetActive(false);
            _postProcessVolumeController.OnHappyHourStopped();

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
            _characterInputs.ForEach(ci => ci.OnRoundEnded());
            _game.OnRoundFinished(_characters.First(ch => ch.gameObject.activeSelf).PlayerID);
            // _game.OnRoundFinished(_characters[0].PlayerID);
        }

        int _roundCounter;
        #endregion
    }
}
