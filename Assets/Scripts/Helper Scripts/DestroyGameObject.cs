using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
   [SerializeField] private float timer;
   
   
   public void DestroyWithTimer()
   {
       Destroy(gameObject, timer);
   }
   
   
   public void DestroyNoTimer()
   {
       Destroy(gameObject);
   }
}
