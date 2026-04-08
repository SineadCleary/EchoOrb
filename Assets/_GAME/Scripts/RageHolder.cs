using UnityEngine;

public class RageHolder : Holder
{
    protected override void OnActivate()
    {
        gameManager.rageMode = !gameManager.rageMode;
    }
}
