using System;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class ReadyUpState : GameState
    {
        Game _game;
        GameState _ingameState;
        ReadyMenuController _readyUpMenu;
        HashSet<int> _readiedControllers;

        public ReadyUpState(Game game, GameState ingameState, ReadyMenuController readyUpMenu, HashSet<int> readiedControllers)
        {
            _game = game;
            _ingameState = ingameState;
            _readyUpMenu = readyUpMenu;
            _readiedControllers = readiedControllers;
        }

        public override void OnStateEnter()
        {
            _game.ResetRoundController();
            _game.SetupPlayers();
            _readiedControllers.Clear();
            _readyUpMenu.ResetUI();
            _readyUpMenu.Show();
        }

        public override void OnStateUpdate()
        {
            // Waiting for players to ready up.
            for (int i = 1; i <= 4; i++)
            {
                if (XCI.GetButtonDown(XboxButton.A, (XboxController)i))
                {
                    if (!_readiedControllers.Contains(i))
                    {
                        _readiedControllers.Add(i);
                        _readyUpMenu.Ready(i);

                        if (_readiedControllers.Count == 4)
                            _game.ChangeState(_ingameState);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
                _game.ChangeState(_ingameState);
        }

        public override void OnStateExit()
        {
            _readyUpMenu.Hide();
        }
    }
}