using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource source2D;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float musicVolume = 1f;

    private float musicFadeDuration = 3f;
    private Coroutine musicFadeRoutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySfx3D(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, position, Mathf.Clamp01(volume * sfxVolume));
    }

    public void PlaySfx2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        source2D.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicFadeRoutine != null)
            StopCoroutine(musicFadeRoutine);

        musicFadeRoutine = StartCoroutine(FadeToMusic(clip));
    }

    private IEnumerator FadeToMusic(AudioClip next)
    {
        float t = 0f;

        // Fade out
        while (t < musicFadeDuration)
        {
            t += Time.deltaTime;
            sourceMusic.volume = Mathf.Lerp(musicVolume, 0f, t / musicFadeDuration);
            yield return null;
        }
        sourceMusic.volume = 0f;

        sourceMusic.clip = next;
        sourceMusic.Play();

        // Fade in
        t = 0f;
        while (t < musicFadeDuration)
        {
            t += Time.deltaTime;
            sourceMusic.volume = Mathf.Lerp(0f, musicVolume, t / musicFadeDuration);
            yield return null;
        }
        sourceMusic.volume = musicVolume;
    }
}
