using System;
using Base;
using System.Collections.Generic;

public class ControllersDependentState<TState> : BaseState<TState>, IBaseStateLoop where TState : Enum
{
    private List<IStateModel> m_stateModels;
    public IStateChangeModel<TState> StateChangeModel;
    private CombatState key;

    public ControllersManager ControllersManager { get; } 
    public ControllersDependentState(TState key, IStateChangeModel<TState> stateChangeModel) : base(key)
    {
        StateChangeModel = stateChangeModel;
        ControllersManager = new ControllersManager();
        m_stateModels = new List<IStateModel>();
    }

    public virtual void EnterState()
    {
        ControllersManager.OnEnterControllersExecute();
    }

    public virtual void UpdateState()
    {
        ControllersManager.OnUpdateControllersExecute();
    }

    public virtual void ExitState()
    {
        ControllersManager.OnExitControllersExecute();
        ClearStateModels();
    }

    public void RegisterModel(IStateModel model)
    {
        if(!m_stateModels.Contains(model))
        {
            m_stateModels.Add(model);        
        }
    }

    private void ClearStateModels()
    {
        foreach(var model in m_stateModels)
        {
            model.ClearModel();
        }
        m_stateModels.Clear();
    }
}
