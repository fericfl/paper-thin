using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableDoor : InteractableObject
    {
        [field: SerializeField]
        public SpriteRenderer DoorSpriteRenderer { get; private set; }
        [field: SerializeField]
        public Sprite DoorClosedSprite { get; private set; }
        [field: SerializeField]
        public Sprite DoorOpenSprite { get; private set; }
        [field: SerializeField]
        public Collider2D DoorCollider { get; private set; }
        [field: SerializeField]
        public bool IsOpen { get; private set; }

        protected virtual void Start()
        {
            DoorSpriteRenderer.sprite = IsOpen ? DoorOpenSprite : DoorClosedSprite;
            DoorCollider.enabled = !IsOpen;
        }

        public override void Interact()
        {
            DoorSpriteRenderer.sprite = IsOpen ? DoorOpenSprite : DoorClosedSprite;
            DoorCollider.enabled = !IsOpen;
            
            IsOpen = !IsOpen;
        }
    }
}