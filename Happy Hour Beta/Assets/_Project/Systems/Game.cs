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
        [Inject] Round _round;
        [Inject] List<Player> _players;
        [Inject] List<GameObject> _characters;
        [Inject] PauseMenuController _pauseController;
        [Inject] ReadyMenuController _readyUpMenu;
        [Inject] ScoreMoniter _scoreMoniter;
        [Inject] BoardController _board;
        [Inject] EndgameMenuController _endgameMenu;
        [Inject] SoundManager _soundManager;
        [Inject(Id = "numberOfRoundsPerGame")] int _numberOfRoundsPerGame;
        #endregion

        #region ------------------------------interface
        public void PauseGame()
        {
            _pauseController.PauseGame();
        }

        public void BeginRound()
        {
            _round.Begin();
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
            Player winner = _players.FirstOrDefault(p => p.Score == _numberOfRoundsPerGame);
            _winner = winner;

            if (winner != null)
                ChangeState(_eofGameState);
            else
                ChangeState(_eofRoundState);
        }

        public void ChangeState(GameState state)
        {
            if (_currentGameState != null)
            { _currentGameState.OnStateExit(); }

            _currentGameState = state;

            _currentGameState.OnStateEnter();
        }
        #endregion

        #region ------------------------------details
        public void Initialize()
        {
            _ingameState = new IngameState(this, _players, _scoreMoniter);
            _readyUpState = new ReadyUpState(this, _ingameState, _readyUpMenu, _readiedControllers);
            _eofRoundState = new EOFRoundState(this, _ingameState, _players, _board);
            _eofGameState = new EOFGameState(this, _readyUpState, _endgameMenu);

            ChangeState(_readyUpState);
        }
        GameState _currentGameState;
        GameState _readyUpState;
        GameState _ingameState;
        GameState _eofRoundState;
        GameState _eofGameState;

        public void Tick()
        {
            _currentGameState.OnStateUpdate();
        }

        public void Setup()
        {
            setupPlayers();
            _round.Setup(_characters.Select(c => c.GetComponent<CharacterController>()).ToList());
        }

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
        }

        HashSet<int> _readiedControllers = new HashSet<int>();
        Player _winner;
        public Player Winner { get {return _winner; } }
        #endregion
    }
}