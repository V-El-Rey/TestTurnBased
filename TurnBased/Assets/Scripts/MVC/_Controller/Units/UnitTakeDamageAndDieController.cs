using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitTakeDamageAndDieController : IBaseController, IEnterController, IExitController
{
    private IPlayerArmyModel m_enemyArmyModel;

    public UnitTakeDamageAndDieController(IPlayerArmyModel enemyArmyModel)
    {
        m_enemyArmyModel = enemyArmyModel;
    }
    public void OnEnterExecute()
    {
        m_enemyArmyModel.onTakeDamage += ProcessDamage;
    }

    public void OnExitExecute()
    {
        m_enemyArmyModel.onTakeDamage -= ProcessDamage;
    }
    private void ProcessDamage(PlayerUnitModel model, int amount, bool diagonal)
    {
        m_enemyArmyModel.units.First(e => e.UnitName == model.UnitName).Health -= amount;
    }
}
