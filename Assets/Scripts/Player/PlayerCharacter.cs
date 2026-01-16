using System;
using System.Collections.Generic;
using System.Xml.XPath;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCharacter : Character
{
    [Header("PlayerSetting")]
    [SerializeField] public PlayerInventory inventory;
    [SerializeField] private int currxp;
    [SerializeField] private int maxXP;
    [SerializeField] private int currmana;
    [SerializeField] private int maxmana;
    [SerializeField] private List<Upgrade> levelUpgrades = new List<Upgrade>();



    protected override void Death()
    {
        if (IsDead())
        {
            Debug.Log("Смерть");
        }
    }

    /*currxp*/

    public int GetCurrXP()
    {
        return currxp;
    }
    public string GerStringCurrXP()
    {
        return currxp.ToString();
    }
    public void IncreaseCurrXP(int change)
    {
        change = Mathf.Abs(change);
        currxp+=change;
    }
    public void DecreaseCurrXP(int change)
    {
        change = Mathf.Abs(change);
        currxp = Mathf.Max(currxp-change,0);
    }

    /*maxXP*/

    public int GetMaxXP()
    {
        return maxXP;
    }
    public string GetStringMaxXP()
    {
        return maxXP.ToString();
    }
    public void IncreaseMaxXP(int change)
    {
        change = Mathf.Abs(change);
        maxXP+=change;
    }
    public void DecreaseMaxXP(int change)
    {
        change = Mathf.Abs(change);
        maxXP = Mathf.Max(maxXP-change,0);
    }
    public void LevelUp()
    {
        if (currxp >= maxXP && level<levelUpgrades.Count)
        {
            IncreaseLevel();
            DecreaseCurrXP(maxXP);
            IncreaseAtk(levelUpgrades[level-1].changeAtk);
            IncreaseMaxHP(levelUpgrades[level-1].changeMaxHP);
            GetFullHP();
            IncreaseArmor(levelUpgrades[level-1].changeArmor);
            IncreaseMagic(levelUpgrades[level-1].changeMagic);
            GetFullMana();
        }
    }

    public void IncreaseStats(Upgrade upgrade)
    {
        if (upgrade.changeArmor >= 0)IncreaseArmor(upgrade.changeArmor);
        else DecreaseArmor(upgrade.changeArmor);

        if (upgrade.changeAtk >= 0) IncreaseAtk(upgrade.changeAtk);
        else DecreaseAtk(upgrade.changeAtk);

        if (upgrade.changeMagic >= 0) IncreaseMagic(upgrade.changeMagic);
        else DecreaseMagic(upgrade.changeMagic);

        if (upgrade.changeMaxHP >= 0) IncreaseMaxHP(upgrade.changeMaxHP);
        else DecreaseMaxHP(upgrade.changeMaxHP);

        if (upgrade.changeMaxMana >= 0) IncreaseMaxMana(upgrade.changeMaxMana);
        else DecreaseMaxMana(upgrade.changeMaxMana);
    }
    public void DecreaseStats(Upgrade upgrade)
    {
        if (upgrade.changeArmor < 0)IncreaseArmor(upgrade.changeArmor);
        else DecreaseArmor(upgrade.changeArmor);

        if (upgrade.changeAtk < 0) IncreaseAtk(upgrade.changeAtk);
        else DecreaseAtk(upgrade.changeAtk);

        if (upgrade.changeMagic < 0) IncreaseMagic(upgrade.changeMagic);
        else DecreaseMagic(upgrade.changeMagic);

        if (upgrade.changeMaxHP < 0) IncreaseMaxHP(upgrade.changeMaxHP);
        else DecreaseMaxHP(upgrade.changeMaxHP);

        if (upgrade.changeMaxMana < 0) IncreaseMaxMana(upgrade.changeMaxMana);
        else DecreaseMaxMana(upgrade.changeMaxMana);
    }

    /*mana*/

    public int GetCurrMana()
    {
        return currmana;
    }
    public string GetStringCurrMana()
    {
        return currmana.ToString();
    }
    public void IncreaseCurrMana(int change)
    {
        change = Mathf.Abs(change);
        currmana = Mathf.Min(currmana+change,maxmana);
    }
    public void DecreaseCurrMana(int change)
    {
        change = Mathf.Abs(change);
        currmana = Mathf.Max(currmana-change,maxmana);
    }
    public void GetFullMana()
    {
        currmana = maxmana;
    }
    public bool IsAbleToCastAbility(int manause)
    {
        return manause<=currmana;
    }

    /*maxmana*/

    public int GetMaxMana()
    {
        return maxmana;
    }
    public string GetStringMaxMana()
    {
        return maxmana.ToString();
    }
    public void IncreaseMaxMana(int change)
    {
        change = Mathf.Abs(change);
        maxmana += change;
    }
    public void DecreaseMaxMana(int change)
    {
        change = Mathf.Abs(change);
        maxmana = Mathf.Max(0,maxmana-change);
    }

    /*items*/

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void DropItem(Item item)
    {
        inventory.Remove(item);
    }
}
