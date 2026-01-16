using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger : MonoBehaviour
{
    public bool isPlayerOnTrigger = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("вход в триггер");
            isPlayerOnTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("выход из триггера");
            isPlayerOnTrigger = false;
        }
    }
}
