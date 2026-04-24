using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBasedRotation : MonoBehaviour
{
    public void RotateLight(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        
        var input = context.ReadValue<Vector2>();
        var normalizedInput = input.normalized;
        if (normalizedInput == Vector2.zero)
        {
            return;
        }
        
        var angle = -Mathf.Atan2(normalizedInput.x, normalizedInput.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
