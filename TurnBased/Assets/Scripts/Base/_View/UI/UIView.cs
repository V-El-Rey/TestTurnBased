using UnityEngine.UIElements;

public class UIView : BaseView
{
    public UIDocument uiDocument;
    protected VisualElement root;

    public virtual void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
}
