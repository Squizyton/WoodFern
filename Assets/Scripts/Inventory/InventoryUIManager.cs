using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    
    [Title("UI")]
    public GameObject inventoryUI;
    public GameObject inventorySlotPrefab;
    
    [SerializeField]private GameObject slotParent;
    [SerializeField] private Transform[] slots;
    private List<GameObject>currentInventorySlots;
    private List<GameObject>currentlySpawnedSlots;
    

    [Title("Above Head UI")]
    [SerializeField]private GameObject objectAcquiredPrefab;
    [SerializeField] private Transform aboveHead;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
    
    private void Start()
    {
        //If the inventory ui is active, then we want to deactivate it
        
        currentlySpawnedSlots = new List<GameObject>();
        
        CloseInventory();
    }

    //Todo: Make inventory Grid Based
    
    
    //Open inventory
    public void OpenInventory()
    {
        Cursor.visible = true;
        
        inventoryUI.SetActive(true);
        RefreshInventory();
    }
    
    //Close inventory
    public void CloseInventory()
    {
        Cursor.visible = false;
        inventoryUI.SetActive(false);
        ClearInventory();
    }

    private void ClearInventory()
    {
        if (currentlySpawnedSlots.Count <= 0) return;
        foreach (var item in currentlySpawnedSlots)
        {
            Destroy(item);
        }
    }
    
    //Update the inventory slots
    //TODO: Automatically update the inventory slots when the inventory changes
    public void RefreshInventory()
    {
        ClearInventory();
        var index = -1;

        foreach (var item in InventorySystem.instance.inventoryItems )
        {
            index++;
            var slotUI = Instantiate(inventorySlotPrefab, slots[index]);
            var uiVariables = slotUI.GetComponent<ItemUISlot>();
            uiVariables.itemCount.SetText(item.stackSize.ToString());
            uiVariables.itemName.SetText(item.itemData.displayName);
            uiVariables.itemSprite.sprite = item.itemData.icon;
            currentlySpawnedSlots.Add(slotUI);
        }
    }


    public void ItemAcquired(InventoryItemData data, int stackAmount)
    {
        //Instiate the object
        var item = Instantiate(objectAcquiredPrefab, aboveHead.position, Quaternion.identity,aboveHead);
        //Change the icon to match the item data icon
        item.GetComponent<Image>().sprite = data.icon;
        //Change the text to match the stack amount
        item.GetComponentInChildren<TextMeshProUGUI>().text = "+" + stackAmount;
    }
}
