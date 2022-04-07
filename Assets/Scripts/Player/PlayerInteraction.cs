using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;

    [Title("Interaction Settings")]
    public IInteractable.InteractionType lookingAt;
    private float coolDown = 0.5f;
    private float coolDownTimer = 0;
    private Action onInteract;
    private Transform cachedHit;

    [Title("Overlap Settings")]
    [SerializeField] private Transform eyes;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float lookDistance;
    [SerializeField] private float overlapRadius;
    [SerializeField] private Collider[] cachedOverlapResults;
    
    
    [Title("Current Interaction Mode")]
    [SerializeField] private GameMode currentMode;

    [Title("Debug Values")] public GameObject closestOverlap;
    public bool showRay;
    public bool showOverlap;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        #region Physics.OverlapSphere

        switch (currentMode)
        {
            case GameMode.InWorld:
                //Do a OverlapSphere to see if we are looking at an interactable object
                Collider[] hitColliders = Physics.OverlapSphere(eyes.position, overlapRadius, interactableMask);

                if (hitColliders.Length.Equals(0))
                {
                    //If we are not looking at anything, But were looking at something before, reset the lookingAt variable
                    if (lookingAt != IInteractable.InteractionType.None)
                            lookingAt = IInteractable.InteractionType.None;

                    return;
                }

                var closest = hitColliders.Where(x => x.GetType() == typeof(BoxCollider))
                    .OrderBy(x => Vector3.Distance(x.transform.position, eyes.position)).FirstOrDefault()?.gameObject;

                if (closest == null) return;

                lookingAt = closest.GetComponent<IInteractable>().Interaction;

                SwitchInteraction(lookingAt);

                #endregion

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //Debug.Log("Test");
                    if (coolDownTimer <= 0)
                        onInteract?.Invoke();
                }


                if (Input.GetKeyDown(KeyCode.I))
                {
                    currentMode = GameMode.InMenu;
                }

                break;
            
            case GameMode.InMenu:
                
                
                break;
        }
    }


    public void SwitchInteraction(IInteractable.InteractionType interaction)
    {
        switch (interaction)
        {
            case IInteractable.InteractionType.None:
                if (onInteract != null)
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
        if (showRay)
            Debug.DrawLine(eyes.position, eyes.position + transform.forward * lookDistance, Color.red);

        if (showOverlap)
            Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(eyes.position, overlapRadius);
    }

    private enum GameMode
    {
        InWorld,
        InMenu,
        InFishing
    }

}