using UnityEngine;

public class NoteItem : MonoBehaviour, Interactable
{
    [TextArea(5, 10)]
    public string noteText;

    public void Interact()
    {
        // 1. Додаємо в інвентар
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddNote(noteText);
        }

        // 2. Виводимо на екран
        NoteViewer viewer = FindFirstObjectByType<NoteViewer>();
        if (viewer != null)
        {
            viewer.ShowNote(noteText);
        }

        // 3. Знищуємо об'єкт зі сцени
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Press E to read note";
    }
}