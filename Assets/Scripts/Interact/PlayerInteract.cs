using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float rayDistance = 10f;

    [SerializeField] private LayerMask mask;

    private PlayerUI playerUI;

    public PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayDistance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (playerInput.actions["Interact"].triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}