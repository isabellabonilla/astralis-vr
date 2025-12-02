using UnityEngine;
using System.Collections;

public class SkyboxFade : MonoBehaviour
{
    public Material[] skyboxMaterials;
    public float fadeDuration = 2.0f;

    private int currentIndex = 0;

    void Start()
    {
        if (skyboxMaterials.Length == 0)
        {
            Debug.LogError("Skybox Materials array is empty!");
            return;
        }

        RenderSettings.skybox = skyboxMaterials[currentIndex];
        SetSkyboxExposure(1.0f);

        StartCoroutine(CycleSkyboxes());
    }

    private void SetSkyboxExposure(float exposure)
    {
        RenderSettings.skybox.SetFloat("_Exposure", exposure);
    }

    IEnumerator CycleSkyboxes()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);

            float time = 0;
            while (time < fadeDuration / 2)
            {
                float t = time / (fadeDuration / 2);
                SetSkyboxExposure(Mathf.Lerp(1.0f, 0.0f, t));
                time += Time.deltaTime;
                yield return null;
            }
            SetSkyboxExposure(0.0f);

            currentIndex = (currentIndex + 1) % skyboxMaterials.Length;
            RenderSettings.skybox = skyboxMaterials[currentIndex];

            time = 0;
            while (time < fadeDuration / 2)
            {
                float t = time / (fadeDuration / 2);
                SetSkyboxExposure(Mathf.Lerp(0.0f, 1.0f, t));
                time += Time.deltaTime;
                yield return null;
            }
            SetSkyboxExposure(1.0f);

            RenderSettings.skybox = skyboxMaterials[currentIndex];
        }
    }
}