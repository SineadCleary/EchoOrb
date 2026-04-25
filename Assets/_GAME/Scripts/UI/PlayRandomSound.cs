using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlayRandomSound();
    }
}
