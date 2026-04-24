using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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

    public Item SelectedItem { get; private set; }
    public event Action<Item> OnSelectedItemChanged;
    public event Action OnItemsChanged;
    private readonly List<Item> _items;

    private Inventory()
    {
        _items = new List<Item>();
    }
    
    public void AddItem(Item item)
    {
        _items.Add(item);
        
        if (SelectedItem == null)
        {
            SelectedItem = item;
            OnSelectedItemChanged?.Invoke(SelectedItem);
        }
        
        OnItemsChanged?.Invoke();
    }

    public bool ContainsItem(Item item)
    {
        return _items.Contains(item);
    }

    public bool RemoveItem(Item item)
    {
        if (SelectedItem != item)
        {
            return TryRemoveItem(item);
        }
        
        var leftItem = GetLeftLoopItem();
        SelectedItem = leftItem == item ? null : leftItem;
        OnSelectedItemChanged?.Invoke(SelectedItem);
        
        return TryRemoveItem(item);
    }
    
    private bool TryRemoveItem(Item item)
    {
        var removed = _items.Remove(item);

        if (!removed)
        {
            return false;
        }
        
        OnItemsChanged?.Invoke();
        return true;
    }

    public Item GetLeftLoopItem()
    {
        if (_items.Count < 2)
        {
            return SelectedItem;
        }
        
        var selectedItemIndex = _items.IndexOf(SelectedItem);
        return selectedItemIndex == 0 ? _items[^1] : _items[selectedItemIndex - 1];
    }

    public Item GetRightLoopItem()
    {
        if (_items.Count < 2)
        {
            return SelectedItem;
        }
        
        var selectedItemIndex = _items.IndexOf(SelectedItem);
        return selectedItemIndex == _items.Count - 1 ? _items[0] : _items[selectedItemIndex + 1];
    }

    public void SelectLeftItem(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        SelectedItem = GetLeftLoopItem();
        OnSelectedItemChanged?.Invoke(SelectedItem);
    }

    public void SelectRightItem(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        
        SelectedItem = GetRightLoopItem();
        OnSelectedItemChanged?.Invoke(SelectedItem);
    }
}