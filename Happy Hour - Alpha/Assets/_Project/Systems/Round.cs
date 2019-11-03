using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] Bar _bar;
        List<CharacterController> _characters;
        List<CharacterInput> _characterInputs = new List<CharacterInput>();
        [SerializeField] TextMeshProUGUI _happyHourTextUI;
        #endregion

        #region ------------------------------interface
        public void Begin(List<CharacterController> characters, bool isFirstRound)
        {
            StartCoroutine(nameof(spawnBarrelTracker));

            _characters = characters;

            foreach (var character in _characters)
                _characterInputs.Add(character.GetComponent<CharacterInput>());

            foreach (var characterInput in _characterInputs)
                characterInput.OnroundBegan();

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
                character.OnHappyHourRan();

            foreach (var characterInput in _characterInputs)
                characterInput.OnHappyHourRan();
        }

        void stopHappyHour()
        {
            _happyHourTextUI.gameObject.SetActive(false);

           foreach (var character in _characters)
               character.OnHappyHourStopped();

           foreach (var characterInput in _characterInputs)
               characterInput.OnHappyHourStopped();
            
        }

        void finishRound()
        {
            StopCoroutine(nameof(spawnBarrelTracker));
            stopHappyHour();
            _game.OnRoundFinished(_characters[0].PlayerID);
            foreach (var characterInput in _characterInputs)
                characterInput.OnroundEnded();
        }
        #endregion
    }
}
