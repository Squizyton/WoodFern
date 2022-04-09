using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
   public int slotNumber;
   public ItemUISlot currentItem;
   
   public void AddItem(ItemUISlot item)
   {
       currentItem = item;
   }

}
