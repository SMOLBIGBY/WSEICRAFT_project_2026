using UnityEngine;

public static class OneShotAudio
{
    public static AudioSource Play(
        AudioClip clip,
        float volume = 1f,
        Vector3? position = null,
        float pitch = 1f,
        bool spatial = false,
        Transform parent = null,
        bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogWarning("OneShotAudio.Play: AudioClip is null");
            return null;
        }

        GameObject audioObject = new GameObject("OneShotAudio_" + clip.name);

        if (parent != null)
            audioObject.transform.SetParent(parent);

        audioObject.transform.position = position ?? Vector3.zero;

        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = Mathf.Clamp01(volume);
        source.pitch = pitch;
        source.playOnAwake = false;
        source.loop = loop;
        source.spatialBlend = spatial ? 1f : 0f;

        source.Play();

        if (!loop)
        {
            float safePitch = Mathf.Abs(pitch);
            if (safePitch < 0.01f)
                safePitch = 1f;

            Object.Destroy(audioObject, clip.length / safePitch);
        }

        return source;
    }

    public static AudioSource Play2D(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        return Play(clip, volume, Vector3.zero, pitch, false, null, loop);
    }

    public static AudioSource Play3D(AudioClip clip, Vector3 position, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        return Play(clip, volume, position, pitch, true, null, loop);
    }

    public static void StopAndDestroy(AudioSource source)
    {
        if (source == null)
            return;

        source.Stop();
        Object.Destroy(source.gameObject);
    }
}