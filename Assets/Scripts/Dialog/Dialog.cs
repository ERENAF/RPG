using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum DialogType
{
    Chooseable,
    NonChooseable
}

public class Choice : ScriptableObject
{
    [SerializeField] public string choice;
    [SerializeField] public int nexDialogIndex;
}

[CreateAssetMenu(fileName = "Dialog", menuName = "DialogSystem/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] public string line;
    [SerializeField] public int speedText;
    [SerializeField] public int nextDialogIndex;
    [SerializeField] public DialogType type;
    [SerializeField] public Choice[] choices;
}
