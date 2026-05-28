using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PuzzleManager : MonoBehaviour
{
    [Header("Наш замок")]
    public KeypadKontroller keypad;

    [Header("Чотири записки")]
    public NoteItem note1;
    public NoteItem note2;
    public NoteItem note3;
    public NoteItem note4;

    void Start()
    {
        GenerateRandomCode();
    }

    void GenerateRandomCode()
    {
       // 1. Створюємо "мішок" з цифрами від 0 до 9
        List<int> availableDigits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] chosenDigits = new int[4];
        string finalCode = "";

        // 2. Витягуємо 4 унікальні цифри
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, availableDigits.Count);
            chosenDigits[i] = availableDigits[randomIndex];
            finalCode += chosenDigits[i].ToString();
            
            // Видаляємо цифру з мішка, щоб вона більше не випала
            availableDigits.RemoveAt(randomIndex); 
        }

        // 3. Відправляємо згенерований код у замок
        if (keypad != null)
        {
            keypad.correctCode = finalCode;
        }

        // 4. Роздаємо текст по записках (можеш змінити текст на свій смак!)
        if (note1 != null) note1.noteText = "Я ледве згадав першу цифру пароля. Здається, це " + chosenDigits[0] + ".";
        if (note2 != null) note2.noteText = "Хтось подряпав стіну біля сейфа... Друга цифра точно " + chosenDigits[1] + ".";
        if (note3 != null) note3.noteText = "Нагадування: третя цифра коду — " + chosenDigits[2] + ". Не забути!";
        if (note4 != null) note4.noteText = "Остання цифра — " + chosenDigits[3] + ". Головне тепер знайти перші три.";
        
        Debug.Log("🤫 Згенерований пароль: " + finalCode);
    }
}