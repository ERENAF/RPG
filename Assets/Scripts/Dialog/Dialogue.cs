using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


[System.Serializable]
public class DialogueNode
{
    public string nodeID;
    [TextArea(3,5)] public string line;
    public List<DialogueOption> options;
    public Sprite characterSprite;
    public string charName;
}

[System.Serializable]
public class DialogueOption
{
    public string text;
    public string nextNodeID;
    public bool isAvailable = true;
    public List<Condition> conditions;
}

public enum ComparisonType
{
    Equals,
    GreaterThan,
    LessThan
}

[System.Serializable]
public class Condition
{
    public string variableName;
    public int requiredValue;
    public ComparisonType comparisonType;
}
