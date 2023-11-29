using System;

public interface IStateChangeModel<TState> : IModel where TState : Enum
{
    Action<TState> onStateChangeRequested { get; set; }
}
