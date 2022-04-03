using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public enum InteractionType
    {
        None,
        PickUp,
        Chop,
        Mine,
        Fish
    }
    
    InteractionType Interaction { get; }
}
