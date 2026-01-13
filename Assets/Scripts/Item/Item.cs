using Unity.Burst.Intrinsics;
using UnityEngine;

public enum Rarity
{
    Common,
    UnCommon,
    Rare,
    Epic,
    Legendary
}

public enum UseType
{
    active,
    passive,
    weapon,
    questItem
}

public abstract class Item : ScriptableObject
{
    [Header("item config")]
    public string itemname;
    public string description;
    public Rarity rarity = Rarity.Common;
    public UseType useType = UseType.passive;
    public GameObject dropItem;
    public int charges = 0;
    public int maxChargers = 0;

    public abstract void OnEquip(PlayerCharacter character);
    public abstract void OnUnequip(PlayerCharacter character);
    public abstract bool CanUse(PlayerCharacter character);
    public abstract void Use(PlayerCharacter character);

    public void DropItem()
    {

    }
}
