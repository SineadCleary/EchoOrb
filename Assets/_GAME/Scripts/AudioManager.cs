using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    [Header("Gameplay Clips")]
    [SerializeField] Sound activateClip;
    [SerializeField] Sound hurtClip;
    [SerializeField] Sound pickupOrbClip;
    [SerializeField] Sound placeClip;
    [SerializeField] Sound takeClip;
    [SerializeField] Sound throwClip;
    [SerializeField] Sound blockClip;

    Sound[] randomSoundList;

    [Header("UI Clips")]
    [SerializeField] Sound paperClip;
    [SerializeField] Sound popClip;

    [Header("Sources")]
    [SerializeField] AudioSource SFXSource;

    public const string MASTER = "masterVolume";
    public const string MUSIC = "musicVolume";
    public const string SFX = "SFXVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
        randomSoundList = new Sound[] { activateClip, hurtClip, pickupOrbClip, placeClip, takeClip, throwClip, blockClip, paperClip, popClip };
    }

    void PlaySound(Sound sound)
    {
        SFXSource.PlayOneShot(sound.clip, sound.volume);
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC, 1f);
        float SFXVolume = PlayerPrefs.GetFloat(SFX, 1f);

        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolume) * 20);
    }

    public void PlayActivateSound() => PlaySound(activateClip);
    public void PlayHurtSound() => PlaySound(hurtClip);
    public void PlayPickupSound() => PlaySound(pickupOrbClip);
    public void PlayPlaceSound() => PlaySound(placeClip);
    public void PlayTakeSound() => PlaySound(takeClip);
    public void PlayThrowSound() => PlaySound(throwClip);
    public void PlayBlockSound() => PlaySound(blockClip);
    public void PlayPaperUISound() => PlaySound(paperClip);
    public void PlayPopUISound() => PlaySound(popClip);
    public void PlayRandomSound() => PlaySound(randomSoundList[Random.Range(0, randomSoundList.Length)]);
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1f;
}
