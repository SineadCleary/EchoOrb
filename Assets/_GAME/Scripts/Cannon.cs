using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
