using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerInteract : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2 InteractionCircleCenter { get; private set; }
        [field: SerializeField]
        public float InteractionCircleRadius { get; private set; }

        public void OnPickup(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }
            
            Debug.Log("Interacting");
            var colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + InteractionCircleCenter, InteractionCircleRadius, LayerMask.GetMask("Interactable", "Auto Pickupable"));
            var interactables = colliders.Select(GetInteractable).Where(interactable => interactable).ToList();
            interactables.ForEach(interactable => interactable.Interact());
        }
        
        private InteractableObject GetInteractable(Collider2D collider)
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
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(PlayerInteract))]
    class PlayerInteractEditor : Editor
    {
        private void OnSceneGUI()
        {
            var playerInteract = (PlayerInteract)target;
            var position = playerInteract.transform.position;
            var center = position + (Vector3)playerInteract.InteractionCircleCenter;
            
            Handles.color = Color.green;
            Handles.DrawWireDisc(center, Vector3.forward, playerInteract.InteractionCircleRadius);
        }
    }
    #endif
}