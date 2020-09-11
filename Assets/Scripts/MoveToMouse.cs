using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveToMouse : MonoBehaviour
{
    public float minimize = 0.03f;
    protected Camera mainCam;
    protected float z;
    protected Vector2 basePosition;


    protected virtual void Awake()
    {
        mainCam = Camera.main;
        var dist = transform.position - mainCam.transform.position;
        z = Distance(transform.position.z, mainCam.transform.position.z);
        basePosition = transform.position;

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled) return;
        Vector2 mousePos = context.ReadValue<Vector2>();
        MoveMouse(mousePos);
    }

    protected virtual void MoveMouse(Vector2 mousePos)
    {
        if (OutOfBounds()) return;
        // Debug.Log(("mousePos before" + mousePos));

        mousePos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, z));
        Vector2 newPos = new Vector2(CalculateX(mousePos.x), CalculateY(mousePos.y));
        // Debug.Log("mousePos" + mousePos + " \nnewPos" + newPos);

        transform.position = new Vector3(basePosition.x, basePosition.y, 0) + new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    private bool OutOfBounds()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height) return true;
        return false;
    }

    protected float CalculateX(float mouseX)
    {
        return mouseX * minimize * ((float)Screen.height / (float)Screen.width);
    }
    protected float CalculateY(float mouseY)
    {
        return mouseY * minimize * ((float)Screen.width / (float)Screen.height);
    }

    protected float Distance(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
}
