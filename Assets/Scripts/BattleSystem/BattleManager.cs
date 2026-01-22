
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {Start,Player_Turn, Enemy_Turn, Win,Lose}

public class BattleManager : MonoBehaviour
{
    [Header("UnitPrefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [Header("BattleTransform")]
    public Transform playerTransform;
    public Transform enemyTransform;

    [Header("UI")]
    public GameObject battleUI;
    public TextMeshProUGUI battleText;

    [Header("Character Data")]
    public PlayerCharacter playerCharacter;
    public Character enemyCharacter;

    private BattleState state;

    public void StartFight()
    {
        state = BattleState.Start;
        battleUI.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        Instantiate(playerPrefab, playerTransform);
        Instantiate(enemyPrefab, enemyTransform);

        battleText.text = "БИТВА НАЧИНАЕТСЯ!";

        yield return new WaitForSeconds(2f);

        state = BattleState.Player_Turn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        battleText.text = "Выберите действие:";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.Player_Turn) return;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        enemyCharacter.DecreaseCurrHP(playerCharacter.GetAtk());
        battleText.text = $"{playerCharacter.name} атакует кирпичом!";
        yield return new WaitForSeconds(2f);

        if (enemyCharacter.IsDead())
        {
            state = BattleState.Win;
            EndBattle();
        }
        else
        {
            state = BattleState.Enemy_Turn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        battleText.text = "Вражеский ход!";
        yield return new WaitForSeconds(1f);

        int typeAttack = Random.Range(0,2);
        switch (typeAttack)
        {
            case 0:
                battleText.text = "Враг ударяет по вам!" ;
                playerCharacter.DecreaseCurrHP(enemyCharacter.GetAtk());
                break;
            case 1:
                battleText.text = "Враг использует магию по вам!" ;
                playerCharacter.DecreaseCurrHP(enemyCharacter.GetMagic());
                break;
        }

        if (playerCharacter.IsDead())
        {
            state = BattleState.Lose;
            EndBattle();
        }
        else
        {
            state = BattleState.Start;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        switch (state)
        {
            case BattleState.Win:
                battleText.text = "ВЫ ВЫИГРАЛИ!";
                break;
            case BattleState.Lose:
                battleText.text = "ВЫ ПРОИГРАЛИ(";
                break;
        }
        battleUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
