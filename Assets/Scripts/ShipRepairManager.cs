using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipRepairManager : MonoBehaviour
{
    public static ShipRepairManager Instance;

    [Header("Bar Settings")]
    public GameObject[] repairBars;
    public int barsPerCrystal = 10;

    [Header("Ship Objects")]
    public GameObject crashedShip;
    public GameObject repairedShip;

    private int repairLevel = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("=== ShipRepairManager START ===");

        if (repairBars == null || repairBars.Length == 0)
        {
            Debug.LogError("❌ NO REPAIR BARS ASSIGNED IN INSPECTOR");
            return;
        }

        for (int i = 0; i < repairBars.Length; i++)
        {
            if (repairBars[i] != null)
            {
                Debug.Log("Turning OFF: " + repairBars[i].name);
                repairBars[i].SetActive(false);
            }
            else
            {
                Debug.LogError($"NULL BAR at index: {i}");
            }
        }

        repairLevel = 0;
        Debug.Log("✅ All bars OFF at start");
    }

    public void RepairShip()
    {
        Debug.Log("=== REPAIR SHIP CALLED ===");

        if (!CrystalManager.Instance.HasCrystal(1))
        {
            Debug.Log("❌ No crystals available");
            return;
        }

        Debug.Log("✅ Crystal detected — using 1");
        CrystalManager.Instance.UseCrystal(1);

        for (int i = 0; i < barsPerCrystal; i++)
        {
            Debug.Log($"✅ Turning ON bar: {repairBars[repairLevel].name}");
            repairBars[repairLevel].SetActive(true);

            repairLevel++;

            if (repairLevel >= repairBars.Length)
            {
                Debug.Log("🛑 All bars have been filled");
                crashedShip.SetActive(false);
                repairedShip.SetActive(true);
                // SceneManager.LoadScene("Spaceship"); (move later to separate script)
                return;
            }
        }
    }
}
