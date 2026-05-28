using UnityEngine;

public class FinalDoor : DoorLogic
{
    [Header("Victory Menu Settings")]
    public Transform playerTransform; // Твій гравець (капсула)
    public Transform endRoomSpawnPoint; // Точка спавну в таємній кімнаті меню
    public GameTimer gameTimer; // Посилання на таймер
    [Header("Audio Settings")]
    public AudioSource bgmSource; // Той самий GameManager
    public AudioClip victoryMusic; 

    public override void OpenDoor()
    {
        base.OpenDoor(); // Запускає анімацію відкриття з DoorLogic

        // Робимо двері тригером, щоб через них можна було пройти
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Спрацьовує тільки якщо двері вже відкриті і туди зайшов гравець
        if (isOpen && other.CompareTag("Player"))
        {
            Debug.Log("🎉 ПЕРЕМОГА! Телепортація в меню...");

            // 1. Зупиняємо таймер (щоб не спрацював скример)
            if (gameTimer != null)
            {
                gameTimer.enabled = false;
            }

            // 2. Телепортуємо гравця в кімнату вибору
            CharacterController cc = playerTransform.GetComponent<CharacterController>();
            if (cc != null && endRoomSpawnPoint != null)
            {
                cc.enabled = false; // Вимикаємо фізику для телепорту
                playerTransform.position = endRoomSpawnPoint.position; // Телепортуємо
                cc.enabled = true; // Вмикаємо фізику назад
            }
            if (bgmSource != null && victoryMusic != null)
            {
                bgmSource.clip = victoryMusic;
                bgmSource.Play();
            }
        }
    }
}