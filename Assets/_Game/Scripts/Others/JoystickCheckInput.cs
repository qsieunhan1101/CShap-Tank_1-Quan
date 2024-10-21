using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickCheckInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Joystick joystick;
    private bool isTouching = false;
    private Vector3 direction = Vector3.zero;


    public bool IsTouching => isTouching;
    public Vector3 Direction => direction;
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isTouching == true)
        {
            direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
    }
}
