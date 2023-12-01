using System;
using System.Linq;
using UnityEngine;

public class UnitAttackController : IBaseController, IEnterController, IExitController, IUpdateController
{
    private IPathfindingModel m_pathfindingModel;
    private IGridModel m_gridModel;
    private IPlayerArmyModel m_enemyArmyModel;
    private IUnitSelectionModel m_unitSelectionModel;

    private bool m_attacking;

    public UnitAttackController(IPathfindingModel pathfindingModel, IGridModel gridModel, IPlayerArmyModel enemyArmyModel, IUnitSelectionModel unitSelectionModel)
    {
        m_pathfindingModel = pathfindingModel;
        m_gridModel = gridModel;
        m_enemyArmyModel = enemyArmyModel;
        m_unitSelectionModel = unitSelectionModel;
    }
    public void OnEnterExecute()
    {
        m_pathfindingModel.onPathEnded += AttackIfPossible;
    }


    public void OnExitExecute()
    {
        m_pathfindingModel.onPathEnded -= AttackIfPossible;
    }

    public void OnUpdateExecute()
    {

    }

    private void AttackIfPossible()
    {
        if (m_unitSelectionModel.unitToAttack != null)
        {
            m_enemyArmyModel.onTakeDamage?.Invoke(m_unitSelectionModel.unitToAttackModel, m_unitSelectionModel.selectedUnitModel.Attack, false);
            Debug.Log($"Attack  {m_unitSelectionModel.unitToAttackModel.UnitName} with {m_unitSelectionModel.selectedUnitModel.Attack} damage. {m_unitSelectionModel.unitToAttackModel.Health} health left");
        }
    }
}
