using UnityEngine;

public class MousePositionInputController : IBaseController, IUpdateController
{
    private IInputModel m_inputModel;

    public MousePositionInputController(IInputModel inputModel)
    {
        m_inputModel = inputModel;
    }
    public void OnUpdateExecute()
    {
        m_inputModel.mousePosition = Input.mousePosition;
    }
}
