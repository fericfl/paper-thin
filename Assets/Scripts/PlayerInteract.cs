using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerInteract : MonoBehaviour
    {
        public Vector2 InteractionCircleCenter { get; private set; }
        public float InteractionCircleRadius { get; private set; }
        
        public void OnPickup(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }
            
            var colliders = Physics.OverlapSphere(InteractionCircleCenter, InteractionCircleRadius, LayerMask.GetMask("Interactable"));
            var interactables = colliders.Select(GetInteractable).Where(interactable => interactable).ToList();
            interactables.ForEach(interactable => interactable.Interact());
        }
        
        private InteractableObject GetInteractable(Collider collider)
        {
            var currentGameObject = collider.gameObject;
            while (currentGameObject)
            {
                if (currentGameObject.TryGetComponent(out InteractableObject interactableObject))
                {
                    return interactableObject;
                }
                
                if (currentGameObject.transform.parent)
                {
                    currentGameObject = currentGameObject.transform.parent.gameObject;
                }
                else
                {
                    currentGameObject = null;
                }
            }
            
            return null;
        }
    }
}