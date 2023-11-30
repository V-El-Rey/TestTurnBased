using Base;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameState : ControllersDependentState<MainGameState>
{
    private StateManager<CombatState> m_combatStateManager;
    private IStateChangeModel<CombatState> m_combatStateChangeModel;
    private MainConfigurationData m_mainConfigurationData;
    private Transform m_environmentRoot;
    private Transform m_cameraTransform;
    private Transform m_uiRoot;
    public GameState(MainGameState key, IStateChangeModel<MainGameState> stateChangeModel, MainConfigurationData mainConfiguration, 
        SceneStaticData sceneStaticData) : base(key, stateChangeModel)
    {
        m_mainConfigurationData = mainConfiguration;
        m_environmentRoot = sceneStaticData.EnvironmentRoot;
        m_cameraTransform = sceneStaticData.CameraTransform;
        m_uiRoot = sceneStaticData.UIRoot;
    }

    public override void EnterState()
    {
        //создание и регистрация моделей для этого стейта
        var gridModel = new GridModel(m_mainConfigurationData.GridConfiguration);
        var inputModel = new InputModel(m_mainConfigurationData.GridConfiguration.gridHitLayerMask);
        var playerOneArmyModel = new PlayerArmyModel(m_mainConfigurationData.ArmyConfiguration.playerOneArmy);
        var playerTwoArmyModel = new PlayerArmyModel(m_mainConfigurationData.ArmyConfiguration.playerTwoArmy);
        RegisterModel(gridModel);
        RegisterModel(inputModel);
        
        //Инициализация сабстейт менеджера
        InitializeSubStateManager(playerOneArmyModel, playerTwoArmyModel);
        //добавление контроллеров в менеджер
        ControllersManager.AddController(new PassTurnUIController(LocalConstants.PASS_TURN_UI, m_uiRoot, m_combatStateChangeModel));
        ControllersManager.AddController(new MousePositionInputController(inputModel));
        ControllersManager.AddController(new MouseRaycastFromCameraController(inputModel, m_cameraTransform));
        ControllersManager.AddController(new GridSystemInitializeController(gridModel));
        ControllersManager.AddController(new GridSystemDrawCellsController(gridModel, m_environmentRoot));
        ControllersManager.AddController(new GridCellHighlightController(inputModel, gridModel, m_mainConfigurationData.GridConfiguration));
        ControllersManager.AddController(new GridPathfindingSystemController(gridModel, inputModel));

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

    private void InitializeSubStateManager(IStateModel playerOneArmy, IStateModel playerTwoArmy)
    {
        m_combatStateChangeModel = new StateChangeModel<CombatState>(CombatState.PlayerOne);
        m_combatStateManager = new StateManager<CombatState>(m_combatStateChangeModel);
        m_combatStateManager.States[CombatState.PlayerOne] = new PlayerTurnState(CombatState.PlayerOne, m_combatStateChangeModel, playerOneArmy);
        m_combatStateManager.States[CombatState.PlayerTwo] = new PlayerTurnState(CombatState.PlayerTwo, m_combatStateChangeModel, playerTwoArmy);
        m_combatStateManager.States[CombatState.Pause] = new PlayerTurnState(CombatState.Pause, m_combatStateChangeModel, playerOneArmy);
        m_combatStateManager.CurrentState = m_combatStateManager.States[CombatState.PlayerOne];
        m_combatStateManager.InitStateManager();
    }
}
