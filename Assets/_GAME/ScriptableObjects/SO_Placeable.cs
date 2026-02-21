using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SO_Placeable", menuName = "Scriptable Objects/Placeable")]
public class SO_Placeable : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public GameObject editorPrefab;
    public GameObject gamePrefab;
    public TileBase tile;
    public MyTilemap tilemap;
}

public enum MyTilemap
{
    None,
    Ground,
    Wall,
    GreenWall_A,
    GreenWall_B,
    PurpleWall_A,
    PurpleWall_B,
}