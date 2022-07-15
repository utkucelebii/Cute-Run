using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{

    private GameManager gameManager;
    private LevelManager levelManager;

    private ModeData.SubMode[] subModes;
    public GameObject lastTile;
    private GameObject tempRight, tempLeft;
    public ModeData.SubMode currentSubMode;
    private ModeData subModeData;

    public List<ModeItem> tiles;

    private const int TILE_LENGTH = 90;
    private const int MAX_TILE_ONGAME = 10;
    private int TILE_COUNT = 0;
    private int ROTATION_COUNT = 0;
    private int subModeChangeCount = 0;
    private int directionChangeCount = 0;
    private Vector3 currentDirection = Vector3.forward;
    public int currentRotation = 0;
    private bool rotation;

    public GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        subModes = levelManager.modeData.subModes;
        subModeController();
        directionController();
        lastTile = transform.GetChild(0).gameObject;
        //lastTile = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, transform);
    }
    private void Awake()
    {
        gameManager = GameManager.Instance;
        levelManager = LevelManager.Instance;
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            changeTileDirection(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            changeTileDirection(false);
        }*/
        if (Vector3.Distance(player.transform.position, lastTile.transform.position) < 50 && TILE_COUNT > 5)
        {
            Destroy(transform.GetChild(0).gameObject);
            TILE_COUNT--;
        }
        spawnTile();
    }

    private void spawnTile()
    {
        if (ROTATION_COUNT == directionChangeCount)
        {
            directionController();
            rotation = true;
            lastTile = Instantiate(tiles.FindAll(x => x.tileType != ModeItem.tile.straight && x.tileType != ModeItem.tile.none)[Random.Range(0, 3)].gameObject, lastTile.transform.position + currentDirection * TILE_LENGTH, Quaternion.Euler(0, -currentRotation, 0), transform);
            switch (lastTile.GetComponent<ModeItem>().tileType)
            {
                case ModeItem.tile.left:
                    switch (currentDirection)
                    {
                        case Vector3 v when v.Equals(Vector3.forward):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.left * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.left):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.back * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.back):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.right * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.right):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.forward * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            break;
                        default:
                            break;
                    }
                    break;
                case ModeItem.tile.right:
                    switch (currentDirection)
                    {
                        case Vector3 v when v.Equals(Vector3.forward):
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.right * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.right):
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.back * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.back):
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.left * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.left):
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.forward * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        default:
                            break;
                    }
                    break;
                case ModeItem.tile.leftRight:
                    switch (currentDirection)
                    {
                        case Vector3 v when v.Equals(Vector3.forward):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.left * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.right * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.left):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.back * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.forward * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.back):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.right * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.left * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        case Vector3 v when v.Equals(Vector3.right):
                            tempLeft = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.forward * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation - 90) * -1, 0), transform);
                            tempRight = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + Vector3.back * TILE_LENGTH * 2, Quaternion.Euler(0, (currentRotation + 90) * -1, 0), transform);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            ROTATION_COUNT = 0;
            TILE_COUNT++;
        }

        if(!rotation && TILE_COUNT < MAX_TILE_ONGAME)
        {
            lastTile = Instantiate(tiles.Find(x => x.tileType == ModeItem.tile.straight).gameObject, lastTile.transform.position + currentDirection * 60, Quaternion.Euler(0, currentRotation, 0), transform);
            TILE_COUNT++;
            ROTATION_COUNT++;
        }

    }

    public void changeTileDirection(bool direction) // true: sol, false: sað
    {
        if(direction)
        {
            currentRotation += 90;
            switch (currentDirection)
            {
                case Vector3 v when v.Equals(Vector3.forward):
                    currentDirection = Vector3.left;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    currentDirection = Vector3.back;
                    break;
                case Vector3 v when v.Equals(Vector3.back):
                    currentDirection = Vector3.right;
                    break;
                case Vector3 v when v.Equals(Vector3.right):
                    currentDirection = Vector3.forward;
                    break;
                default:
                    break;
            }
            lastTile = tempLeft;
        }
        else
        {
            currentRotation -= 90;
            switch (currentDirection)
            {
                case Vector3 v when v.Equals(Vector3.forward):
                    currentDirection = Vector3.right;
                    break;
                case Vector3 v when v.Equals(Vector3.right):
                    currentDirection = Vector3.back;
                    break;
                case Vector3 v when v.Equals(Vector3.back):
                    currentDirection = Vector3.left;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    currentDirection = Vector3.forward;
                    break;
                default:
                    break;
            }
            lastTile = tempRight;
        }
        levelManager.currentDirection = currentDirection;
        rotation = false;
    }

    private void subModeController()
    {
        //currentSubMode = subModes[Random.Range(0, subModes.Length)];
        currentSubMode = subModes[1];
        subModeData = levelManager.modeData.subModeData.ToList<ModeData>().Find(x => x.subModes.ToList<ModeData.SubMode>().Contains(currentSubMode));
        subModeChangeCount = Random.Range(Random.Range(15, 25), Random.Range(25, 35));

        tiles = subModeData.items.ToList<ModeItem>().FindAll(x => x.subMode == currentSubMode && x.itemType == ModeItem.type.tile);

    }

    private void directionController()
    {
        directionChangeCount = Random.Range(Random.Range(2, 4), Random.Range(4, 6));
    }
}
