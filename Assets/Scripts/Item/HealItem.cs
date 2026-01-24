using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Items/HealItem")]

public class HealItem : Item
{

    public int healHP;
    public override void OnEquip(PlayerCharacter character)
    {

    }
    public override void OnUnequip(PlayerCharacter character)
    {
    }

    public override void Use(PlayerCharacter character)
    {
        character.IncreaseCurrHP(healHP);
    }

    public override bool CanUse(PlayerCharacter character)
    {
        return true;
    }
}
