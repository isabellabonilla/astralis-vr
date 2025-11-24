using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    private string[] lines;
    private int index;

    public void StartDialogue(string[] newLines)
    {
        lines = newLines;
        index = 0;
        ShowLine();

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextLine);
    }

    void ShowLine()
    {
        if (lines != null && index < lines.Length)
            dialogueText.text = lines[index];
    }

    void NextLine()
    {
        index++;
        if (index >= lines.Length)
            gameObject.SetActive(false);
        else
            ShowLine();
    }
}

