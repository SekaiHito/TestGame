using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f; 
    public float openSpeed = 3f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    protected virtual void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0); 
    }

    protected virtual void Update()
    {
        if (isOpen)
        {
            // Відкриваємо
            transform.localRotation = Quaternion.Slerp(transform.localRotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            // Закриваємо!
            transform.localRotation = Quaternion.Slerp(transform.localRotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    // Новий метод-перемикач для звичайних дверей
    public virtual void ToggleDoor()
    {
        isOpen = !isOpen;
    }

    // Залишаємо старий метод для кодового замка (який вміє тільки відкривати)
    public virtual void OpenDoor()
    {
        isOpen = true;
    }
}