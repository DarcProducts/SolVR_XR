using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] Vector3 zoomInDirection;
    [SerializeField] Vector3 zoomOutDirection;
    [SerializeField] FloatVariable zoomSpeed;

    public void ZoomPanelIn()
    {
        transform.localPosition += zoomInDirection * zoomSpeed.Value * Time.fixedDeltaTime;
    }
    public void ZoomPanelOut()
    {
        transform.localPosition += zoomOutDirection * zoomSpeed.Value * Time.fixedDeltaTime;
    }
}
