using UnityEngine;

public class PassTurnUIController : BaseUIController<PassTurnUIView>
{
    private IStateChangeModel<CombatState> m_stateChangeModel;
    public PassTurnUIController(string screenId, Transform uiRoot, IStateChangeModel<CombatState> stateChangeModel) : base(screenId, uiRoot)
    {
        m_stateChangeModel = stateChangeModel;
    }

    public override void OnEnterExecute()
    {
        base.OnEnterExecute();
        ((PassTurnUIView)m_uiView).PassTurn.clicked += ChangeTurnState;
    }

    public override void OnExitExecute()
    {
        ((PassTurnUIView)m_uiView).PassTurn.clicked -= ChangeTurnState;
        base.OnExitExecute();
    }

    private void ChangeTurnState()
    {
        if (m_stateChangeModel.CurrentStateKey == CombatState.PlayerOne)
        {
            m_stateChangeModel.onStateChangeRequested?.Invoke(CombatState.PlayerTwo);
        }
        else if (m_stateChangeModel.CurrentStateKey == CombatState.PlayerTwo)
        {
            m_stateChangeModel.onStateChangeRequested?.Invoke(CombatState.PlayerOne);
        }

    }
}
