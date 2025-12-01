using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class RepairManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject backButton;
    public TextMeshProUGUI messageText;

    private void Start()
    {
        optionsPanel.SetActive(false);
        backButton.SetActive(false);   // VERY important
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        backButton.SetActive(false);   // hide back when options appear
        messageText.text = "How can I help repair your ship?";
    }

    public void ShowText(string message)
    {
        optionsPanel.SetActive(false); // hide options
        backButton.SetActive(true);    // show back
        messageText.text = message;
    }

    public void ShowOptionsFromBack()
    {
        ShowOptions();   // reuse, cleaner
    }
}
