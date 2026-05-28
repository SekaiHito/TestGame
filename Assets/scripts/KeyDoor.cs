// Скрипт: KeyDoor.cs
using UnityEngine;

public class KeyDoor : DoorLogic, Interactable
{
    [Header("What key needed?")]
    public string requiredItemID = "SilverKey";

    // Наша пам'ять: чи двері вже були відкриті ключем?
    private bool isUnlocked = false; 

    public void Interact()
    {
        // 1. Якщо двері вже розблоковані, вони працюють як звичайні (відкрий/закрий)
        if (isUnlocked)
        {
            ToggleDoor(); // Викликаємо наш перемикач з DoorLogic
            return; // Зупиняємо код, щоб він не шукав ключ знову
        }

        // 2. Якщо ще заблоковані - шукаємо ключ в інвентарі
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null && inventory.HasItem(requiredItemID))
        {
            isUnlocked = true; // Знімаємо замок назавжди!
            ToggleDoor(); // Відкриваємо двері
            Debug.Log("🔓 Двері розблоковано ключем: " + requiredItemID);
        }
        else
        {
            // Якщо ключа немає
            Debug.Log("🔒 Треба знайти: " + requiredItemID);
        }
    }

    public string GetPromptText()
    {
        // Текст підказки теж змінюється залежно від стану замка
        if (!isUnlocked)
        {
            return "Press E to unlock (Needs " + requiredItemID + ")";
        }
        else
        {
            // Якщо розблоковано, підказка як у звичайних дверей
            return isOpen ? "Press E to close door" : "Press E to open door";
        }
    }
}