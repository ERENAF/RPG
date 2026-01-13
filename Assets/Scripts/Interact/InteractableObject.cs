using Unity.VisualScripting;
using UnityEngine;

public enum StateOfInteractableObject
{
    Interactable,
    NotInteractable,
}

public abstract class InteractableObject : MonoBehaviour
{
    [Header("Configs of Interactable object")]
    public StateOfInteractableObject state;

    public virtual void Interact(){}
    protected virtual bool IsAbleToInteract(){return false;}

}
