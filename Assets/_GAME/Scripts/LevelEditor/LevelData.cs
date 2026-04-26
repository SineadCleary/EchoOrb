using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string title;
    public string author;
    public string date;
    public bool complete;
    public Vector3 cameraPos;
    public float cameraZoom;
    public List<ItemData> items = new List<ItemData>();
    public List<TileData> tiles = new List<TileData>();
    public bool won;

    public LevelData(string title, string author)
    {
        this.title = title;
        this.author = author;
        cameraPos = new Vector3(0, 0, -10);
        cameraZoom = 5;
        complete = false;
    }
}

[System.Serializable]
public class ItemData
{
    public int prefabID;
    public float x;
    public float y;
    public int rotation;
}

[System.Serializable]
public class TileData
{
    public int tileID;
    public int x;
    public int y;
}
