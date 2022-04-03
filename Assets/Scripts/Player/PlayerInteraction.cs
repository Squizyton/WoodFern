using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;
    
    public IInteractable.InteractionType lookingAt;
    private float coolDown = 0.5f;
    private float coolDownTimer = 0;
    private Action onInteract;
    private Transform cachedHit;

    [SerializeField] private Transform eyes;
    [SerializeField]private LayerMask interactableMask;
    [SerializeField] private float lookDistance;


    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.forward, out hit, lookDistance,interactableMask))
        {
            Debug.Log(hit.transform);
            
            if(hit.transform != cachedHit && hit.collider.GetComponent<IInteractable>() != null)
            {
                Debug.Log("THIIIIIIIIING");
                
                //Cache the hit so we dont keep trying to GetComponent every frame
                cachedHit = hit.transform;
                
                //Get the interaction type of the object we are looking at
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                
                //If the object is interactable, set the lookingAt variable to the interaction type
                lookingAt = interactable.Interaction;

                Debug.Log(lookingAt);
                //Set the onInteract action based on the interaction type
                SwitchInteraction(lookingAt);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Test");
            if (coolDownTimer <= 0)
                onInteract?.Invoke();
            
        }
    }
    
    public void SwitchInteraction(IInteractable.InteractionType interaction)
    {
        switch (interaction)
        {
            case IInteractable.InteractionType.None:
                if(onInteract != null)
                    onInteract = null;
                break;
            case IInteractable.InteractionType.Chop:
                onInteract += Chop;
                break;
        }
    }

    #region Interaction Methods
    public void Chop()
    {
        Debug.Log("Chop");
        PlayerMovement.instance.anim.SetTrigger("chop");
        SetTimer(2.6f);
    }
    #endregion
    
    
    #region Cooldown Methods

    public void SetTimer(float time)
    {
        coolDownTimer = coolDown;
        StartCoroutine(StartCoolDown());
    }
    
    IEnumerator StartCoolDown()
    {
        while (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion


    private void OnDrawGizmos()
    {
        Debug.DrawLine(eyes.position, eyes.position + transform.forward * lookDistance, Color.red);
    }
}
