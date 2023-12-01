using UnityEngine;

public class MouseInputController : IBaseController, IUpdateController
{
    private IInputModel m_inputModel;

    public MouseInputController(IInputModel inputModel)
    {
        m_inputModel = inputModel;
    }
    public void OnUpdateExecute()
    {
        m_inputModel.mousePosition = Input.mousePosition;
        if(Input.GetMouseButtonDown(0))
        {
            m_inputModel.onMouseClicked?.Invoke();
        }
    }
}
