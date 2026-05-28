using UnityEngine;
using TMPro;

public class NoteViewer : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject notePanel; // UI панель для читання
    public TextMeshProUGUI noteTextUI; // Текст на панелі

    private FirstPersonController playerController;

    void Start()
    {
        playerController = FindObjectOfType<FirstPersonController>();
    }

    public void ShowNote(string text)
    {
        noteTextUI.text = text;
        notePanel.SetActive(true);
        
        // Вимикаємо керування та показуємо курсор
        if (playerController != null) playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Цей метод потрібно викликати кнопкою "Закрити" на UI панелі
    public void CloseNote()
    {
        notePanel.SetActive(false);
        
        // Вмикаємо керування та ховаємо курсор
        if (playerController != null) playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}