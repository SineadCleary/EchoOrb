using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    [SerializeField] float timerLength = 6f;
    float timer;

    void OnEnable()
    {
        timer = timerLength;
    }

    void Update()
    {
        timer -= Time.unscaledDeltaTime;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
