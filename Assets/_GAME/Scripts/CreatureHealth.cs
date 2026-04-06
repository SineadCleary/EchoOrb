using UnityEngine;

public class CreatureHealth : MonoBehaviour
{
    [SerializeField] int health = 40;
    [SerializeField] bool dropsOrb;
    [SerializeField] GameObject orb;

    public void TakeDamage(int healthPoints)
    {
        health -= healthPoints;
        if (health <= 0)
        {
            // Death
            if(dropsOrb && orb != null) Instantiate(orb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
