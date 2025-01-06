using UnityEngine;

public class Node
{
    public Vector2Int gridPosition;
    public bool isWalkable;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public Node parent;

    public int FCost { get { return gCost + hCost; } }

    public Node(Vector2Int pos, bool walkable, Vector3 worldPos)
    {
        gridPosition = pos;
        isWalkable = walkable;
        worldPosition = worldPos;
    }
}
