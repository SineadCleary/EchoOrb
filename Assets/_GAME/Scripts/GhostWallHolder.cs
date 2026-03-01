using System.Collections.Generic;
using UnityEngine;

public class GhostWallHolder : Holder
{
    public MyTilemap[] myTilemaps;
    public List<GhostWall> walls = new List<GhostWall>();
    //public GhostWall[] walls;

    protected override void OnActivate()
    {
        foreach (var wall in walls)
        {
            wall.SetVisible(!wall.visible);
        }
    }
}
