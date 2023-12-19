using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Size is same for x and y
    [SerializeField] Sprite sprite;
    [SerializeField] private int gridSize = 10;
    private static GameObject[,] Grid;

    private GameObject tile;
    private SpriteRenderer tileSprite;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = gridSize / 2 + 1;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Grid = new GameObject[gridSize, gridSize];
        int tempGrind = gridSize / 2;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                tile = new GameObject($"{x}, {y}");
                tile.transform.position = new Vector3(x - (tempGrind - 0.5f), y - (tempGrind - 0.5f));
                Grid[x, y] = tile;
                tileSprite = tile.AddComponent<SpriteRenderer>();
                tileSprite.sprite = sprite;
                tileSprite.color = new Color(0, 0, 0);
            }
        }
    }

    public int GetGridSize()
    {
        return gridSize;
    }
}