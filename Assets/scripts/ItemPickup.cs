using UnityEngine;

public class ItemPickup : MonoBehaviour, Interactable
{
    [Header("what item?")]
    public string itemName = "SilverKey";
    public void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(itemName);
            Destroy(gameObject);
        }
    }

    public string GetPromptText()
    {
        return "Press E to pick up " + itemName;
    }
}
