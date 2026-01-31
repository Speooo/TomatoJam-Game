using UnityEngine;

public static class AudioManager
{
    public static float SfxVolume = 1f;

    public static void PlaySfx(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
            return;

        AudioSource.PlayClipAtPoint(clip, position, Mathf.Clamp01(volume * SfxVolume));
    }
}
