using UnityEngine;

[CreateAssetMenu]
public class ObjectDatabase : ScriptableObject
{
    public ObjectEntry[] entries;

    public GameObject GetPrefab(int id)
    {
        foreach (var entry in entries)
        {
            if (entry.id == id) return entry.prefab;
        }

        Debug.LogError("Missing object id: " + id);
        return null;
    }

    //public int GetID(GameObject gameObject)
    //{
    //    foreach (var entry in entries)
    //    {
    //        if (entry.prefab.name + "(Clone)" == gameObject.name) return entry.id;
    //    }

    //    //PlaceableObject placeable = gameObject.GetComponent<PlaceableObject>();
    //    //if (placeable != null && placeable.Entry != null)
    //    //    return placeable.Entry.id;

    //    Debug.LogError("No id for gameobject: " + gameObject.name);
    //    return -1;
    //}
}

[System.Serializable]
public class ObjectEntry
{
    public int id;
    public GameObject prefab;
}
