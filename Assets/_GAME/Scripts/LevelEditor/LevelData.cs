using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string title;
    public Vector3 cameraPos;
    public float cameraZoom;
    public List<ItemData> items = new List<ItemData>();
    public List<TileData> tiles = new List<TileData>();

    public LevelData(string title/*, Vector3 cameraPos, float cameraZoom*/)
    {
        this.title = title;
        //this.cameraPos = cameraPos;
        //this.cameraZoom = cameraZoom;
    }
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
