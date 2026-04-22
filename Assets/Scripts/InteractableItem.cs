using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableItem : InteractableObject
    {
        public Item Item { get; private set; }
        
        public override void Interact()
        {
            Inventory.Instance.AddItem(Item);
            Destroy(gameObject);
        }
    }
}