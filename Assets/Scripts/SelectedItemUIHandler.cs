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
        Inventory.Instance.OnSelectedItemChanged += UpdateSelectedItemUI;
        
        UpdateSelectedItemUI(Inventory.Instance.SelectedItem);
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnSelectedItemChanged -= UpdateSelectedItemUI;
    }

    private void UpdateSelectedItemUI(Item item)
    {
        SelectedItemImage.sprite = item?.Icon;
        LeftItemImage.sprite = Inventory.Instance.GetLeftLoopItem()?.Icon;
        RightItemImage.sprite = Inventory.Instance.GetRightLoopItem()?.Icon;
    }
}
