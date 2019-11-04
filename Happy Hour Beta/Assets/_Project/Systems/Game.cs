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
        [Inject] ScoreMoniter _scoreMoniter;
        [Inject] Round _round;
        [Inject] SoundManager _soundManager;
        [Inject] ReadyMenuController _readyMenu;
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
            _scoreMoniter.Hide();

            _currentGameState = GameState.EOF_ROUND;

            //check for game over condition
            Player winner = _players.FirstOrDefault(p => p.Score == 3);

            if (winner != null)
            {
                endGame();
            }
        }
        #endregion

        #region ------------------------------details
        public void Initialize()
        {
            _currentGameState = GameState.START_OF_GAME;

            _scoreMoniter.Hide();
            _readyMenu.Show();

            setupPlayers();
            _isFirstRound = true;
        }

        public void Tick()
        {
            // Awaiting Input
            // Ready
            // Ingame
            // EOF Round
            // EOF Game

            switch (_currentGameState)
            {
                case GameState.START_OF_GAME:
                    for (int i = 1; i <= 4; i++)
                    {
                        if (XCI.GetButtonDown(XboxButton.A, (XboxController)i))
                        {
                            if (!_readyControllers.Contains(i))
                            {
                                _readyControllers.Add(i);
                                _readyMenu.Ready(i);

                                //if (_readyControllers.Count == 4)
                                //    _currentGameState = GameState.READY;
                            }

                            //temp
                            _currentGameState = GameState.READY;
                        }
                    }
                    break;

                case GameState.READY:
                    if (XCI.GetButtonDown(XboxButton.A, XboxController.All))
                    {
                        _readyMenu.Hide();
                        beginRound();
                        _currentGameState = GameState.IN_GAME;
                    }
                    break;

                case GameState.IN_GAME:
                    break;

                case GameState.EOF_ROUND:
                    if (XCI.GetButtonDown(XboxButton.A, XboxController.All))
                    {
                        if (_isGameEnded)
                        {
                            setupPlayers();

                            _readyControllers.Clear();
                            _readyMenu.ResetUI();
                            _readyMenu.Show();

                            _currentGameState = GameState.START_OF_GAME;
                            _isGameEnded = false;
                        }
                        else
                            beginRound();
                    }
                    break;

                default:
                    break;
            }
        }
        bool _isRoundBegan;
        bool _isGameEnded;
        HashSet<int> _readyControllers = new HashSet<int>();
        GameState _currentGameState;

        /// <summary>
        /// It assigns character models to players randomly and starts the first round.
        /// </summary>
        void setupPlayers()
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
            _scoreMoniter.Display(_players);
        }
        bool _isFirstRound;

        void endGame()
        {
            _isGameEnded = true;
            _isFirstRound = true;
        }
        #endregion
    }
}

public enum GameState { START_OF_GAME, READY, IN_GAME, EOF_ROUND }