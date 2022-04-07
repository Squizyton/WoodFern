using Sirenix.OdinInspector;
using UnityEngine;

namespace Rocks
{
    public abstract class BasicRock : MonoBehaviour,IHittable,IInteractable
    {
        [Title("Rock Variables Variables")]
        [SerializeField] protected int health = 5;
        [SerializeField] protected LootTable lootTable;
        [SerializeField] protected float radius;
        [SerializeField] protected Transform materialSpawnPoint;

        private void Awake()
        {
            Interaction = IInteractable.InteractionType.Mine;
        }

        public abstract void OnHit();
        public void SpawnMaterial()
        {
            //Add Force to the rock it spawns
            // Instantiate(LootManager.GenerateItem(lootTable),pos,Quaternion.identity);
        }
    
        public IInteractable.InteractionType Interaction { get; private set; }
    }
}
