using UnityEngine;

[System.Serializable]
public class Upgrade
{
    [SerializeField] public int changeAtk;
    [SerializeField] public int changeMaxHP;
    [SerializeField] public int changeArmor;
    [SerializeField] public int changeMagic;
    [SerializeField] public int changeMaxMana;

    public override string ToString()
    {
        return $"ATK: {changeAtk}, MAXHP: {changeMaxHP}, ARMOR: {changeArmor}, MAGIC: {changeMagic}, MAXMANA: {changeMaxMana}";
    }
}
