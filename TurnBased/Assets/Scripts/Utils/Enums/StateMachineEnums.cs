    public enum MainGameState
    {
        MainMenu,
        Game
    }

    public enum CombatState
    {
        PlayerOne,
        PlayerTwo
    }

    public enum CommonCellEdges
    {
        None,
        Up,
        Right,
        Bottom,
        Left
    }

    public enum CellSelectionState
    {
        None,
        Highlighted,
        Selected,
        Attacked,
        Possible
    }
