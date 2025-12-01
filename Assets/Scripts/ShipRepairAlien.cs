using UnityEngine;

public class ShipRepairAlien : MonoBehaviour
{
    [Header("UI Position")]
    public float dialogueHeight = 2f;

    [Header("References")]
    private RepairManager repairUI;

    void Start()
    {
        repairUI = FindObjectOfType<RepairManager>();

        if (repairUI == null)
            Debug.LogError("No RepairManager found in the scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && repairUI != null)
        {
            // Move REPAIR UI above this alien
            repairUI.transform.position = transform.position + Vector3.up * dialogueHeight;

            // Optional: rotate to same direction as alien
            repairUI.transform.rotation = Quaternion.LookRotation(transform.forward);

            // Show repair options
            repairUI.ShowOptions();
        }
    }

    public void OnInstructionsPressed()
    {
        repairUI.ShowText(
            "Explore the planet and collect energy crystals. Each one restores your ship."
        );
    }

    public void OnExchangePressed()
    {
        if (CrystalManager.Instance.HasCrystal(1))
        {
            ShipRepairManager.Instance.RepairShip();
            repairUI.ShowText("The ship absorbs the crystal's energy.");
        }
        else
        {
            repairUI.ShowText("You don’t have any crystals.");
        }
    }

}
