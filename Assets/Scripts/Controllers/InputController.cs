using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Singleton
    public static InputController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [HideInInspector] public PointerEventData pointerEventData;

    [HideInInspector] public UnityEvent onPress = new UnityEvent();
    [HideInInspector] public UnityEvent onRelease = new UnityEvent();

    #region RaycastTouch
    public void OnPointerDown(PointerEventData _eventData)
    {
        pointerEventData = _eventData;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        pointerEventData = null;
    }

    public RaycastHit hit;
    public Vector2 rayOffset = new Vector2(0, 100);

    private void FixedUpdate()
    {
        if (pointerEventData == null) return;
        
        Ray ray;
        ray = CameraController.instance.cam.ScreenPointToRay(pointerEventData.position + rayOffset);

        Physics.Raycast(ray, out hit);
    }
    #endregion
}
