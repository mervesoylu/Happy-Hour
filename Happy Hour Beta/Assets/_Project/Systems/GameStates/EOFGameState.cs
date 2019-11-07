using System;
using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class EOFGameState : GameState
    {
        Game _game;
        GameState _readyUpState;
        EndgameMenuController _endgameMenu;

        public EOFGameState(Game game, GameState readyUpState, EndgameMenuController endgameMenu)
        {
            _game = game;
            _readyUpState = readyUpState;
            _endgameMenu = endgameMenu;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Show endgame menu!");
            _endgameMenu.Show(_game.Winner);
        }

        public override void OnStateUpdate()
        {
            if (XCI.GetButtonDown(XboxButton.A))
                _game.ChangeState(_readyUpState);
        }

        public override void OnStateExit()
        {
            Debug.Log("Hide endgame menu!");
            _endgameMenu.Hide();
        }
    }
}