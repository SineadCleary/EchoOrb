using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlaceableItem", menuName = "Scriptable Objects/PlaceableItem")]
public class SO_PlaceableItem : ScriptableObject
{
    public int id;
    public string itemName;
    public GameObject prefab;
    public Sprite icon;
}
