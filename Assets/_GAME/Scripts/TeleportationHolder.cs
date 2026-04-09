using UnityEngine;

public class TeleportationHolder : Holder
{
    Transform startPoint;

    protected override void Start()
    {
        base.Start();
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
    }
    protected override void OnActivate()
    {
        player.transform.position = startPoint.position;
    }
}
