using UnityEngine;

public class KeyPadInteract : MonoBehaviour, Interactable
{
    public GameObject keyPadUI;
    public void Interact()
    {
        if (keyPadUI != null)
        {
            keyPadUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public string GetPromptText()
    {
        return "Press E to use the keypad";
    }
}
