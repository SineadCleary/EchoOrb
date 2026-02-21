using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Placeable : MonoBehaviour 
{
    public SO_Placeable data;
    public abstract void AddToLevelData(LevelData levelData);
}

public class Tile : Placeable
{
    public override void AddToLevelData(LevelData levelData)
    {
        Vector3 pos = transform.position;

        levelData.tiles.Add(new TileData
        {
            tileID = data.id,
            x = Mathf.RoundToInt(pos.x),
            y = Mathf.RoundToInt(pos.y)
        });
    }
}
