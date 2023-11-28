using Base;

public class GameState : ControllersDependentState<MainGameState>
{
    private StateManager<CombatState> m_combatStateManager;
    private MainConfigurationData m_mainConfigurationData;
    private IGridModel m_gridModel;
    public GameState(MainGameState key, StateManager<MainGameState> stateManager, MainConfigurationData mainConfiguration) : base(key, stateManager)
    {
        m_combatStateManager = new StateManager<CombatState>();
        m_mainConfigurationData = mainConfiguration;

        
    }

    public override void EnterState()
    {
        //создание и регистрация моделей для этого стейта
        m_gridModel = new GridModel(m_mainConfigurationData.GridConfiguration);
        RegisterModel(m_gridModel);
        
        //добавление контроллеров в менеджер
        ControllersManager.AddController(new GridSystemInitializeController(m_gridModel));

        //инициализация сетки и окружения
        //инициализация пачек врагов
        //закидывание их в новую сабстейт машину
        base.EnterState();
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
