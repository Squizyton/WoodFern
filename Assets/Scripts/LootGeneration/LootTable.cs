using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item Table")]
public class LootTable : ScriptableObject
{
    public List<LootTable> includedTables;
    
    
    public List<ItemDrop> items;
    
}


[Serializable]
public struct ItemDrop
{
    public GameObject itemPrefab;
    public int weight;
}