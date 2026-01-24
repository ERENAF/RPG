using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, Player_Turn, Player_UsingItem, Enemy_Turn, Win, Lose }

public class BattleManager : MonoBehaviour
{
    [Header("Character Sprites")]
    public Sprite playerSprite;
    public Sprite enemySprite;

    [Header("UI")]
    public GameObject battleUI;
    public TextMeshProUGUI battleText;
    public Image playerImageUI; // Изменено с GameObject на Image
    public Image enemyImageUI;  // Изменено с GameObject на Image

    public GameObject actionButtonsPanel;
    public GameObject itemSelectionPanel;
    public Transform itemsContainer;
    public GameObject itemSlotPrefab;
    public Button backButton;

    [Header("Character Data")]
    public PlayerCharacter playerCharacter;
    public Character enemyCharacter;

    private BattleState state;
    private List<GameObject> currentItemSlots = new List<GameObject>();



    public void StartFight()
    {
        Debug.Log("Начало драки");
        state = BattleState.Start;
        Time.timeScale = 0f;
        battleUI.SetActive(true);
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        // Устанавливаем спрайты для UI Images вместо инстанцирования префабов
        if (playerImageUI != null && playerSprite != null)
        {
            playerImageUI.sprite = playerSprite;
            playerImageUI.gameObject.SetActive(true);
        }

        if (enemyImageUI != null && enemySprite != null)
        {
            enemyImageUI.sprite = enemySprite;
            enemyImageUI.gameObject.SetActive(true);
        }

        battleText.text = "БИТВА НАЧИНАЕТСЯ!";

        yield return new WaitForSecondsRealtime(2f);

        state = BattleState.Player_Turn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("ХодИгрока");
        battleText.text = "Выберите действие:";

        if (actionButtonsPanel != null)
            actionButtonsPanel.SetActive(true);

        if (itemSelectionPanel != null)
            itemSelectionPanel.SetActive(false);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.Player_Turn) return;
        StartCoroutine(PlayerAttack());
    }

    public void OnMagicAttackButton()
    {
        if (state != BattleState.Player_Turn) return;
        StartCoroutine(PlayerMagicAttack());
    }

    public void OnUseItemButton()
    {
        if (state != BattleState.Player_Turn) return;

        state = BattleState.Player_UsingItem;
        ShowActiveItems();
    }

    IEnumerator PlayerAttack()
    {
        enemyCharacter.DecreaseCurrHP(playerCharacter.GetAtk());
        battleText.text = $"{playerCharacter.name} атакует!";
        yield return new WaitForSecondsRealtime(2f);

        CheckEnemyStatus();
    }

    IEnumerator PlayerMagicAttack()
    {
        int manaCost = 10;
        if (playerCharacter.GetCurrMana() >= manaCost)
        {
            playerCharacter.DecreaseCurrMana(manaCost);
            enemyCharacter.DecreaseCurrHP(playerCharacter.GetMagic());
            battleText.text = $"{playerCharacter.name} использует магическую атаку!";

            yield return new WaitForSecondsRealtime(2f);

            CheckEnemyStatus();
        }
        else
        {
            battleText.text = "Недостаточно маны для магической атаки!";
            yield return new WaitForSecondsRealtime(1.5f);

            PlayerTurn();
        }
    }

    void CheckEnemyStatus()
    {
        if (enemyCharacter.IsDead())
        {
            state = BattleState.Win;
            enemyCharacter.Death();
            EndBattle();
        }
        else
        {
            state = BattleState.Enemy_Turn;
            StartCoroutine(EnemyTurn());
        }
    }

    void ShowActiveItems()
    {
        battleText.text = "Выберите активный предмет:";

        if (actionButtonsPanel != null)
            actionButtonsPanel.SetActive(false);

        if (itemSelectionPanel != null)
            itemSelectionPanel.SetActive(true);

        ClearItemSlots();

        List<Item> activeItems = playerCharacter.inventory.FindListItem(ItemType.active);

        if (activeItems == null || activeItems.Count == 0)
        {
            GameObject emptySlot = Instantiate(itemSlotPrefab, itemsContainer);
            TextMeshProUGUI nameText = emptySlot.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = "Нет активных предметов";
            }

            Button useButton = emptySlot.transform.Find("UseButton")?.GetComponent<Button>();

            currentItemSlots.Add(emptySlot);
            return;
        }

        foreach (Item item in activeItems)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemsContainer);

            TextMeshProUGUI nameText = slot.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = slot.transform.Find("DescriptionText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rarityText = slot.transform.Find("RarityText")?.GetComponent<TextMeshProUGUI>();
            Button useButton = slot.transform.Find("UseButton")?.GetComponent<Button>();

            if (nameText != null)
                nameText.text = item.itemname;

            if (descriptionText != null)
                descriptionText.text = item.description;

            if (rarityText != null)
            {
                rarityText.text = item.rarity.ToString();
            }

            if (useButton != null)
            {
                useButton.onClick.AddListener(() => UseItemInBattle(item));

                if (!item.CanUse(playerCharacter))
                {
                    useButton.interactable = false;
                }
            }

            currentItemSlots.Add(slot);
        }
    }

    void UseItemInBattle(Item item)
    {
        if (state != BattleState.Player_UsingItem) return;

        item.Use(playerCharacter);

        InventoryUI inventoryUI = FindAnyObjectByType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventory();
        }

        StartCoroutine(AfterItemUse());
    }

    IEnumerator AfterItemUse()
    {
        battleText.text = $"{playerCharacter.name} использует предмет!";
        yield return new WaitForSecondsRealtime(1.5f);

        ClearItemSlots();

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

    public void OnBackButton()
    {
        if (state == BattleState.Player_UsingItem)
        {
            ClearItemSlots();
            if (itemSelectionPanel != null)
                itemSelectionPanel.SetActive(false);
            PlayerTurn();
        }
    }

    void ClearItemSlots()
    {
        foreach (GameObject slot in currentItemSlots)
        {
            if (slot != null)
                Destroy(slot);
        }
        currentItemSlots.Clear();
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Вражескийход");
        battleText.text = "Вражеский ход!";
        yield return new WaitForSecondsRealtime(1f);

        int typeAttack = Random.Range(0, 2);
        switch (typeAttack)
        {
            case 0:
                battleText.text = "Враг ударяет по вам!";
                playerCharacter.DecreaseCurrHP(enemyCharacter.GetAtk());
                break;
            case 1:
                battleText.text = "Враг использует магию по вам!";
                playerCharacter.DecreaseCurrHP(enemyCharacter.GetMagic());
                break;
        }

        yield return new WaitForSecondsRealtime(1f);

        if (playerCharacter.IsDead())
        {
            state = BattleState.Lose;
            EndBattle();
        }
        else
        {
            state = BattleState.Player_Turn;
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

        ClearItemSlots();

        if (actionButtonsPanel != null)
            actionButtonsPanel.SetActive(false);
        if (itemSelectionPanel != null)
            itemSelectionPanel.SetActive(false);

        StartCoroutine(CloseBattleUI());
    }

    IEnumerator CloseBattleUI()
    {
        yield return new WaitForSecondsRealtime(2f);
        battleUI.SetActive(false);
        Time.timeScale = 1f;
        if (state == BattleState.Lose)
        {
            playerCharacter.Death();
        }
    }

}
