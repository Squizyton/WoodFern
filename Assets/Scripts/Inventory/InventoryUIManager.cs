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

    [Title("UI")] public GameObject inventoryUI;
    public GameObject inventorySlotPrefab;

    [Title("Slot Variables")] [SerializeField]
    private GameObject slotParent;

    [SerializeField] private Slot[] slots;
    private List<GameObject> currentlySpawnedSlots;


    [Title("Inventory Manipulation")] public bool holdingSomething;
    public Slot overSlot;
    public ItemUISlot itemBeingHeld;

    [Title("Above Head UI")] [SerializeField]
    private GameObject objectAcquiredPrefab;

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


    //TODO: Clean up code
    private void Update()
    {
        if (!inventoryUI.activeSelf) return;

        if (Input.GetMouseButton(0) && overSlot != null)
        {
            Debug.Log("Mouse button down");
            if (overSlot.currentItem != null)
            {
                holdingSomething = true;
                itemBeingHeld = overSlot.currentItem;
                overSlot.currentItem.transform.SetParent(null);
                //have the item being held to follow the mouse
                itemBeingHeld.transform.position = Input.mousePosition;
                //TODO: Turn off the name of the item and make it transparent
            }

           
        }

        if (holdingSomething)
        {
            itemBeingHeld.nextSlot = overSlot;
        }
        
        if (!Input.GetMouseButtonUp(0)) return;
        if (!holdingSomething) return;
        //If the item is over a slot that's empty, then we want to move the item to that slot
        if (overSlot.currentItem == null)
        {
            itemBeingHeld.previousSlot.currentItem = null;
             itemBeingHeld.nextSlot.AddItem(itemBeingHeld);
            itemBeingHeld.transform.SetParent(itemBeingHeld.nextSlot.transform);
            itemBeingHeld.transform.localPosition = Vector3.zero;
            itemBeingHeld.previousSlot = itemBeingHeld.nextSlot;
            itemBeingHeld.nextSlot = null;
            holdingSomething = false;
        }
        //If the are holding but not over a valid slot then put it back in it's previous slot
        else
        {
            itemBeingHeld.transform.SetParent(itemBeingHeld.previousSlot.transform);
            itemBeingHeld.transform.localPosition = Vector3.zero;
            holdingSomething = false;
        }

        itemBeingHeld = null;
    }


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

        foreach (var item in InventorySystem.instance.inventoryItems)
        {
            //Increase the index
            index++;
            //Instantiate the slot
            var slotUI = Instantiate(inventorySlotPrefab, slots[index].transform);
            //Acquire the slot variables
            var uiVariables = slotUI.GetComponent<ItemUISlot>();
            //Set the slot variables
            uiVariables.itemCount.SetText(item.stackSize.ToString());
            uiVariables.itemName.SetText(item.itemData.displayName);
            uiVariables.itemSprite.sprite = item.itemData.icon;

            //Add the slot to the list
            currentlySpawnedSlots.Add(slotUI);
            //Add the item to the slot so Inventory knows it can be moved
            slots[index].AddItem(uiVariables);
        }
    }


    public void ItemAcquired(InventoryItemData data, int stackAmount)
    {
        //Instiate the object
        var item = Instantiate(objectAcquiredPrefab, aboveHead.position, Quaternion.identity, aboveHead);
        //Change the icon to match the item data icon
        item.GetComponent<Image>().sprite = data.icon;
        //Change the text to match the stack amount
        item.GetComponentInChildren<TextMeshProUGUI>().text = "+" + stackAmount;
    }
}