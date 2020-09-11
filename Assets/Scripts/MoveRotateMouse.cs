
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Rotate. -X is down. X is up. -Y is right; Y is left
/// </summary>
public class MoveRotateMouse : MoveToMouse
{
    public float minX = -2, maxX = 2, minY = -2, maxY = 2;

    protected override void Awake()
    {
        base.Awake();
        PlayerInput input = FindObjectOfType<PlayerInput>();
        if (input.notificationBehavior == PlayerNotifications.InvokeCSharpEvents)
        {
            input.onActionTriggered += TriggerPoint;
        }
    }

    private void TriggerPoint(InputAction.CallbackContext context)
    {
        if (context.canceled) return;
        if (context.action.name == "Point" && context.performed)
        {
            MoveMouse(context.ReadValue<Vector2>());
        }
    }

    protected override void MoveMouse(Vector2 mousePos)
    {
        base.MoveMouse(mousePos);
        Rotate();

    }

    private void Rotate()
    {
        Vector2 bottomLeft = mainCam.ScreenToWorldPoint(new Vector3(0, 0, z));
        Vector2 topRight = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
        float minPossibleX = CalculateX(bottomLeft.x), maxPossibleX = CalculateX(topRight.x);
        float minPossibleY = CalculateY(bottomLeft.y), maxPossibleY = CalculateY(topRight.y);


        float rotateX = Mathf.Lerp(minX, maxX, Distance(transform.position.x, minPossibleX) / Distance(maxPossibleX, minPossibleX));
        float rotateY = Mathf.Lerp(minY, maxY, Distance(transform.position.y, minPossibleY) / Distance(maxPossibleY, minPossibleY));

        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }
}
