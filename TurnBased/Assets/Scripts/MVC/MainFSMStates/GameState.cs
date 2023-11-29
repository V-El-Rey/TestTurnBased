using Base;
using UnityEngine;

public class GameState : ControllersDependentState<MainGameState>
{
    private StateManager<CombatState> m_combatStateManager;
    private IStateChangeModel<CombatState> m_combatStateChangeModel;
    private MainConfigurationData m_mainConfigurationData;
    private Transform m_environmentRoot;
    private Transform m_cameraTransform;
    public GameState(MainGameState key, IStateChangeModel<MainGameState> stateChangeModel, MainConfigurationData mainConfiguration, 
        SceneStaticData sceneStaticData) : base(key, stateChangeModel)
    {
        m_combatStateChangeModel = new StateChangeModel<CombatState>();
        m_combatStateManager = new StateManager<CombatState>(m_combatStateChangeModel);
        m_mainConfigurationData = mainConfiguration;
        m_environmentRoot = sceneStaticData.EnvironmentRoot;
        m_cameraTransform = sceneStaticData.CameraTransform;
    }

    public override void EnterState()
    {
        //создание и регистрация моделей для этого стейта
        var gridModel = new GridModel(m_mainConfigurationData.GridConfiguration);
        var inputModel = new InputModel();
        RegisterModel(gridModel);
        RegisterModel(inputModel);
        
        //добавление контроллеров в менеджер
        ControllersManager.AddController(new MousePositionInputController(inputModel));
        ControllersManager.AddController(new MouseRaycastFromCameraController(inputModel, m_cameraTransform));
        ControllersManager.AddController(new GridSystemInitializeController(gridModel));
        ControllersManager.AddController(new GridSystemDrawCellsController(gridModel, m_environmentRoot));
        ControllersManager.AddController(new GridCellHighlightController(inputModel, gridModel, m_mainConfigurationData.GridConfiguration));

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
        //m_combatStateManager.OnUpdateExecute();
    }
}
