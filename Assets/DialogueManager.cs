using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialogueBubblePrefab;
    public float dialogueDuration = 4f;

    private GameObject activeBubble;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(string[] lines, Transform alienTransform)
    {
        if (dialogueBubblePrefab == null) return;

        // Destroy existing bubble if one is active
        if (activeBubble != null)
            Destroy(activeBubble);

        // Spawn dialogue bubble above the alien
        activeBubble = Instantiate(dialogueBubblePrefab, alienTransform.position + Vector3.up * 2f, Quaternion.identity);

        // --- Assign Event Camera for World Space Canvas ---
        Canvas canvas = activeBubble.GetComponentInChildren<Canvas>();
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            Camera xrCam = Camera.main;
            if (xrCam == null)
                xrCam = FindObjectOfType<Camera>(); // fallback if MainCamera tag missing

            if (xrCam != null)
                canvas.worldCamera = xrCam; // ensures text actually renders
            else
                Debug.LogWarning("DialogueManager: No camera found for dialogue canvas!");
        }

        // --- Make the bubble face the player's headset ---
        if (Camera.main != null)
            activeBubble.transform.LookAt(Camera.main.transform);

        // --- Set the dialogue text ---
        TextMeshProUGUI textComponent = activeBubble.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = lines[Random.Range(0, lines.Length)];
            textComponent.ForceMeshUpdate(); // force TMP to refresh mesh immediately
        }
        else
        {
            Debug.LogWarning("DialogueManager: TextMeshProUGUI not found in dialogue bubble prefab!");
        }

        // --- Hide bubble after delay ---
        StartCoroutine(HideBubbleAfterTime());
    }

    private IEnumerator HideBubbleAfterTime()
    {
        yield return new WaitForSeconds(dialogueDuration);
        if (activeBubble != null)
            Destroy(activeBubble);
    }
}
