using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoorTrigger : MonoBehaviour
{
    public bool isRestartDoor; // Галочка в Інспектора: рестарт чи вихід?
    [Header("Scene Settings")]
    public string menuSceneName = "SampleScene"; // Точна назва твоєї меню сцени

    // Цей метод спрацьовує автоматично, коли щось заходить всередину коллайдера
    private void OnTriggerEnter(Collider other)
    {
        // Перевіряємо, чи об'єкт, який зайшов — це гравець
        if (other.CompareTag("Player"))
        {
            if (isRestartDoor)
            {
                // Перезавантажуємо поточну сцену з нуля
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene(menuSceneName);
            }
        }
    }
}