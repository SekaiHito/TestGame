using UnityEngine;

public class NormalDoor : DoorLogic, Interactable
{
    public void Interact()
    {
        ToggleDoor(); // Тепер викликаємо перемикач замість OpenDoor()
    }

    public string GetPromptText()
    {
        // Якщо відкриті - пишемо "закрити", інакше "відкрити"
        return isOpen ? "Press E to close door" : "Press E to open door"; 
    }
}