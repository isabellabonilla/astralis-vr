using UnityEngine;

public class AlienAudio : MonoBehaviour
{
    public AudioClip[] alienSounds;       // Drag MP3s here
    public float minDelay = 2f;
    public float maxDelay = 6f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSounds());
    }

    System.Collections.IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            // Wait a random amount of time
            float wait = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(wait);

            // Pick a random sound effect
            AudioClip clip = alienSounds[Random.Range(0, alienSounds.Length)];

            // Play it
            audioSource.PlayOneShot(clip);
        }
    }
}
