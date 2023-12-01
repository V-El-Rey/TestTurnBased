using System;
using System.Collections.Generic;
using Base;
using Unity.VisualScripting;
using UnityEngine;

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
        var gridModel = new GridModel(m_mainConfigurationData.GridConfiguration);
        var inputModel = new InputModel(m_mainConfigurationData.GridConfiguration.gridHitLayerMask);
        var unitSelectionModel = new UnitSelectionModel();
        var pathfindingModel = new PathfindingModel();
        var deepCopyArmyOne = new List<PlayerUnitModel>();
        m_mainConfigurationData.ArmyConfiguration.playerOneArmy.ForEach((item) => 
            {
                PlayerUnitModel newItem = (PlayerUnitModel)(ICloneable)item.Clone();
                deepCopyArmyOne.Add(newItem);
            });
        var deepCopyArmyTwo = new List<PlayerUnitModel>();
        m_mainConfigurationData.ArmyConfiguration.playerTwoArmy.ForEach((item) => 
            {
                PlayerUnitModel newItem = (PlayerUnitModel)(ICloneable)item.Clone();
                deepCopyArmyTwo.Add(newItem);
            });
        
        var playerOneArmyModel = new PlayerArmyModel(deepCopyArmyOne);
        var playerTwoArmyModel = new PlayerArmyModel(deepCopyArmyTwo);
        RegisterModel(unitSelectionModel);
        RegisterModel(pathfindingModel);
        RegisterModel(playerOneArmyModel);
        RegisterModel(playerTwoArmyModel);
        RegisterModel(gridModel);
        RegisterModel(inputModel);
        

        InitializeSubStateManager(playerOneArmyModel, playerTwoArmyModel, inputModel, gridModel, unitSelectionModel, pathfindingModel);

        ControllersManager.AddController(new PassTurnUIController(LocalConstants.PASS_TURN_UI, m_uiRoot, m_combatStateChangeModel));
        ControllersManager.AddController(new MouseInputController(inputModel));
        ControllersManager.AddController(new MouseRaycastFromCameraController(inputModel, m_cameraTransform));
        ControllersManager.AddController(new GridSystemInitializeController(gridModel));
        ControllersManager.AddController(new GridSystemDrawCellsController(gridModel, m_environmentRoot));
        ControllersManager.AddController(new GridCellHighlightController(inputModel, gridModel, unitSelectionModel, m_mainConfigurationData.GridConfiguration));
        ControllersManager.AddController(new GridPathfindingSystemController(gridModel, inputModel, pathfindingModel));
        ControllersManager.AddController(new SpawnArmiesController(gridModel, playerOneArmyModel, playerTwoArmyModel));

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

    private void InitializeSubStateManager(IPlayerArmyModel playerOneArmy, IPlayerArmyModel playerTwoArmy, 
        IInputModel inputModel, IGridModel gridModel, IUnitSelectionModel unitSelectionModel, IPathfindingModel pathfindingModel)
    {
        m_combatStateChangeModel = new StateChangeModel<CombatState>(CombatState.PlayerOne);
        m_combatStateManager = new StateManager<CombatState>(m_combatStateChangeModel);
        m_combatStateManager.States[CombatState.PlayerOne] = new PlayerTurnState(CombatState.PlayerOne, m_combatStateChangeModel, playerOneArmy, playerTwoArmy,
            inputModel, gridModel, unitSelectionModel, pathfindingModel, m_mainConfigurationData.ArmyConfiguration.playerOneArmy);
        m_combatStateManager.States[CombatState.PlayerTwo] = new PlayerTurnState(CombatState.PlayerTwo, m_combatStateChangeModel, playerTwoArmy, playerOneArmy,
            inputModel, gridModel, unitSelectionModel, pathfindingModel, m_mainConfigurationData.ArmyConfiguration.playerTwoArmy);
        m_combatStateManager.CurrentState = m_combatStateManager.States[CombatState.PlayerOne];
        m_combatStateManager.InitStateManager();
    }
}
