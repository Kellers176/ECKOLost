//using UnityEngine;

//public class TileGridScript : MonoBehaviour
//{

//    //create variables
//    public static TileGridScript instance;
//    public Color noneColor, walkableColor, dashableColor;

//    public LayerMask obstacleLayer;

//    public GameObject tilePrefab;

//    public Vector2 gridWorldSize;
//    public float nodeRadius;
//    public TileSquare[,] tileGrid;

//    float nodeDiameter;
//    int gridNumX, gridNumY;

//    private void Awake()
//    {
//        instance = this;

//        nodeDiameter = nodeRadius * 2;

//        // how many nodes we can fit into the X distance of the world
//        gridNumX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
//        // how many nodes we can fit into the Y distance of the world
//        gridNumY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

//        CreateGrid();
//    }

//    void CreateGrid()
//    {
//        tileGrid = new TileSquare[gridNumX, gridNumY];

//        // Get the bottom left coordinate of the World Space
//        Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2.0f - Vector2.up * gridWorldSize.y / 2.0f;

//        for (int x = 0; x < gridNumX; x++)
//        {
//            for (int y = 0; y < gridNumY; y++)
//            {
//                // Use math to get each point in the world that a node will occupy
//                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

//                GameObject tile = Instantiate(tilePrefab, worldPoint, Quaternion.identity);
//                tileGrid[x, y] = tile.GetComponent<TileSquare>();
//                tileGrid[x, y].gridX = x;
//                tileGrid[x, y].gridY = y;
//            }
//        }
//    }

//    public TileSquare TileFromWorldPoint(Vector2 worldPosition)
//    {
//        // Think about this like UVs. We are converting from world space to node space
//        float percentX = (worldPosition.x + gridWorldSize.x / 2.0f) / gridWorldSize.x;
//        float percentY = (worldPosition.y + gridWorldSize.y / 2.0f) / gridWorldSize.y;
//        percentX = Mathf.Clamp(percentX, 0f, .999f);
//        percentY = Mathf.Clamp(percentY, 0f, .999f);

//        // We now map the location to the X and Y coordinates of our grid
//        int x = (int)(gridNumX * percentX);
//        int y = (int)(gridNumY * percentY);

//        return tileGrid[x, y];
//    }

//    //update the tiles surrounding the player each time that they move
//    public void UpdateTiles(TileSquare playerTile, int freeMovesLeft, int dashMovesLeft)
//    {

//        foreach (TileSquare tile in tileGrid)
//        {
//            tile.SetTile(TileType.none);
//        }
//        //initalizes the tiles around the player
//        int currentGridX = playerTile.gridX;
//        int currentGridY = playerTile.gridY;

//        //as long as the player is able to move in the x direction, continue updating tiles around the player
//        for (int x = currentGridX - freeMovesLeft - dashMovesLeft; x <= currentGridX + freeMovesLeft + dashMovesLeft; x++)
//        {
//            //as long as the player is able to move in the y direction, continue updating the tiles around the player
//            for (int y = currentGridY - freeMovesLeft - dashMovesLeft; y <= currentGridY + freeMovesLeft + dashMovesLeft; y++)
//            {
//                if (x == currentGridX && y == currentGridY)
//                {
//                    // Nothing
//                }
//                //create the dash tiles around the player is they are still available
//                else if ((x > currentGridX - freeMovesLeft - dashMovesLeft) || (x < currentGridX + freeMovesLeft + dashMovesLeft))
//                {
//                    if (y <= currentGridY + freeMovesLeft + dashMovesLeft - Mathf.Abs(currentGridX - x) && (y >= currentGridY - freeMovesLeft - dashMovesLeft + Mathf.Abs(currentGridX - x)))
//                    {
//                        tileGrid[x, y].SetTile(TileType.dashable);
//                    }
//                }
//                //create the walking tiles around the player if they are still available
//                if ((x > currentGridX - freeMovesLeft) || (x < currentGridX + freeMovesLeft))
//                {
//                    if (y <= currentGridY + freeMovesLeft - Mathf.Abs(currentGridX - x) && (y >= currentGridY - freeMovesLeft + Mathf.Abs(currentGridX - x)))
//                    {
//                        tileGrid[x, y].SetTile(TileType.walkable);
//                    }
//                }

//                if (Physics2D.OverlapCircle(tileGrid[x, y].transform.position, 0.5f, obstacleLayer)){

//                    tileGrid[x, y].SetTile(TileType.none);
//                }

//            }
//        }
//    }
//}
