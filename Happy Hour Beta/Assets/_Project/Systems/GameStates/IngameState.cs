using System;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class IngameState : GameState
    {
        Game _game;
        List<Player> _players;
        ScoreMoniter _scoreMonitor;

        public IngameState(Game game, List<Player> players, ScoreMoniter scoreMonitor)
        {
            _game = game;
            _players = players;
            _scoreMonitor = scoreMonitor;
        }

        public override void OnStateEnter()
        {
            _scoreMonitor.Display(_players);
            _game.BeginRound();
        }

        public override void OnStateUpdate()
        {
            if (XCI.GetButtonDown(XboxButton.Start))
                _game.PauseGame();
        }

        public override void OnStateExit()
        {
            _scoreMonitor.Hide();
        }
    }
}