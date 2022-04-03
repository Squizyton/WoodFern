using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   //Item that this Object refrences
   [SerializeField] private InventoryItemData itemData;
   //The amount this item will give on pickup
   [SerializeField] private int amount;

   public void OnPickup()
   {
      InventorySystem.instance.AddItem(itemData, amount);
      Destroy(gameObject);
   }


   private void OnTriggerEnter(Collider other)
   {
      
      Debug.Log("Collided with " + other.name);
      
      if(other.transform.root.CompareTag("Player"))
      {
         OnPickup();
      }
   }
}

