using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField]
    private String Name { get; set; }
    [field: SerializeField]
    private Sprite Icon { get; set; }
    [field: SerializeField]
    private bool Selectable { get; set; }
}
