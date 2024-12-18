using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator Instance;
    
    public int height;
    public int width;
    public float cellSize;
    private Tile[,] _grid;

    [SerializeField] private bool onDrawGrid;
    [SerializeField] private GameObject cellPrefab;
    
    private void Awake()
    {
        Instance = this;
        CreateGrid();
    }
    
    /// <summary>
    /// Creating the grid 
    /// </summary>
    
    private void CreateGrid()
    {
        _grid = new Tile[width, height];
        for (var y = 0; y < height; y++)
        {   
            for (var x = 0; x < width; x++)
            {
                var startPos = new Vector3(x + 0.5f, y + 0.5f);
                var worldPosition = new Vector3(startPos.x * cellSize, startPos.y * cellSize, 0f);
                _grid[x, y] = new Tile(worldPosition, new Vector2Int(x, y));
                RenderGrid(worldPosition);
            }
        }
    }
    
    private void RenderGrid(Vector3 worldPosition)
    {
        cellPrefab.transform.localScale = new Vector3(cellSize,cellSize,0f);
        Instantiate(cellPrefab, transform.position + worldPosition, Quaternion.identity, transform);
    }
    

    private void OnDrawGizmos()
    {
        if (!onDrawGrid) return;
        
        Gizmos.color = Color.white;

        var pos0 = new Vector3();
        var pos1 = new Vector3();

        for (var x = 0; x < width ; x++)
        {
            pos0.x = width * cellSize;
            pos0.y = x * cellSize;
            pos1.x = 0;
            pos1.y = x * cellSize;
            Gizmos.DrawLine(pos0,pos1);
        }
        
        for (var y = 0; y < height + 2; y++)
        {
            pos0.x = y * cellSize;
            pos0.y = height  * cellSize;
            pos1.x = y * cellSize;
            pos1.y = 0;
            Gizmos.DrawLine(pos0,pos1);
        }
    }

    private void OnValidate()
    {
        if (width <= 1)
            width = 1;
        if (height <= 1)
            height = 1;
        if (cellSize <= 0.25f)
            cellSize = 0.25f;
    }
    
}
