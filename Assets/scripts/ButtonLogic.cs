using Unity.VisualScripting;
using UnityEngine;

public class ButtonLogic : MonoBehaviour, Interactable
{
    public DoorLogic myDoor;
    [Header("what key needed?")]
    public string requiredItemID = "SilverKey"; // Має ІДЕАЛЬНО збігатися з ID ключа!
    public void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null && inventory.HasItem(requiredItemID))
        if (myDoor != null)
        {
            myDoor.OpenDoor();
        }
        else
        {
            Debug.Log("You need the " + requiredItemID + " to open this door.");
        }
    }

    public string GetPromptText()
    {
        return "Press E to use key";
    }
}
