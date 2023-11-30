
using UnityEngine;

public class PlayerTurnState : ControllersDependentState<CombatState>
{
    private IStateChangeModel<CombatState> m_stateChangeModel;
    private PlayerArmyModel m_armyModel;
    public PlayerTurnState(CombatState key, IStateChangeModel<CombatState> stateChangeModel, IStateModel armyModel) : base(key, stateChangeModel)
    {
        m_stateChangeModel = stateChangeModel;
        m_armyModel = armyModel as PlayerArmyModel;
    }

    public override void EnterState()
    {
        Debug.Log($"{StateKey} turn");
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

}
