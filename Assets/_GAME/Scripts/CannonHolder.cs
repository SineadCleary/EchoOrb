using System.Collections.Generic;
using UnityEngine;

public class CannonHolder : Holder
{
    public List<Cannon> cannons;

    protected override void OnActivate()
    {
        foreach (var cannon in cannons)
        {
            cannon.Shoot();
        }
    }
}
