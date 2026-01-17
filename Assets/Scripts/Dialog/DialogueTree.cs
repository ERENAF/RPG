using Unity;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
[CreateAssetMenu(fileName = "", menuName = "Dialogue/DialogueTree")]
public class DialogueTree : ScriptableObject
{
    public string startNodeID;
    public List<DialogueNode> nodes = new List<DialogueNode>();
    private Dictionary<string,DialogueNode> nodeDictionary;
    private void InitDictionary()
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
        else
        {
            Debug.LogError($"Node with ID {nodeID} not found!");
            return null;
        }
    }

}
