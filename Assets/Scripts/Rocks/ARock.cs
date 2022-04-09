using System.Collections;
using System.Collections.Generic;
using Rocks;
using UnityEngine;

public class ARock : BasicRock
{
    

    public override void OnHit()
    {
        health--;

        var rock = Instantiate(LootManager.GenerateItem(lootTable), transform.position + new Vector3(0,2,0), Quaternion.identity);
            
        rock.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,2),Random.Range(0,2),Random.Range(0,2)),ForceMode.Impulse);
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tool"))
        {
            OnHit();
        }
    }
}
