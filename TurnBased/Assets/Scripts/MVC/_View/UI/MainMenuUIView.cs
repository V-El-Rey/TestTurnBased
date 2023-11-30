using UnityEngine.UIElements;

public class MainMenuView : UIView
{
    public Button StartGame;
    public Button QuitGame;
    public override void OnEnable()
    {
        base.OnEnable();
        StartGame = root.Q<Button>(LocalConstants.UIBUTTON_START);
        QuitGame = root.Q<Button>(LocalConstants.UIBUTTON_QUIT);

        //TODO: Локализации
        StartGame.text = LocalConstants.UIBUTTON_START;
        QuitGame.text = LocalConstants.UIBUTTON_QUIT;
    }
}
