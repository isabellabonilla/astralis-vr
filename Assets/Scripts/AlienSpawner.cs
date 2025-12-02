using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject alienPrefab;
    public int numberOfAliens = 20;
    public Transform terrainMesh; // Assign your mesh terrain here
    public float spawnHeightOffset = 2f; // Slight offset above surface

    [Header("Alien Scale")]
    [Tooltip("Uniform scale applied to each spawned alien.")]
    public float alienScale = 0.5f; // Default = half size

    [Header("Spawn Area (optional)")]
    public Vector2 spawnAreaSize = new Vector2(500, 500); // Adjust to match your terrain size
    public Vector3 spawnAreaCenter = Vector3.zero;

    void Start()
    {
        if (alienPrefab == null || terrainMesh == null)
        {
            Debug.LogError("AlienSpawner: Missing references!");
            return;
        }

        for (int i = 0; i < numberOfAliens; i++)
        {
            SpawnAlien();
        }
    }

    void SpawnAlien()
    {
        // Random XZ within the defined area
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f) + spawnAreaCenter.x;
        float z = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f) + spawnAreaCenter.z;

        // Start ray slightly above terrain to ensure hit
        Vector3 rayStart = new Vector3(x, 1000f, z);
        Ray ray = new Ray(rayStart, Vector3.down);
        RaycastHit hit;

        // Raycast down to find the terrain surface
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject alien = Instantiate(alienPrefab, hit.point + Vector3.up * spawnHeightOffset, Quaternion.identity, transform);

            // Align alien with terrain slope
            alien.transform.up = hit.normal;

            // Scale down the alien
            alien.transform.localScale = Vector3.one * alienScale;
        }
        else
        {
            Debug.LogWarning("AlienSpawner: Raycast didn't hit terrain at " + rayStart);
        }
    }
}
