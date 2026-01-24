using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public Sprite enemySprite;
    public Character enemyCharacter;
    private BattleManager battleManager;

    void Start()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
    }

    public void StartBattle()
    {
        battleManager.enemySprite = enemySprite;
        battleManager.enemyCharacter = enemyCharacter;
        battleManager.StartFight();
    }
}
