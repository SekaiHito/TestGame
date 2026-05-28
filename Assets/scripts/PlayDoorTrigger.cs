using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDoorTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "MainScene"; // Точна назва твоєї ігрової сцени

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}