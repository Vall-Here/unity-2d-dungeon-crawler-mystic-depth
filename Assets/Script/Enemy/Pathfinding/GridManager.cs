using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;
    public Tilemap obstacleTilemap;
    public float nodeSize = 1f;

    private Node[,] grid;
    private Vector3Int gridOrigin;
    private Vector2Int gridSize;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        BoundsInt groundBounds = groundTilemap.cellBounds;
        BoundsInt wallBounds = wallTilemap.cellBounds;
        BoundsInt obstacleBounds = obstacleTilemap.cellBounds; 

        int minX = Mathf.Min(groundBounds.min.x, wallBounds.min.x, obstacleBounds.min.x);
        int minY = Mathf.Min(groundBounds.min.y, wallBounds.min.y, obstacleBounds.min.y);
        int maxX = Mathf.Max(groundBounds.max.x, wallBounds.max.x, obstacleBounds.max.x);
        int maxY = Mathf.Max(groundBounds.max.y, wallBounds.max.y, obstacleBounds.max.y);

        gridOrigin = new Vector3Int(minX, minY, 0);
        gridSize = new Vector2Int(maxX - minX, maxY - minY);

        grid = new Node[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x + gridOrigin.x, y + gridOrigin.y, 0);
                Vector3 worldPosition = groundTilemap.CellToWorld(cellPosition) + groundTilemap.cellSize / 2;

                bool isWall = wallTilemap.GetTile(cellPosition) != null;
                bool isObstacle = obstacleTilemap.GetTile(cellPosition) != null; 
                bool isGround = groundTilemap.GetTile(cellPosition) != null;
                bool isWalkable = isGround && !isWall && !isObstacle; 

                grid[x, y] = new Node(new Vector2Int(x, y), isWalkable, worldPosition);
            }
        }
    }


    public Node GetNode(Vector2 worldPosition)
    {
        Vector3Int cellPosition = groundTilemap.WorldToCell(worldPosition);
        int x = cellPosition.x - gridOrigin.x;
        int y = cellPosition.y - gridOrigin.y;

        if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
        {
            return null;
        }

        return grid[x, y];
    }

    public bool IsWalkable(Vector2 worldPosition)
    {
        Node node = GetNode(worldPosition);
        return node != null && node.isWalkable;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        foreach (var dir in directions)
        {
            int checkX = node.gridPosition.x + dir.x;
            int checkY = node.gridPosition.y + dir.y;

            if (checkX >= 0 && checkX < gridSize.x && checkY >= 0 && checkY < gridSize.y)
            {
                neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = GetNode(startPos);
        Node endNode = GetNode(endPos);

        if (startNode == null || endNode == null || !startNode.isWalkable || !endNode.isWalkable)
        {
            return null;
        }

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost ||
                    (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (var neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        Debug.Log("No path found.");
        return null;
    }

    int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        int dstY = Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
        return dstX + dstY;
    }

    List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    void OnDrawGizmosSelected()
    {
        if (grid != null)
        {
            foreach (var node in grid)
            {
        
                if (node.isWalkable)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(node.worldPosition, 0.1f);
                }
            }
        }
        if (groundTilemap != null)
        {
            Gizmos.color = Color.blue;
            Vector3 originPosition = groundTilemap.CellToWorld(gridOrigin) + groundTilemap.cellSize / 2;
            Gizmos.DrawSphere(originPosition, 0.1f);
        }
    
    }



    public Vector2 AlignPositionToGrid(Vector2 worldPosition)
    {
        Vector3Int cellPosition = groundTilemap.WorldToCell(worldPosition);
        Vector3 worldAlignedPosition = groundTilemap.CellToWorld(cellPosition) + groundTilemap.cellSize / 2;
        return (Vector2)worldAlignedPosition;
    }
}
