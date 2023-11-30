using UnityEngine.UIElements;

public class PassTurnUIView : UIView
{
    public Button PassTurn;

    public override void OnEnable()
    {
        base.OnEnable();
        PassTurn = root.Q<Button>(LocalConstants.PASS_TURN_BUTTON);

        PassTurn.text = LocalConstants.PASS_TURN_BUTTON_TEXT;
    }
}
