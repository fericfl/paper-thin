using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemSelection : MonoBehaviour
{
    public void SelectLeftItem(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        Inventory.Instance.SelectLeftItem(context);
    }
    
    public void SelectRightItem(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        Inventory.Instance.SelectRightItem(context);
    }
}
