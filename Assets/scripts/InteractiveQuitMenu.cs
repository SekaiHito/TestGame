using UnityEngine;

public class InteractiveQuitMenu : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject quitPanel; // Твоя панель підтвердження

    [Header("Player Settings")]
    public Transform playerTransform; // Капсула гравця
    public Transform stepBackPoint; // Порожній об'єкт ПЕРЕД дверима

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Показуємо Canvas
            quitPanel.SetActive(true);

            // 2. Зупиняємо гравця
            FirstPersonController playerController = playerTransform.GetComponent<FirstPersonController>();
            if (playerController != null) playerController.enabled = false;

            // 3. Звільняємо курсор миші для кнопок
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Метод для кнопки YES
    public void ConfirmQuit()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }

    // Метод для кнопки NO
    public void CancelQuit()
    {
        // 1. Ховаємо Canvas
        quitPanel.SetActive(false);

        // 2. Відкидаємо гравця назад, щоб він вийшов з тригера дверей
        CharacterController cc = playerTransform.GetComponent<CharacterController>();
        if (cc != null && stepBackPoint != null)
        {
            cc.enabled = false;
            playerTransform.position = stepBackPoint.position;
            cc.enabled = true;
        }

        // 3. Повертаємо керування гравцю
        FirstPersonController playerController = playerTransform.GetComponent<FirstPersonController>();
        if (playerController != null) playerController.enabled = true;

        // 4. Ховаємо курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}