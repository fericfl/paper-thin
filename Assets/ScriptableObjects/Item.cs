using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public String Name { get; set; }
    public Sprite Icon { get; set; }
    public bool Selectable { get; set; }
}
