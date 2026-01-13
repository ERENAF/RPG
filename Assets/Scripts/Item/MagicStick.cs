using UnityEngine;

[CreateAssetMenu(fileName = "MagicStick", menuName = "Items/MagicStick")]
public class MagicStick : Item
{
    [Header("Magic Stick config")]

    public int magicUp;

    public override void OnEquip(PlayerCharacter character)
    {
        character.IncreaseMagic(magicUp);
    }
    public override void OnUnequip(PlayerCharacter character)
    {
        character.DecreaseMagic(magicUp);
    }
    public override bool CanUse(PlayerCharacter character)
    {
        return false;
    }
    public override void Use(PlayerCharacter character){}
}
