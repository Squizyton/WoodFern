using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is used to control the inventory of the player
//For Debugging purposes this is a monobehaviour

[Serializable]
public class InventorySystem : MonoBehaviour
{
    //Create Instance of this Inventory System as a Singleton
    public static InventorySystem instance;
    
    //Allows you grab item data more easily
    public Dictionary<InventoryItemData, InventoryItem> itemDictionary;

    public List<InventoryItem> inventoryItems { get; private set; }


    private void Awake()
    {
        instance = this;
        
        //Initialize the inventory list and dictionary
        inventoryItems = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }


    public void AddItem(InventoryItemData reference,int amount)
    {
        //Check the dictionary to see if the item is already in the inventory
        if (itemDictionary.TryGetValue(reference, out var value))
        {
            value.AddToStack(amount);
        }
        else
        {
            Debug.Log($"Adding Item: {reference.id}");
            
            //Create a new item
            var newItem  = new InventoryItem(reference);
            newItem.AddToStack(amount);
            //Add the item to the inventory
            inventoryItems.Add(newItem);
            //add the item to the dictionary
            itemDictionary.Add(reference, newItem);
        }
        InventoryUIManager.instance.ItemAcquired(reference, amount);
    }
    
    public void RemoveItem(InventoryItemData reference)
    {
        //Check the dictionary to see if the item is already in the inventory
        if (itemDictionary.TryGetValue(reference, out var value))
        {
            value.RemoveFromStack();

            if (!value.stackSize.Equals(0)) return;
            
            inventoryItems.Remove(value);
            itemDictionary.Remove(reference);
        }
    }
}
