using UnityEngine;

public class AudioSetup : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    private AudioSource audioSource;

    private void Awake()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music == null) return;

        audioSource = music.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
