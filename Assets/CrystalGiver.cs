using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGiver : MonoBehaviour
{
    public GameObject crystalPrefab;
    public Transform spawnPoint;

    private bool hasGivenCrystal = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasGivenCrystal) return;

        if (other.CompareTag("Player"))
        {
            GiveCrystal();
        }
    }

    void GiveCrystal()
    {
        Instantiate(crystalPrefab, spawnPoint.position, Quaternion.identity);
        hasGivenCrystal = true;
    }
}

