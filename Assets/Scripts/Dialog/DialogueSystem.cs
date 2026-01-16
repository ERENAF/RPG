using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueTree dialogueTree;
    [SerializeField] private GameObject dialogueUI;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterImage;

    // ЗАМЕНА: Transform на ScrollRect и RectTransform
    [SerializeField] private ScrollRect optionsScrollRect; // Компонент ScrollView
    [SerializeField] private RectTransform optionsContainer; // Content внутри ScrollView
    [SerializeField] private GameObject optionButtonPrefab;

    [Header("ScrollView Settings")]
    [SerializeField] private bool autoScrollToTop = true;
    [SerializeField] private float scrollSpeed = 25f;

    private DialogueNode currentNode;
    private Dictionary<string, int> gameVariables = new Dictionary<string, int>();

    void Start()
    {
        if (dialogueTree != null)
        {
            if (dialogueTree.startNodeID != "") StartDialogue(dialogueTree.startNodeID);
        }
    }

    public void StartDialogue(string startNodeID)
    {
        dialogueUI.SetActive(true);
        currentNode = dialogueTree.GetNode(startNodeID);
        DisplayCurrentNode();
    }

    private void DisplayCurrentNode()
    {
        if (currentNode == null) return;

    // Обновляем UI текущего узла
        characterNameText.text = currentNode.charName;
        dialogueText.text = currentNode.line;
        characterImage.sprite = currentNode.characterSprite;

    // Очищаем старые варианты ответов
        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

    // Сбрасываем позицию контейнера (ВАЖНО!)
        if (optionsContainer is RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
        }

    // Создаем кнопки для доступных вариантов
        bool hasOptions = false;
        foreach (var option in currentNode.options)
        {
            if (IsOptionAvailable(option))
            {
                CreateOptionButton(option);
                hasOptions = true;
            }
        }

    // Если нет вариантов - добавляем кнопку "Продолжить"
        if (!hasOptions || currentNode.options.Count == 0)
        {
            CreateContinueButton();
        }

    // Принудительное обновление Layout (ИСПРАВЛЕНО)
        Canvas.ForceUpdateCanvases();
        if (optionsContainer.TryGetComponent<VerticalLayoutGroup>(out var layoutGroup))
        {
            layoutGroup.CalculateLayoutInputVertical();
            layoutGroup.SetLayoutVertical();
        }

        if (optionsContainer.parent != null && optionsContainer.parent.parent != null)
        {
            var scrollRect = optionsContainer.parent.parent.GetComponent<ScrollRect>();
            if (scrollRect != null)
            {
        // Прокручиваем к началу
                scrollRect.verticalNormalizedPosition = 1f;

        // Включаем/выключаем скролл в зависимости от количества кнопок
                float contentHeight = optionsContainer.childCount * 45f; // 40 + 5 spacing
                float viewportHeight = scrollRect.viewport.rect.height;
                scrollRect.vertical = contentHeight > viewportHeight;
            }
        }
    }

    private void ClearOptions()
    {
        if (optionsContainer == null) return;

        // Удаляем все дочерние объекты
        for (int i = optionsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(optionsContainer.GetChild(i).gameObject);
        }
    }

    private void CreateOptionButton(DialogueOption option)
    {
        GameObject optionButton = Instantiate(optionButtonPrefab);

    // УСТАНАВЛИВАЕМ ПРАВИЛЬНОГО РОДИТЕЛЯ (ВАЖНО!)
    optionButton.transform.SetParent(optionsContainer, false);

    // СБРАСЫВАЕМ TRANSFORM КНОПКИ
    RectTransform buttonRect = optionButton.GetComponent<RectTransform>();
    if (buttonRect != null)
    {
        buttonRect.localScale = Vector3.one;
        buttonRect.localPosition = Vector3.zero;
        buttonRect.anchoredPosition = Vector3.zero;
        buttonRect.sizeDelta = new Vector2(buttonRect.sizeDelta.x, 40f);
    }

    // Настраиваем текст
    Text buttonText = optionButton.GetComponentInChildren<Text>();
    if (buttonText != null)
    {
        buttonText.text = option.text;
    }

    // Настраиваем кнопку
    Button button = optionButton.GetComponent<Button>();
    if (button != null)
    {
        // Копируем значение для замыкания
        string targetNode = option.nextNodeID;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectOption(targetNode));
    }
    }

    private void CreateContinueButton()
    {
        GameObject continueButton = Instantiate(optionButtonPrefab);

    // Устанавливаем правильного родителя
        continueButton.transform.SetParent(optionsContainer, false);

    // Сбрасываем transform
        RectTransform buttonRect = continueButton.GetComponent<RectTransform>();
        if (buttonRect != null)
        {
            buttonRect.localScale = Vector3.one;
            buttonRect.localPosition = Vector3.zero;
            buttonRect.anchoredPosition = Vector3.zero;
            buttonRect.sizeDelta = new Vector2(buttonRect.sizeDelta.x, 40f);
        }

        Text buttonText = continueButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = "Продолжить";
        }

        Button button = continueButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(EndDialogue);
        }
    }

    private bool IsOptionAvailable(DialogueOption option)
    {
        if (!option.isAvailable) return false;

        foreach (var condition in option.conditions)
        {
            if (!CheckCondition(condition)) return false;
        }
        return true;
    }

    private bool CheckCondition(Condition condition)
    {
        if (!gameVariables.ContainsKey(condition.variableName))
            return false;

        int value = gameVariables[condition.variableName];

        return condition.comparisonType switch
        {
            ComparisonType.Equals => value == condition.requiredValue,
            ComparisonType.GreaterThan => value > condition.requiredValue,
            ComparisonType.LessThan => value < condition.requiredValue,
            _ => false
        };
    }

    public void SelectOption(string targetNodeID)
    {
        if (targetNodeID == "END")
        {
            EndDialogue();
            return;
        }

        currentNode = dialogueTree.GetNode(targetNodeID);
        DisplayCurrentNode();
    }

    public void SetVariable(string variableName, int value)
    {
        gameVariables[variableName] = value;
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        Debug.Log("Диалог завершен");
    }

    // Дополнительный метод для ручного скроллинга
    public void ScrollOptions(float direction)
    {
        if (optionsScrollRect != null)
        {
            float currentPos = optionsScrollRect.verticalNormalizedPosition;
            float newPos = Mathf.Clamp01(currentPos + direction * scrollSpeed * Time.deltaTime);
            optionsScrollRect.verticalNormalizedPosition = newPos;
        }
    }

}
