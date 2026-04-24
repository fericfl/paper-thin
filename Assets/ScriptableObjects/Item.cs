using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public String Name { get; set; }
    [field: SerializeField]
    public Sprite Icon { get; set; }
    [field: SerializeField]
    public bool Selectable { get; set; }
}
