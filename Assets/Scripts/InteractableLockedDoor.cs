using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableLockedDoor : InteractableDoor
    {
        public bool IsLocked  { get; private set; }
        public Item RequiredItem { get; private set; }
        
        protected override void Start()
        {
            base.Start();
            
            IsLocked = RequiredItem;
        }

        public override void Interact()
        {
            if (IsLocked)
            {
                if (!Inventory.Instance.SelectedItem != RequiredItem)
                {
                    return;
                }

                IsLocked = false;
                Inventory.Instance.RemoveItem(RequiredItem);
            }
            
            base.Interact();
        }
    }
}