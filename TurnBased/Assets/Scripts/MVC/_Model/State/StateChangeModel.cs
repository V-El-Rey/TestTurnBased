using System;

public class StateChangeModel<TState> : IStateChangeModel<TState> where TState : Enum
{
    public Action<TState> onStateChangeRequested { get; set;  }

    public void ClearModel()
    {
        onStateChangeRequested = null;
    }
}
