using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    
    [SerializeField]private GameObject objectAcquiredPrefab;
    [SerializeField] private Transform aboveHead;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
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
