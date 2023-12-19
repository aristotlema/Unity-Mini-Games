using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private const float timePerTick = 2f;
    float timer;

    GridManager grid;
    private int gridMax;

    private int gridAdjusted; // (gridMax + 1 ) / 2; - used align game objects to grid indexes - without 3,3 would end up being (6,6)
    private static string[] directions = { "Up", "Down", "Left", "Right" };
    private string currentDirection = directions[0];
    private List<Vector3> snakeBody = new List<Vector3>();

    private GameObject snakeHead;
    private List<GameObject> snakeTileList = new List<GameObject>();

    [Header("SnakeSprites")]
    [SerializeField] Sprite snakeHeadSprite;
    [SerializeField] Sprite snakeBodySprite;
    [SerializeField] GameObject snakeBodyPrefab;
    [SerializeField] Sprite snakeCurveSprite;
    [SerializeField] Sprite snakeTailSprite;

    private SpriteRenderer snakeTileSprite;

    void Start()
    {
        //import and setup grid variables based on size
        grid = FindObjectOfType<GridManager>();
        gridMax = grid.GetGridSize() - 1;
        gridAdjusted = (gridMax + 1) / 2;

        //setup snake
        snakeBody.Add(new Vector3(3, 3));
        snakeBody.Add(new Vector3(3, 2));
        snakeBody.Add(new Vector3(3, 1));
        snakeBody.Add(new Vector3(3, 0));
        snakeBody.Add(new Vector3(2, 0));

        RenderSnake();
    }

    // Update is called once per frame
    void Update() 
    {
        //Needs to run every frame outside of timer incase input change
        SnakeController();

        timer += Time.deltaTime; 

        if (timer > timePerTick )
        {
            DirectionHandler();
            timer = 0;

            //Top
            if (snakeBody[0].y > gridMax)
            {
                snakeBody[0] = new Vector3(snakeBody[0].x, 0);
            }
            //Bottom
            else if (snakeBody[0].y < 0)
            {
                snakeBody[0] = new Vector3(snakeBody[0].x, gridMax);
            }
            else if (snakeBody[0].x > gridMax)
            {
                snakeBody[0] = new Vector3(0, snakeBody[0].y);
            }
            else if (snakeBody[0].x < 0)
            {
                snakeBody[0] = new Vector3(gridMax, snakeBody[0].y);
            }
            UpdateSnake();
        }
    }

    private void RenderSnake()
    {
        int snakeTileNumber = 0;
        foreach(Vector3 snakeTile in snakeBody)
        {
            Debug.Log($"snake piece {snakeTileNumber}");
            if(snakeTile == snakeBody[0])
            {
                snakeHead = new GameObject($"Snake Head");
                snakeHead.transform.position = new Vector3(snakeTile.x - (gridAdjusted - 0.5f), snakeTile.y - (gridAdjusted - 0.5f));
                snakeTileList.Add(snakeHead);
                snakeTileSprite = snakeHead.AddComponent<SpriteRenderer>();
                snakeTileSprite.sprite = snakeHeadSprite;
                snakeTileSprite.color = new Color(255, 255, 255);
                snakeTileSprite.sortingOrder = 10;
            }
            else
            {

                GameObject snakeTileGO = Instantiate(snakeBodyPrefab);
                snakeTileList.Add(snakeTileGO);
                snakeTileGO.transform.position = new Vector3(snakeTile.x - (gridAdjusted - 0.5f), snakeTile.y - (gridAdjusted - 0.5f));
            }
            snakeTileNumber += 1;
        }
    }

    private int CornerPieceRotationCalculation(int snakeTileNumber)
    {
        Vector3 previousTile = snakeBody[snakeTileNumber - 1];
        Vector3 currentTile = snakeBody[snakeTileNumber];
        Vector3 nextTile = snakeBody[snakeTileNumber + 1];

        bool rotate0 = (previousTile.x == currentTile.x - 1 && nextTile.y == currentTile.y + 1) || (previousTile.y == currentTile.y + 1 && nextTile.x == currentTile.x - 1);
        bool rotate90 = (previousTile.y == currentTile.y - 1 && nextTile.x == currentTile.x - 1) || (previousTile.x == currentTile.x - 1 && nextTile.y == currentTile.y - 1);
        bool rotate180 = (previousTile.x == currentTile.x + 1 && nextTile.y == currentTile.y - 1) || (previousTile.y == currentTile.y - 1 && nextTile.x == currentTile.x + 1);
        bool rotate270 = (previousTile.y == currentTile.y + 1 && nextTile.x == currentTile.x + 1) || (previousTile.x == currentTile.x + 1 && nextTile.y == currentTile.y + 1);

        if (rotate0)
        {
            return 0;
        }
        else if(rotate90)
        {
            return 90;
        }
        else if (rotate180)
        {
            return 180;
        }
        else if (rotate270)
        {
            return 270;
        }
        else
        {
            Debug.Log("Corner Piece Calculation Failed");
            return 360;
        }
    }

    private int StraightPieceRotationCalculation(int snakeTileNumber)
    {
        Vector3 previousTile = snakeBody[snakeTileNumber - 1];
        Vector3 currentTile = snakeBody[snakeTileNumber];
        Vector3 nextTile = snakeBody[snakeTileNumber + 1];

        bool rotate0 = previousTile.x == nextTile.x;
        bool roate90 = previousTile.y == nextTile.y;

        if(rotate0)
        {
            return 0;
        }
        else if(roate90)
        {
            return 90;
        }

        Debug.Log("Calculation Failed");
        return 360;
    }


    private bool IsStraightPiece(int snakeTileNumber)
    {
        Vector3 previousTile = snakeBody[snakeTileNumber - 1];
        Vector3 currentTile = snakeBody[snakeTileNumber];
        Vector3 nextTile = snakeBody[snakeTileNumber + 1];

        bool leftRight = previousTile.x == nextTile.x;
        bool upDown = previousTile.y == nextTile.y;

        if(leftRight || upDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateSnake()
    {
        int snakeTileNumber = 0;

        foreach (GameObject snakeTile in snakeTileList)
        {
            snakeTileSprite = snakeTile.GetComponent<SpriteRenderer>();
            if (snakeTileNumber > 0 && snakeTileNumber < snakeTileList.Count - 1) // If first and not last
            {
                if(IsStraightPiece(snakeTileNumber))
                {
                    snakeTileSprite.sprite = snakeBodySprite;
                    snakeTile.transform.eulerAngles = Vector3.forward * StraightPieceRotationCalculation(snakeTileNumber);
                }
                else
                {
                    snakeTileSprite.sprite = snakeCurveSprite;
                    snakeTile.transform.eulerAngles = Vector3.forward * CornerPieceRotationCalculation(snakeTileNumber);
                }
            }
            else if (snakeTileNumber == snakeTileList.Count - 1) // If last Tile
            {
                if (snakeBody[snakeTileNumber].x == snakeBody[snakeTileNumber - 1].x)
                {
                    snakeTile.transform.eulerAngles = Vector3.forward * 0;
                }
                //Left Right
                else if (snakeBody[snakeTileNumber].y == snakeBody[snakeTileNumber - 1].y)
                {
                    snakeTile.transform.eulerAngles = Vector3.forward * 90;
                }
            }
            snakeTile.transform.position = new Vector3(snakeBody[snakeTileNumber].x - (gridAdjusted - 0.5f), snakeBody[snakeTileNumber].y - (gridAdjusted - 0.5f));
            snakeTileNumber += 1;
        }
    }

    private void SnakeController()
    {
        if(Input.GetKey(KeyCode.W))
        {
            currentDirection = directions[0]; // Up
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentDirection = directions[2]; // Left
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentDirection = directions[1]; // Down
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentDirection = directions[3]; // Right
        }
    }
    private void DirectionHandler()
    {
        snakeBody.Insert(1, snakeBody[0]);
        snakeBody.RemoveAt(snakeBody.Count - 1);
        if (currentDirection == "Up")
        {
            snakeBody[0] = new Vector3(snakeBody[0].x, snakeBody[0].y + 1);
            snakeHead.transform.eulerAngles = Vector3.forward * 0;
        }
        else if (currentDirection == "Down")
        {
            snakeBody[0] = new Vector3(snakeBody[0].x, snakeBody[0].y - 1);
            //headCoordinates.y -= 1;
            snakeHead.transform.eulerAngles = Vector3.forward * 180;
        }
        else if (currentDirection == "Left")
        {
            snakeBody[0] = new Vector3(snakeBody[0].x - 1, snakeBody[0].y);
            //headCoordinates.x -= 1;
            snakeHead.transform.eulerAngles = Vector3.forward * 90;
        }
        else if (currentDirection == "Right")
        {
            snakeBody[0] = new Vector3(snakeBody[0].x + 1, snakeBody[0].y);
            //headCoordinates.x += 1;
            snakeHead.transform.eulerAngles = Vector3.forward * 270;
        }
    }
}
