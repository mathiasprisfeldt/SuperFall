using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [field: SerializeField] public GameObject SFXAudioSourcePrefab { get; set; }
    [field: SerializeField] public GameObject MusicAudioSourcePrefab { get; set; }

    public void PlaySFX(AudioClip clip)
    {
        var audioSource = Instantiate(SFXAudioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        var audioSource = Instantiate(MusicAudioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}