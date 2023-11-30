using System;

public interface IStateChangeModel<TState> : IModel where TState : Enum
{
    TState CurrentStateKey { get; set; }
    Action<TState> onStateChangeRequested { get; set; }
}
