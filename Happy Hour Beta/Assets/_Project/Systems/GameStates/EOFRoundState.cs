using System;
using System.Collections.Generic;
using XboxCtrlrInput;

namespace Project
{
    public class EOFRoundState : GameState
    {
        Game _game;
        GameState _ingameState;
        BoardController _boardController;
        List<Player> _players;

        public EOFRoundState(Game game, GameState ingameState, List<Player> players, BoardController boardController)
        {
            _game = game;
            _ingameState = ingameState;
            _players = players;
            _boardController = boardController;
        }

        public override void OnStateEnter()
        {
            _boardController.Show(_players);
        }

        public override void OnStateUpdate()
        {
            if (XCI.GetButtonDown(XboxButton.A))
                _game.ChangeState(_ingameState);
        }

        public override void OnStateExit()
        {
            _boardController.Hide();
        }
    }
}