using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueTree",menuName = "Dialogue/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    public string startNodeID;
    public List<DialogueNode> nodes = new List<DialogueNode>();

    private Dictionary<string, DialogueNode> nodeDictionary;

    public void InitDictionary()
    {
        nodeDictionary = new Dictionary<string, DialogueNode>();
        foreach (var node in nodes)
        {
            nodeDictionary[node.nodeID] = node;
        }
    }

    public DialogueNode GetNode(string nodeID)
    {
        if (nodeDictionary == null)
        {
            InitDictionary();
        }
        if (nodeDictionary.TryGetValue(nodeID, out DialogueNode node))
        {
            return node;
        }
        Debug.LogError($"Node with ID {nodeID} not found!");
        return null;
    }
}
