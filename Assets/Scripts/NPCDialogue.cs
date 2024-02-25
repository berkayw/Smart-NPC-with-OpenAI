using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class NPCDialogue : Interactable
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject dialogueState;

    [SerializeField] private Transform standingPoint;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Interact()
    {
        //disable inputs and clear prompt message
        player.GetComponent<PlayerInput>().enabled = false;
        promptMessage = string.Empty;
        
        
        player.SetActive(false);
        //teleport to standing point
        player.transform.position = standingPoint.position;
        player.transform.rotation = standingPoint.rotation;
        player.SetActive(true);

        //disable main cam, enable dialogue cam
        mainCamera.SetActive(false);
        dialogueState.SetActive(true);

        //display cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseDialogueState()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        promptMessage = "Press \"E\" To Talk";
        
        mainCamera.SetActive(true);
        dialogueState.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
