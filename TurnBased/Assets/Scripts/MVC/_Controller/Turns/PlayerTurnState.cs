
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : ControllersDependentState<CombatState>
{
    private IStateChangeModel<CombatState> m_stateChangeModel;
    private IPlayerArmyModel m_armyModel;
    private IPlayerArmyModel m_enemyArmyModel;
    private List<PlayerUnitModel> m_armyConfiguration;
    private IInputModel m_inputModel;
    private IGridModel m_gridModel;
    private IUnitSelectionModel m_unitSelectionModel;
    private IPathfindingModel m_pathfindingModel;
    public PlayerTurnState(CombatState key, IStateChangeModel<CombatState> stateChangeModel, IPlayerArmyModel armyModel, IPlayerArmyModel enemyArmyModel, 
        IInputModel inputModel, IGridModel gridModel, IUnitSelectionModel unitSelectionModel, IPathfindingModel pathfindingModel, List<PlayerUnitModel> armyConfiguration) : base(key, stateChangeModel)
    {
        m_stateChangeModel = stateChangeModel;
        m_armyModel = armyModel;
        m_enemyArmyModel = enemyArmyModel;
        m_inputModel = inputModel;
        m_gridModel = gridModel;
        m_unitSelectionModel = unitSelectionModel;
        m_pathfindingModel = pathfindingModel;
        m_armyConfiguration = armyConfiguration;
    }

    public override void EnterState()
    {
        var pm = new PathfindingModel();
        ControllersManager.AddController(new ResetActionPointsController(m_armyModel, m_armyConfiguration));
        ControllersManager.AddController(new SelectUnitController(m_inputModel, m_armyModel, m_gridModel, m_unitSelectionModel));
        ControllersManager.AddController(new MoveUnitsController(m_inputModel, m_gridModel, m_unitSelectionModel, m_pathfindingModel, m_armyModel));
        ControllersManager.AddController(new UnitAttackController(m_pathfindingModel, m_gridModel, m_enemyArmyModel, m_unitSelectionModel));

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
