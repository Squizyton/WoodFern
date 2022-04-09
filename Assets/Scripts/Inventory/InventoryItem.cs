using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
   //Item data that this item will reference
   public InventoryItemData itemData { get; private set; }

   //Current amount of this item
   public int stackSize { get; private set; }
   
   //Constructor
   public InventoryItem(InventoryItemData itemData, int amount)
   {
      this.itemData = itemData;
      AddToStack(amount);
   }
   
   //Adds to the stack size
   public void AddToStack(int amount)
   {
      //If the stack size is less than the max stack size
      if(stackSize < itemData.maxStack)
      {
         //Add to the stack size
         stackSize+= amount;
      }
   }
   public void RemoveFromStack()
   {
      //Remove from the stack size
      //TODO: Remove any amount from the stack size
      stackSize--;
   }


   //Sort the inventory by item type  
   public void SortInventory()
   {
      
   }
}
