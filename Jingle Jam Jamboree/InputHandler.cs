using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class InputHandler : MonoBehaviour, AxisState.IInputAxisProvider
{
    [HideInInspector] public InputAction Horizontal;
    [HideInInspector] public InputAction Vertical;

    // gives each player own input
    public float GetAxisValue(int pAxis)
    {
        switch (pAxis)
        {
            case 0: return Horizontal.ReadValue<Vector2>().x;
            case 1: return Horizontal.ReadValue<Vector2>().y;
            case 2: return Vertical.ReadValue<float>();
        }

        return 0;
    }
}
