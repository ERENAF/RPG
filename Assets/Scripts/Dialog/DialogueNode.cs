using UnityEngine;
using System;
using Microsoft.Unity.VisualStudio.Editor;

public enum OptionType
{
    continueTalk,
    fight,
    end
}


[System.Serializable]
public class DialogueNode
{
    public string nodeID;
    [TextArea(3,5)] public string dialogueLine;
    public string characterName;
    public Sprite characterImage;
    public string nextNodeID;
    public Option[] options;
}

[System.Serializable]
public class Option
{
    [TextArea(3,5)] public string dialogueLine;
    [SerializeField] private string nextNodeID;
    public OptionType type = OptionType.continueTalk;

    public string OptionUse()
    {
        switch (type)
        {
            case OptionType.continueTalk:
                return OptionUseContinueTalk();
            case OptionType.fight:
                return OptionUseFight();
            case OptionType.end:
                return OptionUseEnd();
        }
        return "END";
    }

    private string OptionUseContinueTalk()
    {
        return nextNodeID;
    }
    private string OptionUseFight()
    {
        return "END";
    }
    private string OptionUseEnd()
    {
        return "END";
    }
}
