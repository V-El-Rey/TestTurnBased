using Base;

public class GameState : ControllersDependentState<MainGameState>
{
    private StateManager<CombatState> m_combatStateManager;
    public GameState(MainGameState key, StateManager<MainGameState> stateManager) : base(key, stateManager)
    {
        m_combatStateManager = new StateManager<CombatState>();
    }

    public override void EnterState()
    {
        base.EnterState();
        //инициализация сетки и окружения
        //инициализация пачек врагов
        //закидывание их в новую сабстейт машину
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        m_combatStateManager.OnUpdateExecute();
    }
}
