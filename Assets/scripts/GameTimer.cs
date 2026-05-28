using UnityEngine;
using System.Collections; // Обов'язково для корутин
using TMPro;
using UnityEngine.Video; // Обов'язково для роботи з відео

public class GameTimer : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource bgmSource; // Перетягни сюди GameManager
    public AudioClip tensionMusic; // Трек для останніх 30 секунд
    public AudioClip limboMusic; // Трек для кімнати програшу
    
    private bool tensionStarted = false; // Запобіжник
    public float timeRemaining = 120f; 
    public TextMeshProUGUI timerText; 

    [Header("Rickroll UI")]
    public GameObject jumpscareUI; 
    public VideoPlayer videoPlayer; 

    [Header("Teleport To Menu")]
    public Transform playerTransform; 
    public Transform endRoomSpawnPoint; 

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            // НОВА ЛОГІКА: Вмикаємо напружену музику на 30 секундах
            if (timeRemaining <= 30f && !tensionStarted)
            {
                tensionStarted = true; // Записуємо, що музика вже змінилася
                if (bgmSource != null && tensionMusic != null)
                {
                    bgmSource.clip = tensionMusic;
                    bgmSource.Play();
                }
            }
        }
        else
        {
            StartCoroutine(FinishGameRoutine());
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        if (timerText != null) timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Сценарій фіналу
    IEnumerator FinishGameRoutine()
    {
        isGameOver = true;

        // 1. Вимикаємо керування (щоб гравець не бігав під час відео)
        FirstPersonController playerController = FindObjectOfType<FirstPersonController>();
        if (playerController != null) playerController.enabled = false;

        // 2. Вмикаємо екран із Рікролом
        if (jumpscareUI != null) jumpscareUI.SetActive(true);

        // 3. Динамічно дізнаємося довжину відео і чекаємо, поки воно дограє до кінця
        if (videoPlayer != null)
        {
            float videoLength = (float)videoPlayer.length;
            yield return new WaitForSeconds(videoLength); // Пауза на тривалість кліпу
        }
        else
        {
            // Якщо раптом відео немає — просто почекаємо 5 секунд про всяк випадок
            yield return new WaitForSeconds(5f);
        }

        // 4. ТЕЛЕПОРТАЦІЯ: Тимчасово вимикаємо капсулу фізики, міняємо позицію, вмикаємо назад
        CharacterController cc = playerTransform.GetComponent<CharacterController>();
        if (cc != null && endRoomSpawnPoint != null)
        {
            cc.enabled = false;
            playerTransform.position = endRoomSpawnPoint.position;
            cc.enabled = true;
        }
        
        if (bgmSource != null && limboMusic != null)
        {
            bgmSource.clip = limboMusic;
            bgmSource.Play();
        }

        // 5. Вимикаємо відео, щоб гравець знову бачив світ (але вже нову кімнату)
        if (jumpscareUI != null) jumpscareUI.SetActive(false);

        // 6. Повертаємо гравцеві керування, щоб він міг підійти і зайти в двері-трігери
        if (playerController != null) playerController.enabled = true;
    }
}