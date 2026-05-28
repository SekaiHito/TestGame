using UnityEngine;

public class DiggableObject : MonoBehaviour, Interactable
{
    [Header("Hidden Item")]
    public GameObject hiddenItem; // Сюди перетягнути ключ
    
    private bool isDug = false;

    public void Interact()
    {
        if (!isDug && hiddenItem != null)
        {
            hiddenItem.SetActive(true); // Вмикаємо ключ
            isDug = true; // Запобігаємо повторному викопуванню
        }
    }

    public string GetPromptText()
    {
        return isDug ? "" : "Press E to dig";
    }
}