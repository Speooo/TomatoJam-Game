using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource source2D;
    public float sfxVolume = 1f;
    public float musicVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx3D(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, position, Mathf.Clamp01(volume * sfxVolume));
    }

    public void PlaySfx2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        source2D.PlayOneShot(clip, volume * sfxVolume);
    }

    public void IncreaseSFXVolume()
    {
        sfxVolume = Mathf.Clamp01(sfxVolume + 0.1f);
        source2D.volume = sfxVolume;
    }

    public void DecreaseSFXVolume()
    {
        sfxVolume = Mathf.Clamp01(sfxVolume - 0.1f);
        source2D.volume = sfxVolume;
    }

    public void IncreaseMusicVolume()
    {
        musicVolume = Mathf.Clamp01(musicVolume + 0.1f);
        sourceMusic.volume = musicVolume;
    }

    public void DecreaseMusicVolume()
    {
        musicVolume = Mathf.Clamp01(musicVolume - 0.1f);
        sourceMusic.volume = musicVolume;
    }
}
