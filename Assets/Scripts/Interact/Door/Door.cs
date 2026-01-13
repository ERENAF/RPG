using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    [Header("Door's config")]

    public DoorTrigger doorTrigger;
    public QuestItem key;
    public KeyCode interactKey = KeyCode.F;
    public int SceneNum = -1;
    public string SceneStr;

    void Update()
    {
        Interact();
    }

    public override void Interact()
    {
        if (IsAbleToInteract())
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (FindAnyObjectByType<PlayerCharacter>().items.Contains(key) || key == null)
                {
                    DoorInteracting();
                }
                else
                {
                    NotKeyInteracting();
                }
            }
        }
    }
    public void NotKeyInteracting()
    {
        Debug.Log("Нет Ключа");
    }
    public void DoorInteracting()
    {
        if (SceneNum >= 0)
        {
            SceneManager.LoadScene(SceneNum);
        }
        else if (SceneStr != null)
        {
            SceneManager.LoadScene(SceneStr);
        }
        Debug.Log("интеракция двери");
    }

    protected override bool IsAbleToInteract()
    {
        return state == StateOfInteractableObject.Interactable && doorTrigger.isPlayerOnTrigger == true;
    }
}
