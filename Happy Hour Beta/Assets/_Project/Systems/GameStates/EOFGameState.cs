using System;
using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class EOFGameState : GameState
    {
        Game _game;
        GameState _readyUpState;


        public EOFGameState(Game game, GameState readyUpState)
        {
            _game = game;
            _readyUpState = readyUpState;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Show endgame menu!");
        }

        public override void OnStateUpdate()
        {
            if (XCI.GetButtonDown(XboxButton.A))
                _game.ChangeState(_readyUpState);
        }

        public override void OnStateExit()
        {
            Debug.Log("Hide endgame menu!");
        }
    }
}