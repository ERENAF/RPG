using UnityEngine;


public enum TriggerType
{
    Enter,
    Stay,
    Exit
}
[RequireComponent(typeof(BoxCollider2D))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    public DialogueTree dialogueTree;
    public BattleStarter battleStarter;
    public TriggerType triggerType;

    void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerType == TriggerType.Enter)
        {
            dialogueManager.battleStarter = battleStarter;
            dialogueManager.StartDialogue(dialogueTree);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerType == TriggerType.Stay && Input.GetKeyDown(KeyCode.F))
        {
            dialogueManager.battleStarter = battleStarter;
            dialogueManager.StartDialogue(dialogueTree);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerType == TriggerType.Exit)
        {
            dialogueManager.battleStarter = battleStarter;
            dialogueManager.StartDialogue(dialogueTree);
        }
    }
}
