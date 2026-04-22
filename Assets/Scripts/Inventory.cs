using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            _instance ??= new Inventory();
            return _instance;
        }
    }

    public Item _selectedItem { get; private set; }
    private List<Item> _items;

    private Inventory()
    {
        _items = new List<Item>();
    }
    
    public void AddItem(Item item)
    {
        _items.Add(item);
        
        if (_selectedItem == null)
        {
            _selectedItem = item;
        }
    }

    public bool ContainsItem(Item item)
    {
        return _items.Contains(item);
    }

    public bool RemoveItem(Item item)
    {
        if (_selectedItem != item)
        {
            return _items.Remove(item);
        }
        
        var leftItem = GetLeftLoopItem();
        _selectedItem = leftItem == item ? null : leftItem;
        return _items.Remove(item);
    }

    public Item GetLeftLoopItem()
    {
        if (_items.Count < 2)
        {
            return _selectedItem;
        }
        
        var selectedItemIndex = _items.IndexOf(_selectedItem);
        return selectedItemIndex == 0 ? _items[^1] : _items[selectedItemIndex - 1];
    }

    public Item GetRightLoopItem()
    {
        if (_items.Count < 2)
        {
            return _selectedItem;
        }
        
        var selectedItemIndex = _items.IndexOf(_selectedItem);
        return selectedItemIndex == _items.Count - 1 ? _items[0] : _items[selectedItemIndex + 1];
    }
}