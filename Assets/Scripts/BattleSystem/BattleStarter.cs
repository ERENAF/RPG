using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Character enemyCharacter;
    private BattleManager battleManager;

    void Start()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
    }

    public void StartBattle()
    {
        battleManager.enemyPrefab = enemyPrefab;
        battleManager.enemyCharacter = enemyCharacter;
        battleManager.StartFight();
    }
}
