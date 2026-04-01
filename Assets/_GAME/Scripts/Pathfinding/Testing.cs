using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathfindingTester : MonoBehaviour
{
    [SerializeField] private int width = 58;
    [SerializeField] private int height = 31;

    private Pathfinding pathfinding;

    private Vector3 startWorld;
    private Vector3 endWorld;
    private bool hasStart;

    private void Start()
    {
        pathfinding = new Pathfinding(width, height);
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        // LEFT CLICK = path
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    Vector3 mouseWorld = GetMouseWorldPosition();

        //    if (!hasStart)
        //    {
        //        startWorld = mouseWorld;
        //        hasStart = true;
        //    }
        //    else
        //    {
        //        endWorld = mouseWorld;
        //        hasStart = false;

        //        DrawPath(startWorld, endWorld);
        //    }
        //}

        // RIGHT CLICK = obstacle
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ToggleObstacle(GetMouseWorldPosition());
        }
    }

    private void ToggleObstacle(Vector3 worldPos)
    {
        var grid = pathfinding.GetGrid();

        grid.GetGridPosition(worldPos, out int x, out int y);

        PathNode node = grid.GetGridObject(x, y);
        if (node == null) return;

        node.isWalkable = !node.isWalkable;

        grid.TriggerGridObjectChanged(x, y);

        // draw visual
        Vector3 center = grid.GetWorldPosition(x, y);
        Debug.DrawRay(center, Vector3.up * .4f, node.isWalkable ? Color.white : Color.red, 100f);
    }

    private void DrawPath(Vector3 startWorld, Vector3 endWorld)
    {
        MyGrid<PathNode> grid = pathfinding.GetGrid();

        //int startX = Mathf.FloorToInt(startWorld.x);
        //int startY = Mathf.FloorToInt(startWorld.y);

        //int endX = Mathf.FloorToInt(endWorld.x);
        //int endY = Mathf.FloorToInt(endWorld.y);
        grid.GetGridPosition(startWorld, out int startX, out int startY);
        grid.GetGridPosition(endWorld, out int endX, out int endY);

        // Call your A*
        var path = GetPath(startX, startY, endX, endY);

        if (path == null) return;

        //for (int i = 0; i < path.Count - 1; i++)
        //{
        //    Vector3 a = new Vector3(path[i].x + .5f, path[i].y + .5f);
        //    Vector3 b = new Vector3(path[i + 1].x + .5f, path[i + 1].y + .5f);

        //    Debug.DrawLine(a, b, Color.green, 20f);
        //}
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 a = pathfinding.GetGrid().GetWorldPosition(path[i].x, path[i].y);
            Vector3 b = pathfinding.GetGrid().GetWorldPosition(path[i + 1].x, path[i + 1].y);

            Debug.DrawLine(a, b, Color.green, 5f);
        }
    }

    // wrapper because your FindPath is private
    private List<PathNode> GetPath(int startX, int startY, int endX, int endY)
    {
        var method = typeof(Pathfinding).GetMethod(
            "FindPath",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
        );

        return (List<PathNode>)method.Invoke(pathfinding, new object[] { startX, startY, endX, endY });
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(mouseScreen);
        world.z = 0f;
        return world;
    }
}