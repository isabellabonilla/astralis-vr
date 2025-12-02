using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStartManager : MonoBehaviour
{
    public GameObject interiorUI;
    public GameObject finalUI;

    public void ShowFinalUI()
    {
        // Disable the interior UI
        if (interiorUI != null)
            interiorUI.SetActive(false);

        // Enable the final UI
        if (finalUI != null)
            finalUI.SetActive(true);
    }
}