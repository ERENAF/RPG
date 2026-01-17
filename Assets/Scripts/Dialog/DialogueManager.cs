using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using TMPro;
using Unity;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueTree dialogueTree;
    [SerializeField] private GameObject dialoguePanel;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject[] optionButtons;

    private DialogueNode currentNode;


    public void StartDialogue(DialogueTree newDialogueTree)
    {
        dialogueTree = newDialogueTree;
        if (dialogueTree == null)
        {
            Debug.LogError("dialogueTree is missing!");
            return;
        }

        dialoguePanel.SetActive(true);
        currentNode = dialogueTree.GetNode(dialogueTree.startNodeID);
        Time.timeScale = 0;
        DisplayCurrentNode();
    }

    private void DisplayCurrentNode()
    {
        if (currentNode == null) return;

        characterNameText.text = (currentNode.characterName!= null)?currentNode.characterName:"неизвестно";
        dialogueText.text = currentNode.dialogueLine;
        characterImage.sprite = currentNode.characterImage;

        DisplayOptions();
    }

    private void DisplayOptions()
    {
        foreach (var optionButton in optionButtons)
        {
            optionButton.SetActive(false);
        }

        for (int i = 0; i < currentNode.options.Count(); i++)
        {
            optionButtons[i].SetActive(true);
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.options[i].dialogueLine;

            Button button = optionButtons[i].GetComponent<Button>();
            string targetNode = currentNode.options[i].OptionUse();
            button.onClick.AddListener(()=> SelectOption(targetNode));
        }

        if (currentNode.options.Count() <= 0)
        {
            Debug.Log("продолжить кнопка");
            optionButtons[0].SetActive(true);
            if (currentNode.nextNodeID != "END")
            {
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Продолжить";
            }
            else
            {
                optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Закончить диалог";
            }
            optionButtons[0].GetComponent<Button>().onClick.AddListener(() => SelectOption(currentNode.nextNodeID));
        }
    }

    private void SelectOption(string targetNodeID)
    {
        if (targetNodeID == "END")
        {
            EndDialogue();
            return;
        }
        currentNode = dialogueTree.GetNode(targetNodeID);
        DisplayCurrentNode();
    }

    private void EndDialogue()
    {
        Time.timeScale = 1;
        dialoguePanel.SetActive(false);
        Debug.Log("Диалог завершен");
    }

}
