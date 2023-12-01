using UnityEngine;

public class SpawnArmiesController : IBaseController, IEnterController
{
    private IGridModel m_gridModel;
    private IPlayerArmyModel m_playerOneArmy;
    private IPlayerArmyModel m_playerTwoArmy;
    public SpawnArmiesController(IGridModel gridModel, IPlayerArmyModel playerOneArmy, IPlayerArmyModel playerTwoArmy)
    {
        m_gridModel = gridModel;
        m_playerOneArmy = playerOneArmy;
        m_playerTwoArmy = playerTwoArmy;
    }

    public void OnEnterExecute()
    {
        SpawnArmy(m_playerOneArmy, true);
        SpawnArmy(m_playerTwoArmy, false);
    }

    private void SpawnArmy(IPlayerArmyModel playerArmyModel, bool left)
    {
        if (playerArmyModel.units.Count > 0)
        {
            for (int i = 0; i < playerArmyModel.units.Count; i++)
            {
                (var randomX, var randomY) = FindRandomSpot(left);
                var unit = playerArmyModel.units[i];
                var cellToSpawn = m_gridModel.gridView[randomX, randomY];
                var gridNode = m_gridModel.grid[randomX, randomY];

                var go = GameObject.Instantiate(unit.prefab, cellToSpawn.unitSpawnPoint.transform.position, unit.prefab.transform.rotation);
                go.name = unit.UnitName;
                gridNode.unit = go.GetComponent<UnitView>();
                gridNode.playerUnitModel = playerArmyModel.units[i];
                playerArmyModel.unitViews.Add(gridNode.unit);
                gridNode.unit.healthBar.LookAt(Camera.main.transform.position);
                gridNode.isOccupied = true;
            }
        }
    }

    private (int, int) FindRandomSpot(bool left)
    {
        var randomX = left ? Random.Range(0, 2) : m_gridModel.width - Random.Range(1, 3);
        var randomY = 5 + Random.Range(-4, 4);
        if (m_gridModel.grid[randomX, randomY].isOccupied)
        {
            while (m_gridModel.grid[randomX, randomY].isOccupied)
            {
                (randomX, randomY) = FindRandomSpot(left);
                if (!m_gridModel.grid[randomX, randomY].isOccupied)
                {
                    m_gridModel.grid[randomX, randomY].isOccupied = true;
                    return (randomX, randomY);
                }
                else
                {
                    return (0, 0);
                }
            }
            return (0, 0);
        }
        else
        {
            return (randomX, randomY);
        }
    }
}
