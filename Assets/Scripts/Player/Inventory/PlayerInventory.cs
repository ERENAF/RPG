using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Scriptable Objects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    [SerializeField] private List<Item> activeItems = new List<Item>();
    [SerializeField] private List<Item> passiveItems = new List<Item>();
    [SerializeField] private List<Item> questItems = new List<Item>();

    [SerializeField] public PlayerCharacter player;

    public void Add(Item item)
    {
        AddItem(item);
    }

    public void Remove(Item item, Transform dropPoint)
    {
        DropItem(item);
        item.DropItem(dropPoint);
    }

    public List<Item> FindListItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.active:
                return activeItems;
            case ItemType.passive:
                return passiveItems;
            case ItemType.questItem:
                return questItems;
            default: return null;
        }
    }
    public List<Item> FindListItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.active:
                return activeItems;
            case ItemType.passive:
                return passiveItems;
            case ItemType.questItem:
                return questItems;
            default: return null;
        }
    }

    private void AddItem(Item item)
    {
        List<Item> foundListItem = FindListItem(item.type);
        foundListItem.Add(item);
    }

    private void DropItem(Item item)
    {
        List<Item> foundListItem = FindListItem(item.type);
        if (foundListItem != null)
        {
            int index = foundListItem.FindIndex(i => i == item);
            if (index >= 0 && index < foundListItem.Count)
            {
                foundListItem.RemoveAt(index);
            }
        }
    }
}
