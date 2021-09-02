using UnityEngine;
public class AudioFX : FX
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [Range(0f, 1f)] [SerializeField] float volume;
    
    public override void ActivateFX()
    {
        if (source != null && clip != null)
            source.PlayOneShot(clip, volume);
    }

    public float GetClipLength()
    {
        if (clip != null)
            return clip.length;
        return 0;
    }
}
