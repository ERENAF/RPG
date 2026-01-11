using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("CharacterSetting")]

    [SerializeField] protected string characterName;

    [Header("BattleSetting")]

    [SerializeField] protected int currhp;
    [SerializeField] protected int maxhp;
    [SerializeField] protected int armor;
    [SerializeField] protected int atk;
    [SerializeField] protected int magic;
    [SerializeField] protected int level;

    /*characterName*/

    public string GetCharacterName()
    {
        return characterName;
    }

    /*currHP functions*/

    public int GetCurrHp()
    {
        return currhp;
    }
    public string GetStringCurrHp()
    {
        return currhp.ToString();
    }
    public void IncreaseCurrHP(int change)
    {
        change = Mathf.Abs(change);
        currhp = Mathf.Min(currhp+change,maxhp);
    }
    public void DecreaseCurrHP(int change)
    {
        change = Mathf.Abs(change);
        currhp = Mathf.Max(currhp - Mathf.Max(change-armor,0),0);
    }
    public void GetFullHP()
    {
        currhp = maxhp;
    }

    /*death and alive checkfunctions*/

    public bool IsAlive()
    {
        return currhp>0;
    }
    public bool IsDead()
    {
        return currhp <=0;
    }
    protected abstract void Death();

    /*maxHP functions*/

    public string GetStringMaxHP()
    {
        return maxhp.ToString();
    }
    public int GetMaxHP()
    {
        return maxhp;
    }
    public void DecreaseMaxHP(int change)
    {
        change = Mathf.Abs(change);
        maxhp = Mathf.Max(0, maxhp-change);
        currhp = Mathf.Min(maxhp,currhp);
    }
    public void IncreaseMaxHP(int change)
    {
        change = Mathf.Abs(change);
        maxhp += change;
    }

    /*armor*/

    public void IncreaseArmor(int change)
    {
        change = Mathf.Abs(change);
        armor+=change;
    }
    public void DecreaseArmor(int change)
    {
        change = Mathf.Abs(change);
        armor -= change;
    }
    public string GetStringArmor()
    {
        return armor.ToString();
    }
    public int GetArmor()
    {
        return armor;
    }

    /*atk*/

    public int GetAtk()
    {
        return atk;
    }
    public string GetStringAtk()
    {
        return atk.ToString();
    }
    public void DecreaseAtk(int change)
    {
        change = Mathf.Abs(change);
        atk = Mathf.Max(atk-change,0);
    }
    public void IncreaseAtk(int change)
    {
        change = Mathf.Abs(change);
        atk += change;
    }

    /*magic*/
    public int GetMagic()
    {
        return magic;
    }
    public string GetStringMagic()
    {
        return magic.ToString();
    }
    public void IncreaseMagic(int change)
    {
        change = Mathf.Abs(change);
        magic += change;
    }
    public void DecreaseMagic(int change)
    {
        change = Mathf.Abs(change);
        magic = Mathf.Max(magic-change,0);
    }

    /*level*/

    public int GetLevel()
    {
        return level;
    }
    public string GetStringLevel()
    {
        return level.ToString();
    }
    public void DecreaseLevel(int change = 1)
    {
        change = Mathf.Abs(change);
        level -= change;
    }
    public void IncreaseLevel(int change = 1)
    {
        change = Mathf.Abs(change);
        level += change;
    }

    /*other functions*/


}
