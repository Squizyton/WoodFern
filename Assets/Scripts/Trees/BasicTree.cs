using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BasicTree : MonoBehaviour,IHittable,IInteractable
{
    [Title("Tree Variables")]
    [SerializeField] protected int health = 5;
    [SerializeField] protected LootTable lootTable;
    [SerializeField] protected int growRate;
    [SerializeField] protected int maxGrowRate;
    [SerializeField] protected float radius;
    [SerializeField] protected Transform materialSpawnPoint;

    private void Awake()
    {
        Interaction = IInteractable.InteractionType.Chop;
    }

    public abstract void OnHit();
    public void SpawnMaterial()
    {
        var xPos = Mathf.Cos(Mathf.Deg2Rad * Random.Range(0, 360)) * Random.Range(0,radius) + materialSpawnPoint.position.x;
        var zPos = Mathf.Sin(Mathf.Deg2Rad * Random.Range(0, 360)) * Random.Range(0,radius) + materialSpawnPoint.position.z;
        
        var pos = new Vector3(xPos,materialSpawnPoint.position.y,zPos);
        
        Instantiate(LootManager.GenerateItem(lootTable),pos,Quaternion.identity);
    }


    public void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(materialSpawnPoint.transform.position, new Vector3(0, 1, 0), radius);
    }
    public IInteractable.InteractionType Interaction { get; private set; }
    
};

