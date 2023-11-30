using System;

public class StateChangeModel<TState> : IStateChangeModel<TState> where TState : Enum
{
    public Action<TState> onStateChangeRequested { get; set;  }
    public TState CurrentStateKey { get; set; }
    public StateChangeModel(TState initialState)
    {
        CurrentStateKey = initialState;
    }

    public void ClearModel()
    {
        onStateChangeRequested = null;
    }
}
