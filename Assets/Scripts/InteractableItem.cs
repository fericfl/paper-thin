using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractableItem : InteractableObject
    {
        [field: SerializeField]
        public Item Item { get; private set; }
        
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = Item.Icon;
        }

        public override void Interact()
        {
            Inventory.Instance.AddItem(Item);
            Destroy(gameObject);
        }
    }
}