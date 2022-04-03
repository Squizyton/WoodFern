using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakWood : BasicTree
{
    public override void OnHit()
    {
        health--;
        //Spawn a material item
        SpawnMaterial();
        //Shake the tree
        if (health <= 0)
        {
           // TODO:Cut down tree and start growing again when day night cycle is implemented
           Destroy(gameObject);
           PlayerInteraction.instance.SwitchInteraction(IInteractable.InteractionType.None);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.CompareTag("Tool"))
        {
            OnHit();
        }
    }
}
