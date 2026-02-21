using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SO_Placeable", menuName = "Scriptable Objects/Placeable")]
public class SO_Placeable : ScriptableObject
{
    public int id;
    //public PlaceableKind kind;
    public string itemName;
    public Sprite icon;
    public GameObject editorPrefab;
    public GameObject gamePrefab;
    public TileBase tile;
}

//public enum PlaceableKind
//{
//    Item,
//    Tile
//}