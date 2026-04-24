using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemUIHandler : MonoBehaviour
{
    [field: SerializeField]
    public Image SelectedItemImage { get; private set; }
    [field: SerializeField]
    public Image LeftItemImage  { get; private set; }
    [field: SerializeField]
    public Image RightItemImage  { get; private set; }

    private void Start()
    {
        Inventory.Instance.OnSelectedItemChanged += UpdateUISelectedItemChanged;
        Inventory.Instance.OnItemsChanged += UpdateUIItemsChanged;
        
        UpdateUISelectedItemChanged(Inventory.Instance.SelectedItem);
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnSelectedItemChanged -= UpdateUISelectedItemChanged;
    }

    private void UpdateUISelectedItemChanged(Item item)
    {
        SelectedItemImage.sprite = item?.Icon;
        LeftItemImage.sprite = Inventory.Instance.GetLeftLoopItem()?.Icon;
        RightItemImage.sprite = Inventory.Instance.GetRightLoopItem()?.Icon;
    }

    private void UpdateUIItemsChanged()
    {
        LeftItemImage.sprite = Inventory.Instance.GetLeftLoopItem()?.Icon;
        RightItemImage.sprite = Inventory.Instance.GetRightLoopItem()?.Icon;
    }
}
