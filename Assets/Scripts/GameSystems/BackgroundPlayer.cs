using System.Collections;
using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] backgroundMusic = new AudioClip[0];
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] BoolVariable playMusic;

    void Start()
    {
        if (playMusic != null)
            playMusic.Value = true;
    }

    void OnEnable()
    {
        if (playMusic != null)
        {
            playMusic.ValuedChangeTrue += Play;
            playMusic.ValueChangeFalse += Stop;
        }
    }

    void OnDisable()
    {
        if (playMusic != null)
        {
            playMusic.Value = false;
            playMusic.ValuedChangeTrue -= Play;
            playMusic.ValueChangeFalse -= Stop;
        }
    }
    [ContextMenu("Start BG Music")]
    void Play() => StartCoroutine(nameof(StartBackgroundMusic));
    void Stop() => StopCoroutine(nameof(StartBackgroundMusic));

    IEnumerator StartBackgroundMusic()
    {
        if (backgroundMusic.Length > 0 && backgroundSource != null)
        {
            int ranSong = Random.Range(0, backgroundMusic.Length);
            backgroundSource.clip = backgroundMusic[ranSong];
            backgroundSource.Play();
            yield return new WaitForSeconds(backgroundSource.clip.length);
            StartCoroutine(nameof(StartBackgroundMusic));
        }
        else
            StopCoroutine(nameof(StartBackgroundMusic));
    }
}
 