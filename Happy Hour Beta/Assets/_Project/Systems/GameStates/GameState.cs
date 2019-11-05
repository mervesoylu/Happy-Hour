using System;

namespace Project
{
    public abstract class GameState
    {
        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}