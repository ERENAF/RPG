using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum TypeDialogueTrigger
{
    OnEnter,
    OnStay,
    OnExit
}

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueTree dialogueTree;
    [SerializeField] private string startNodeID;
    public TypeDialogueTrigger typeDialogueTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && typeDialogueTrigger == TypeDialogueTrigger.OnEnter)
        {
            FindAnyObjectByType<DialogueSystem>().StartDialogue(startNodeID);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && typeDialogueTrigger == TypeDialogueTrigger.OnStay && Input.GetKeyDown(KeyCode.F))
        {
            FindAnyObjectByType<DialogueSystem>().StartDialogue(startNodeID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && typeDialogueTrigger == TypeDialogueTrigger.OnExit)
        {
            FindAnyObjectByType<DialogueSystem>().StartDialogue(startNodeID);
        }
    }
}
