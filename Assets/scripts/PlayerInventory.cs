using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<string> collectedItems = new List<string>();
    public List<string> collectedNotes = new List<string>(); // Список для нотаток

    public GameObject inventoryPanel;
    public TextMeshProUGUI itemText;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventory();
    }

    void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);
        
        if (inventoryPanel != null)
        {
            if (!isActive)
            {
                UpdateInventoryUI();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void AddItem(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            UpdateInventoryUI();
        }
    }

    // Базовий метод додавання нотатки
    public void AddNote(string noteContent)
    {
        if (!collectedNotes.Contains(noteContent))
        {
            collectedNotes.Add(noteContent);
        }
    }

    public void UpdateInventoryUI()
    {
        itemText.text = "Знайдені речі:\n";
        foreach (string item in collectedItems) itemText.text += "- " + item + "\n";
        
        itemText.text += "\nЗаписки:\n";
        foreach (string note in collectedNotes) itemText.text += note + "\n\n";
    }

    public bool HasItem(string itemName) => collectedItems.Contains(itemName);
}