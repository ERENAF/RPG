using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade",menuName = "Upgrade")]

public class Upgrade : ScriptableObject
{
    [SerializeField] public int changeAtk;
    [SerializeField] public int changeMaxHP;
    [SerializeField] public int changeArmor;
    [SerializeField] public int changeMagic;
    [SerializeField] public int changeMaxMana;
}
