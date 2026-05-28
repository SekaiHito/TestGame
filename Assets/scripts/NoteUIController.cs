using UnityEngine;

public class NoteUIController : MonoBehaviour
{
    public GameObject notePanel; // Сама панель

    // Цю функцію буде викликати хрестик
    public void CloseNote()
    {
        notePanel.SetActive(false);
        
        // НАЙГОЛОВНІШЕ: Повертаємо курсор грі, щоб гравець міг ходити!
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
}