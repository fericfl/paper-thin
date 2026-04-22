using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableDoor : InteractableObject
    {
        public SpriteRenderer DoorSpriteRenderer { get; private set; }
        public Sprite DoorClosedSprite { get; private set; }
        public Sprite DoorOpenSprite { get; private set; }
        public Collider2D DoorCollider { get; private set; }
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