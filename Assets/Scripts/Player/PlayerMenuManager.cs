using NUnit.Framework;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    private bool isActiveInventoryUI;

    void Update()
    {
        SetActiveInventoryUI();
    }

    private void SetActiveInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            inventoryUI.itemsContainer.SetActive(!isActiveInventoryUI);
            isActiveInventoryUI = !isActiveInventoryUI;

            if (isActiveInventoryUI)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
