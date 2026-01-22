using UnityEngine;

[CreateAssetMenu(fileName = "SimpleItem", menuName = "Items/Item/SimpleItem")]
public class SimpleItem : Item
{
    public override void OnEquip(PlayerCharacter character)
    {
        character.IncreaseStats(upgrade);
    }
    public override void OnUnequip(PlayerCharacter character)
    {
        character.DecreaseStats(upgrade);
    }

    public override bool CanUse(PlayerCharacter character)
    {
        return false;
    }
    public override void Use(PlayerCharacter character){}
}
