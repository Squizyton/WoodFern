using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotNumber;
    public ItemUISlot currentItem;

    public void AddItem(ItemUISlot item)
    {
        currentItem = item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        InventoryUIManager.instance.overSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUIManager.instance.overSlot = null;
    }
}