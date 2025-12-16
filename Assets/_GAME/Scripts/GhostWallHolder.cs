using UnityEngine;

public class GhostWallHolder : Holder
{
    [SerializeField] GhostWall[] walls;

    protected override void OnActivate()
    {
        foreach (var wall in walls)
        {
            wall.Toggle();
        }
    }
}
