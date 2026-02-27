using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for DontDestroyOnLoad

public class MusicManager : MonoBehaviour
{
    public float fadeInDuration = 50.0f; 
    public float bkgVolume = 0.3f; 

    private AudioSource audioSource;
    private static MusicManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();

    }

    public static IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        float endVolume = instance.bkgVolume;
        float currentTime = 0f;

        // Ensure the audio source is playing before starting the fade
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.volume = startVolume;
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            // Lerp the volume over time.
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = endVolume; // Ensure volume reaches exactly the target at the end
    }

    public void StartBkgMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeIn(audioSource, fadeInDuration));
        }
    }
}