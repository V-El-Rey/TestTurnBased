using UnityEngine.UIElements;

public class MainMenuView : UIView
{
    public Button startGame;
    public Button quitGame;
    public override void OnEnable()
    {
        base.OnEnable();
        startGame = root.Q<Button>(LocalConstants.UIBUTTON_START);
        quitGame = root.Q<Button>(LocalConstants.UIBUTTON_QUIT);

        //TODO: Локализации
        startGame.text = LocalConstants.UIBUTTON_START;
        quitGame.text = LocalConstants.UIBUTTON_QUIT;
    }
}
