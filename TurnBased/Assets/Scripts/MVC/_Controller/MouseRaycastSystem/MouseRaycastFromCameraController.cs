using UnityEngine;

public class MouseRaycastFromCameraController : IBaseController, IUpdateController
{
    private IInputModel m_inputModel;
    private Transform m_cameraTransform;

    public MouseRaycastFromCameraController(IInputModel inputModel, Transform cameraTransform)
    {
        m_inputModel = inputModel;
        m_cameraTransform = cameraTransform;
    }

    public void OnUpdateExecute()
    {
        //Draw debug ray
        var mousePos = m_inputModel.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(m_cameraTransform.position, mousePos - m_cameraTransform.position, Color.red);

        Ray ray = Camera.main.ScreenPointToRay(m_inputModel.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, m_inputModel.gridLayerMask))
        {
            m_inputModel.gridCellHitCoordinates = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.z);
        } 
        else
        {
            m_inputModel.gridCellHitCoordinates = new Vector2(-1.0f, -1.0f);

        }
    }
}
