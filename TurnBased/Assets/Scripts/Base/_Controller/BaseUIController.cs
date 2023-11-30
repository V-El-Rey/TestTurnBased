using UnityEngine;

public class BaseUIController<TView> : IBaseController, IEnterController, IExitController where TView : UIView
{
    private GameObject m_uiPrefab;
    protected UIView m_uiView;
    private Transform m_uiRoot;

    public BaseUIController(string screenId, Transform uiRoot)
    {
        m_uiRoot = uiRoot;
        m_uiPrefab = Resources.Load<GameObject>(screenId);
    }
    public virtual void OnEnterExecute()
    {
        var obj = GameObject.Instantiate(m_uiPrefab, m_uiRoot);
        m_uiView = obj.GetComponent<TView>();
    }

    public virtual void OnExitExecute()
    {
        GameObject.Destroy(m_uiView.gameObject);
    }
}
