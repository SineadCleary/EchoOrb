using UnityEngine;

[CreateAssetMenu(fileName = "SO_Database", menuName = "Scriptable Objects/Database")]
public class SO_Database : ScriptableObject
{
    public SO_PlaceableItem[] items;

    public GameObject GetPrefab(int id)
    {
        foreach (var item in items)
        {
            if (item.id == id) return item.prefab;
        }

        Debug.LogError("Missing object id: " + id);
        return null;
    }
}
