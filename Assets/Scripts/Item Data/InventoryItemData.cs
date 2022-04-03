using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public ItemType type;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    
    public bool isStackable;
    [ShowIf("isStackable")]
    public int maxStack;


    public enum ItemType
    {
        Material,
        Accessory,
        Tool,
    }

}
