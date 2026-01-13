using UnityEngine;

[CreateAssetMenu(fileName = "ItemWithOnlyStatUpgrade", menuName = "Items/Equipment/ItemWithOnlyStatUpgrade")]
public class ItemWithStatUpgrade : Item
{
    [Header(" config")]

    public Upgrade upgrade;

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
