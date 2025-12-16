using UnityEngine;

public class CannonHolder : Holder
{
    [SerializeField] Cannon[] cannons;

    protected override void OnActivate()
    {
        foreach (var cannon in cannons)
        {
            cannon.Shoot();
        }
    }
}
