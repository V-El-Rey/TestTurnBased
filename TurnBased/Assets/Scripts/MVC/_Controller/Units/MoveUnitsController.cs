using UnityEngine;

public class MoveUnitsController : IBaseController, IEnterController, IExitController, IUpdateController
{
    private IUnitSelectionModel m_unitSelectionModel;
    private IInputModel m_inputModel;
    private IGridModel m_gridModel;
    private IPathfindingModel m_pathfindingModel;
    private IPlayerArmyModel m_playerArmyModel;

    private int pathLength;
    private int pathNodeIndex = 0;
    private float t = 0.0f;

    private float lerpTime = 4.5f;

    private Vector2 m_startPos;
    private Vector2 m_endPos;

    public MoveUnitsController(IInputModel inputModel, IGridModel gridModel, IUnitSelectionModel unitSelectionModel,
            IPathfindingModel pathfindingModel, IPlayerArmyModel playerArmyModel)
    {
        m_inputModel = inputModel;
        m_gridModel = gridModel;
        m_unitSelectionModel = unitSelectionModel;
        m_pathfindingModel = pathfindingModel;
        m_playerArmyModel = playerArmyModel;
    }
    public void OnEnterExecute()
    {
        m_startPos = new Vector2(-1, -1);
        m_endPos = new Vector2(-1, -1);
        m_inputModel.onMouseClicked += MoveSelectedUnit;
    }
    public void OnExitExecute()
    {
        m_inputModel.onMouseClicked -= MoveSelectedUnit;
    }

    public void OnUpdateExecute()
    {
        if (pathNodeIndex <= pathLength)
        {
            MoveUnitAlongPath();
        }
    }

    private void MoveUnitAlongPath()
    {
        if (m_unitSelectionModel.selectedUnitModel != null && m_unitSelectionModel.selectedUnitModel.actionPoints > 0 && 
                m_pathfindingModel.Path != null && pathLength > 0)
        {
            var nextCell =
                m_gridModel.gridView[m_pathfindingModel.Path[pathNodeIndex].x, m_pathfindingModel.Path[pathNodeIndex].y];
            m_unitSelectionModel.onCellSelectionChangeRequested?.Invoke(nextCell);

            m_unitSelectionModel.selectedUnit.transform.position = Vector3.Lerp(
                m_unitSelectionModel.selectedUnit.transform.position,
                nextCell.unitSpawnPoint.transform.position,
                lerpTime * Time.deltaTime);

            m_unitSelectionModel.selectedUnit.transform.LookAt(nextCell.unitSpawnPoint.transform.position, Vector3.up);
            m_unitSelectionModel.selectedUnit.healthBar.LookAt(Camera.main.transform.position);

            t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

            if (t > 0.9f)
            {
                t = 0f;
                pathNodeIndex++;
                m_unitSelectionModel.selectedUnitModel.actionPoints--;
                if(m_unitSelectionModel.selectedUnitModel.actionPoints == 0)
                {
                    m_pathfindingModel.Path.Clear();
                }
                if (pathNodeIndex >= pathLength)
                {
                    m_pathfindingModel.Path.Clear();
                    if (m_endPos.x != -1 && m_startPos.x != -1)
                    {
                        m_gridModel.grid[(int)m_startPos.x, (int)m_startPos.y].unit = null;
                        m_gridModel.grid[(int)m_startPos.x, (int)m_startPos.y].playerUnitModel = null;
                        m_gridModel.grid[(int)m_startPos.x, (int)m_startPos.y].isOccupied = false;

                        m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].playerUnitModel = m_unitSelectionModel.selectedUnitModel;
                        m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].unit = m_unitSelectionModel.selectedUnit;
                        m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].isOccupied = true;
                    }
                    pathLength = 0;
                    m_pathfindingModel.onPathEnded?.Invoke();
                    m_pathfindingModel.onPathEnded = null;
                }
            }
        }
    }

    private void MoveSelectedUnit()
    {
        m_endPos.x = m_inputModel.gridCellHitCoordinates.x;
        m_endPos.y = m_inputModel.gridCellHitCoordinates.y;
        if (m_endPos.x != -1 && m_endPos.y != -1)
        {
            if (m_unitSelectionModel.selectedCell != null && m_unitSelectionModel.selectedUnit != null)
            {
                m_startPos.x = m_unitSelectionModel.selectedCell.transform.position.x;
                m_startPos.y = m_unitSelectionModel.selectedCell.transform.position.z;
                if (!(m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].unit != null && m_playerArmyModel.unitViews.Contains(m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].unit)))
                {
                    pathNodeIndex = 0;
                    m_pathfindingModel.onPathRequested?.Invoke((int)m_startPos.x, (int)m_startPos.y, (int)m_endPos.x, (int)m_endPos.y);
                    if (m_pathfindingModel.Path.Count > 0)
                    {
                        m_endPos.x = m_pathfindingModel.Path[m_pathfindingModel.Path.Count - 1].x;
                        m_endPos.y = m_pathfindingModel.Path[m_pathfindingModel.Path.Count - 1].y;

                        if(m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].unit != null)
                        {
                            m_unitSelectionModel.unitToAttackModel = m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].playerUnitModel;
                            m_unitSelectionModel.unitToAttack = m_gridModel.grid[(int)m_endPos.x, (int)m_endPos.y].unit;
                        }
                    }
                    pathLength = m_pathfindingModel.Path.Count;
                }
            }
        }
    }
}
