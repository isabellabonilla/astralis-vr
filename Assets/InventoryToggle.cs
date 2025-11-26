using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InventoryToggle : MonoBehaviour
{
    [Header("Assign visuals here")]
    public GameObject bg;
    public GameObject[] slots;

    [Header("Input Action")]
    public InputActionReference toggleAction;

    private bool visible = true;

    private void OnEnable()
    {
        toggleAction.action.performed += OnToggle;
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= OnToggle;
    }

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        visible = !visible;

        // Toggle BG image
        var bgImage = bg.GetComponent<UnityEngine.UI.Image>();
        if (bgImage != null)
            bgImage.enabled = visible;

        // Handle all slots
        foreach (var slot in slots)
        {
            if (slot == null) continue;

            // Toggle Slot Image
            var img = slot.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
                img.enabled = visible;

            // Get the socket
            UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket = slot.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
            if (socket == null) continue;

            // Check if something is attached
            if (socket.interactablesSelected.Count == 0)
                continue;

            // Get the attached interactable
            UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = socket.interactablesSelected[0];
            if (interactable == null) continue;

            // Get the actual GameObject
            Transform item = interactable.transform;

            // Disable/enable mesh renderers on the item
            MeshRenderer[] renderers = item.GetComponentsInChildren<MeshRenderer>();
            foreach (var r in renderers)
                r.enabled = visible;
        }
    }
}
