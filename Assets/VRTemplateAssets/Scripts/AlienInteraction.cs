using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInteraction : MonoBehaviour
{
    [Header("Dialogue")]
    public string[] dialogueLines; // Lines this alien will say

    [Header("Interaction Settings")]
    public float dialogueHeight = 2f; // How high above the alien the dialogue appears

    private DialogueUI dialogueUI; // Reference to your Dialogue UI script

    void Start()
    {
        // Find the DialogueUI that already exists in your scene
        dialogueUI = FindObjectOfType<DialogueUI>();
        if (dialogueUI == null)
        {
            Debug.LogError("No DialogueUI found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogueUI != null)
        {
            // Move dialogue UI above this alien
            dialogueUI.transform.position = transform.position + Vector3.up * dialogueHeight;

            // make the canvas face the same way the alien is facing
            dialogueUI.transform.rotation = Quaternion.LookRotation(transform.forward);

            // Start the dialogue with this alien's lines
            dialogueUI.StartDialogue(dialogueLines);
        }
    }
}

