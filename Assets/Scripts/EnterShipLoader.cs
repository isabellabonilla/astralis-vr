using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterShipLoader : MonoBehaviour
{
    public AudioSource enterShipAudio;
    public float delay = 1f;

    public void LoadSpaceshipScene()
    {
        if (enterShipAudio != null)
        {
            enterShipAudio.Play();
        }

        Invoke(nameof(LoadScene), delay);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Spaceship");
    }
}