using System;

namespace IngameStateMachine
{
    public interface IState
    {
        
        void OnEnter();
        void OnExit();
    }
}