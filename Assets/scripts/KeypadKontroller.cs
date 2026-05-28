using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class KeypadKontroller : MonoBehaviour
{
    [Header("Keypad Settings")]
    public string correctCode = "14234"; // The correct code to unlock
    public DoorLogic doorToUnlock; // Reference to the door that will be unlocked

    [Header("Links")]
    public TextMeshProUGUI codeDisplay; // UI element to display the entered code
    public GameObject keypadPanel; // The UI panel for the keypad
    private string CurrentInput = ""; // The code currently entered by the player
    void Start()
    {
        ClearInput();
    }

    public void AddDigit(string digit)
    {
        if (CurrentInput.Length < correctCode.Length)
        {
            CurrentInput += digit;
            codeDisplay.text = CurrentInput;
        }
    }
    public void CheckCode()
    {
        if (CurrentInput == correctCode)
        {
            codeDisplay.text = "CORRECT!";
            codeDisplay.color = Color.green;
            if (doorToUnlock != null)
            {
                doorToUnlock.OpenDoor();
            }
            Invoke("CloseKeypad", 1f); // Close the keypad after a short delay
        }
        else
        {
            codeDisplay.text = "INCORRECT!";
            codeDisplay.color = Color.red;
            CurrentInput = "";
            Invoke("ClearInput", 1f); // Clear the input after a short delay
        }
    }
    public void ClearInput()
    {
        CurrentInput = "";
        RersetDisplay();
    }
    public void RersetDisplay()
    {
        codeDisplay.text = "ENTER CODE";
        codeDisplay.color = Color.white;
    }
    public void CloseKeypad()
    {
        if (keypadPanel != null)
        {
            keypadPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // Ховаємо курсор
            Cursor.visible = false;
            ClearInput();
        }
    }
    

}