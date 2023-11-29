using System;
using System.Collections.Generic;

namespace Base
{
    public class StateManager<EState> : IDisposable where EState : Enum
    {
        public Dictionary<EState, IBaseStateLoop> States = new Dictionary<EState, IBaseStateLoop>();
        public IBaseStateLoop CurrentState {get; set;}
        private IStateChangeModel<EState> m_stateChangeModel;
        private bool isStateTransitioning;

        public StateManager(IStateChangeModel<EState> stateChangeModel)
        {
            m_stateChangeModel = stateChangeModel;
        }

        public void InitStateManager()
        {
            m_stateChangeModel.onStateChangeRequested += SwitchState;
            CurrentState.EnterState();
        }

        public void OnUpdateExecute()
        {
            if(!isStateTransitioning)
            {
                CurrentState.UpdateState();
            }
        }

        public void SwitchState(EState nextStateKey)
        {
            isStateTransitioning = true;
            CurrentState.ExitState();
            CurrentState = States[nextStateKey];
            CurrentState.EnterState();
            isStateTransitioning = false;
        }

        public void Dispose()
        {
            m_stateChangeModel.ClearModel();
        }
    }
}
