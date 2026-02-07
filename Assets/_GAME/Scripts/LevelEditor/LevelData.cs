using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public List<TileData> tiles = new List<TileData>();
}

[System.Serializable]
public class TileData
{
    public int id;
    public float x; // change to int for tiles (must snap)
    public float y;
}
