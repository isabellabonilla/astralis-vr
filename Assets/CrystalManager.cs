using UnityEngine;
using TMPro;

public class CrystalManager : MonoBehaviour
{
    public static CrystalManager Instance;

    public TextMeshProUGUI crystalText;
    public int crystalCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("CrystalManager SET -> " + gameObject.name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCrystal(int amount = 1)
    {
        crystalCount += amount;
        UpdateUI();
    }

    public bool HasCrystal(int amount = 1)
    {
        return crystalCount >= amount;
    }

    public void UseCrystal(int amount = 1)
    {
        {
    Debug.Log("UseCrystal called by: " + System.Environment.StackTrace);

    if (crystalCount >= amount)
    {
        crystalCount -= amount;
        UpdateUI();
    }
    }
    }

    void UpdateUI()
    {
        if (crystalText != null)
            crystalText.text = crystalCount.ToString();
    }
}
