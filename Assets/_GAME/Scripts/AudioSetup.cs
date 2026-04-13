using UnityEngine;

public class AudioSetup : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
