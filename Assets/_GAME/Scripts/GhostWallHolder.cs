using UnityEngine;

public class GhostWallHolder : Holder
{
    [SerializeField] GhostWall[] walls;

    protected override void OnActivate()
    {
        Debug.Log("Activation");
        foreach (var wall in walls)
        {
            wall.Toggle();
        }
    }
}
