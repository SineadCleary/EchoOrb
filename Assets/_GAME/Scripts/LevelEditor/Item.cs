using UnityEngine;

public class Item : Placeable
{
    public override void AddToLevelData(LevelData levelData)
    {
        Vector3 pos = transform.position;

        levelData.items.Add(new ItemData
        {
            prefabID = data.id,
            x = pos.x,
            y = pos.y
        });
    }

}
