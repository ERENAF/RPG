

using UnityEngine;

public class Chest : InteractableObject
{
    [Header("Chest's Config")]
    public PlayerCharacter playerCharacter;
    public Item item;

    void Start()
    {
        playerCharacter = FindAnyObjectByType<PlayerCharacter>();
    }

    void Update()
    {
        Interact();
    }

    public override void Interact()
    {
        if (IsAbleToInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                state = StateOfInteractableObject.NotInteractable;
                playerCharacter.AddItem(item);
            }
        }
    }

    protected override bool IsAbleToInteract()
    {
        return state == StateOfInteractableObject.Interactable && trigger.isPlayerOnTrigger == true;
    }
}
