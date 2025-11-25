using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Manages the InventoryGrid by listening for an input action.
/// When the action is performed, it checks all child XRSocketInteractors (slots).
/// If a slot is filled, it deactivates the item and the slot itself.
/// </summary>
public class InventoryToggle : MonoBehaviour
{
    [Header("Input Configuration")]
    [Tooltip("The input action that will trigger the clearing of the inventory slots.")]
    [SerializeField]
    private InputActionReference clearInventoryActionReference;

    // A cached list of all XRSocketInteractors (slots) that are children of this GameObject.
    private List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor> inventorySlots;
    private InputAction clearInventoryAction;

    private void Awake()
    {
        // 1. Initialize Input Action
        if (clearInventoryActionReference != null)
        {
            clearInventoryAction = clearInventoryActionReference.action;
        }
        else
        {
            Debug.LogError("Clear Inventory Action Reference is null on " + gameObject.name);
        }

        // 2. Find all Slot Interactors in children (assuming slots are children of this object)
        inventorySlots = GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>().ToList();

        if (inventorySlots.Count == 0)
        {
            Debug.LogWarning("Found no XRSocketInteractor components (slots) as children of " + gameObject.name);
        }
        else if (inventorySlots.Count != 16)
        {
            Debug.LogWarning($"Expected 16 slots, but found {inventorySlots.Count} slots.");
        }
    }

    private void OnEnable()
    {
        // Subscribe to the input action event
        if (clearInventoryAction != null)
        {
            clearInventoryAction.performed += OnClearInventoryPerformed;
            clearInventoryAction.Enable();
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the input action event
        if (clearInventoryAction != null)
        {
            clearInventoryAction.performed -= OnClearInventoryPerformed;
            clearInventoryAction.Disable();
        }
    }

    /// <summary>
    /// Event handler for when the input action is triggered.
    /// </summary>
    /// <param name="context">The context of the input action.</param>
    private void OnClearInventoryPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Clear Inventory action triggered. Processing slots...");
        ProcessSlots();
    }

    /// <summary>
    /// Iterates through all inventory slots, checks if they are filled, and if so,
    /// deactivates both the item and the slot GameObject.
    /// </summary>
    private void ProcessSlots()
    {
        int itemsCleared = 0;

        foreach (var slot in inventorySlots)
        {
            // Use 'hasSelection' to check if the slot is currently holding an item (modern API).
            if (slot.hasSelection)
            {
                // CORRECTED: Use 'interactablesSelected[0]' to get the selected item. 
                // XRSocketInteractor is single-selection, so index 0 is safe.
                UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable selectedItem = slot.interactablesSelected[0];

                // Get the item's GameObject
                // We access the transform from the IXRSelectInteractable interface.
                GameObject itemObject = selectedItem.transform.gameObject;

                // 1. Make the item disappear (set inactive)
                itemObject.SetActive(false);
                Debug.Log($"Cleared item: {itemObject.name}");

                // 2. Make the slot disappear (set inactive)
                // Note: The socket interactor's GameObject is the slot itself
                slot.gameObject.SetActive(false);
                Debug.Log($"Cleared slot: {slot.gameObject.name}");

                itemsCleared++;
            }
        }

        if (itemsCleared > 0)
        {
            Debug.Log($"Successfully cleared {itemsCleared} item(s) and their respective slots.");
        }
        else
        {
            Debug.Log("Inventory was empty. Nothing to clear.");
        }
    }
}