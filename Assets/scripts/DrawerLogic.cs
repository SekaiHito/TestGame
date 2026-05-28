using UnityEngine;

public class DrawerLogic : DoorLogic, Interactable
{
    [Header("Drawer Settings")]
    // Вісь і відстань, на яку виїжджає шухляда (спробуй покрутити X, Y або Z)
    public Vector3 openOffset = new Vector3(0, 0, 1.35f); 
    
    private Vector3 closedPos;
    private Vector3 openPos;

    protected override void Start()
    {
        // Не викликаємо base.Start(), бо нам не треба рахувати градуси для дверей
        closedPos = transform.localPosition;
        openPos = closedPos + openOffset;
    }

    protected override void Update()
    {
        // Не викликаємо base.Update(), щоб шухляда не почала крутитися як двері
        
        // Lerp плавно рухає об'єкт між двома точками
        if (isOpen)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, Time.deltaTime * openSpeed);
        }
        else
        {
            // Бонус: шухляду можна буде закрити назад!
            transform.localPosition = Vector3.Lerp(transform.localPosition, closedPos, Time.deltaTime * openSpeed);
        }
    }

    public void Interact()
    {
        // Перемикач: якщо відкрита - закриваємо, якщо закрита - відкриваємо
        isOpen = !isOpen; 
    }

    public string GetPromptText()
    {
        return isOpen ? "Press E to close" : "Press E to open";
    }
}