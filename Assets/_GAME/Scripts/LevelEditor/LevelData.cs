using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public List<ItemData> items = new List<ItemData>();
    public List<TileData> tiles = new List<TileData>();
}

// Possibly combine ItemData and TileData if floats are not required...
[System.Serializable]
public class ItemData
{
    public int prefabID;
    public float x;
    public float y;
}

[System.Serializable]
public class TileData
{
    public int tileID;
    public int x;
    public int y;
}
