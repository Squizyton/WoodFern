using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }


    public static GameObject GenerateItem(LootTable table)
    {
        var availableItems = table.items.ToList();
        
        //Go through all the items in includedTables and add to avaiableItems
        foreach (var includedItems in table.includedTables.Select(includedTable => includedTable.items.ToList()))
        {
            availableItems.AddRange(includedItems);
        }
        
        //Add each of the items weight to the total weight
        var totalWeight = availableItems.Sum(item => item.weight);
        //Correspond weight to the item
        var weightIndex = availableItems.Select(weight => weight.weight).ToList();
        
        //Index of the available item list
        var index = 0;
        
        var lastIndex  = weightIndex.Count - 1;

        while (index < lastIndex)
        {
        
            //Do a probability check with a likelihood of weight. The greater the number, the greater the likely its to spawn
            if (Random.Range(0, totalWeight) > weightIndex[index])
            {
                return availableItems[index].itemPrefab;
            }
            //Remove the last item from the sum of the total untested weight and try again
            totalWeight -= weightIndex[index++];
        }
        
        
        //If no item was picked, return the last item
        return availableItems[index].itemPrefab;
    }


}
