using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private PlayerInventory playerInventory;

    [SerializeField] public GameObject itemsContainer;

    [Header("Панели для разных типов предметов")]
    [SerializeField] private Transform activeItemsContainer;
    [SerializeField] private Transform passiveItemsContainer;
    [SerializeField] private Transform questItemsContainer;

    [Header("Префаб слота предмета")]
    [SerializeField] private GameObject itemSlotPrefab;

    void Start()
    {
        if (player != null)
        {
            playerInventory = player.inventory;
        }
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        // Очищаем все контейнеры
        ClearContainer(activeItemsContainer);
        ClearContainer(passiveItemsContainer);
        ClearContainer(questItemsContainer);

        // Показываем предметы по категориям
        ShowItems(playerInventory.FindListItem(ItemType.active), activeItemsContainer, true);
        ShowItems(playerInventory.FindListItem(ItemType.passive), passiveItemsContainer, false);
        ShowItems(playerInventory.FindListItem(ItemType.questItem), questItemsContainer, false);
    }

    void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    void ShowItems(List<Item> items, Transform container, bool isActiveItem)
    {
        if (items == null) return;

        foreach (Item item in items)
        {
            // Создаем слот в нужном контейнере
            GameObject slot = Instantiate(itemSlotPrefab, container);

            // Находим UI элементы
            TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = slot.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rarityText = slot.transform.Find("RarityText").GetComponent<TextMeshProUGUI>();
            Button dropButton = slot.transform.Find("DropButton").GetComponent<Button>();

            // Заполняем информацию
            nameText.text = item.itemname;
            if (item.type == ItemType.active || item.type == ItemType.passive)
            {
                descriptionText.text = $"{item.description} | {item.upgrade}";
            }
            else
            {
                descriptionText.text = item.description;
            }

            rarityText.text = item.rarity.ToString();

            // Меняем цвет редкости
            SetRarityColor(rarityText, item.rarity);

            // Настраиваем кнопку выброса
            dropButton.onClick.AddListener(() => DropItem(item));

            // Для активных предметов добавляем кнопку "Использовать"
            if (isActiveItem)
            {
                Button useButton = slot.transform.Find("UseButton").GetComponent<Button>();
                useButton.gameObject.SetActive(true);
                useButton.onClick.AddListener(() => UseItem(item));
            }
        }
    }

    void SetRarityColor(TextMeshProUGUI text, Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                text.color = Color.white;
                break;
            case Rarity.UnCommon:
                text.color = Color.green;
                break;
            case Rarity.Rare:
                text.color = Color.blue;
                break;
            case Rarity.Epic:
                text.color = Color.magenta;
                break;
            case Rarity.Legendary:
                text.color = Color.yellow;
                break;
        }
    }

    void DropItem(Item item)
    {
        // Вызываем существующий метод из инвентаря
        player.DropItem(item);

        // Обновляем UI
        UpdateInventory();
    }

    void UseItem(Item item)
    {
        // Проверяем, можно ли использовать предмет
        if (item.CanUse(playerInventory.player))
        {
            playerInventory.UseItem(item);
            UpdateInventory();
        }
    }

}
