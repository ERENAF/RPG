using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DropItem : MonoBehaviour
{
    [SerializeField] protected Item Item;
    [SerializeField] protected string tag_ = "Player";
    [SerializeField] protected string describtion;

    private void OnTriggerStay2D(Collider2D other)
    {
        DropOnTriggerStay2D(other);
    }


    protected virtual void DropOnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(tag_))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                other.GetComponent<PlayerCharacter>().AddItem(Item);
                Destroy(gameObject);
            }
        }
    }
}
