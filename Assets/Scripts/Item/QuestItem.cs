using UnityEngine;

[CreateAssetMenu(fileName = "QuestItem", menuName = "Items/Quest/QuestItem")]
public class QuestItem : Item
{
    public override void OnEquip(PlayerCharacter character){}
    public override void OnUnequip(PlayerCharacter character){}
    public override bool CanUse(PlayerCharacter character){return false;}
    public override void Use(PlayerCharacter character){}
}
