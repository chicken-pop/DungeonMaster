using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 20;

    // DungeonMap�̃^�C�v��I�肷��
    private enum DungeonMapType
    {
        Floor = 0,
        Wall = 1,
        StartPos = 2,
        Portion=3,
        Treasure=4,
        Statu=5,
        NextStagePos = 999,
    }
    private System.Random rand = null;

    private int reqFloorAmount = 0;

    [Tooltip("The Tilemap to draw onto")]
    //�n�ʂ�`�悷�邽�߂�tilemap(Collision��)
    public Tilemap GroundeTilemap;
    //�ǂ�`�悷�邽�߂�tilemap(Collision�L)
    public Tilemap WallTilemap;
    //�A�C�e���Ȃǂ�`�悷�邽�߂�tilemap(Collision�L)
    public Tilemap OuterTilemap;

    public Tile[] Tiles = new Tile[6];

    public RuleTile potion;

    public RuleTile exit;

    public RuleTile treasure;

    public RuleTile statue;

    //public RuleTile ClearStatue;

    // map�͊O����A�N�Z�X�͂ł��邪�A���̃N���X�ȊO�ŃZ�b�g���邱�Ƃ��ł��Ȃ�����
    public static int[,] map
    {
        get;
        private set;
    }

    //Player�̃X�^�[�g�ʒu
    public static Vector2 StartPos = Vector2.zero;

    //Enemy�̃X�^�[�g�ʒu
    public static List<Vector2> EnemyPos = new List<Vector2>();


    private void Awake()
    {
        EnemyPos.Clear();
        // map���쐬����
        map = new int[width, height];
        // map�𖄂߂�
        // GetUpperBound(0)�͈ꎟ���z��̗v�f�̍Ō�̏ꏊ��Ԃ�
        // ��F[1,2,4,8,16,32]�̏ꍇ�AGetUpperBound(0)��5
        for (int x = 0; x < map.GetUpperBound(0) + 1; x++)
        {
            // GetUpperBound(1)�͓񎟌��z��̓񎟌��ڂ̍Ō�̏ꏊ��Ԃ�
            for (int y = 0; y < map.GetUpperBound(1) + 1; y++)
            {
                map[x, y] = (int)DungeonMapType.Wall;
            }
        }
        // seed�����߂܂��BRandom�ɂ������ꍇ��Time.time�B
        float seed = Time.time;
        map = RandomWalkCave(map, seed, 50);

        // �X�^�[�g�ʒu�ƌ��߂܂�
        // map�̒���0�𑀍삵�āA�����_���ɍ��W�����o���܂��B
        // seed�������悤�Ɍ��ʂ��Œ�ł���悤�ɂ��܂��B
        if (rand == null)
        {
            rand = new System.Random(seed.GetHashCode());
        }

        if (reqFloorAmount == 0)
        {
            Debug.LogError("map����������܂���ł����BreqFloorAmount��0�ł��B");
        }
        var startPos = rand.Next(reqFloorAmount);

        Debug.Log($"startPos:{startPos}");
        var nextStagePos = rand.Next(reqFloorAmount);

        Debug.Log($"nextStagePos:{nextStagePos}");
        // �������ʂ������������ꍇ�͂�����xnextStagePos��Random�ŐU�蒼��
        if (startPos == nextStagePos)
        {
            nextStagePos = rand.Next(reqFloorAmount);
        }

        // Portion�̏ꏊ�������_���Ō��߂�
        var portionPos = rand.Next(reqFloorAmount);

        var enemyPos = rand.Next(reqFloorAmount); 

        var trasurePos = rand.Next(reqFloorAmount);

        var statuePos = rand.Next(reqFloorAmount);

        // �J�E���g��0����X�^�[�g���������̂�-1����J�E���g�A�b�v�����Ă����B
        var posCount = -1;
        // GetUpperBound(0)�͂��̎����̍Ō�̒l�̏ꏊ��Ԃ�
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // map�̍��W���󂢂Ă����startpos��nextStagePos�̏ꍇ�ɂ����̍��W��ύX����
                if (map[x, y] == 0)
                {
                    posCount++;
                    if (posCount == startPos)
                    {
                        map[x, y] = (int)DungeonMapType.StartPos;
                        StartPos = new Vector2(x, y); 
                    }
                    if (posCount == nextStagePos)
                    {
                        map[x, y] = (int)DungeonMapType.NextStagePos;
                    }
                    if (posCount == portionPos)
                    {
                        map[x, y] = (int)DungeonMapType.Portion;
                    }
                    if (posCount == enemyPos)
                    {
                        EnemyPos.Add(new Vector2(x, y));
                    }
                    if (posCount == trasurePos)
                    {
                        map[x, y] = (int)DungeonMapType.Treasure;
                    }
                    if (posCount == trasurePos)
                    {
                        map[x, y] = (int)DungeonMapType.Statu;
                    }
                }
            }
        }

        RenderMap(map);

    }
    private void Start()
    {
        
        this.GetComponent<EnemyGenerater>().EnemySpawn(EnemyPos.FirstOrDefault(), EnemyParameterBase.EnemyType.Normal);

        StartCoroutine(SetHighOrcSpawn());
    }

    /// <summary>
    // �����_���ɕ��������߁A�ʒu���ړ����ă^�C�����폜���邱��(0�ɂ���)��dig���Ă����܂��B
    /// </summary>
    /// <param name="map">The array that holds the map information</param>
    /// <param name="seed">The seed for the random</param>
    /// <param name="requiredFloorPercent">The amount of floor we want</param>
    /// <returns>The modified map array</returns>
    public int[,] RandomWalkCave(int[,] map, float seed, int requiredFloorPercent)
    {
        //Seed our random
        rand = new System.Random(seed.GetHashCode());
        //Define our start x position
        int floorX = rand.Next(1, width - 1);

        //Define our start y position
        // rand.Next�͈����܂ł̐�����Ԃ��B
        int floorY = rand.Next(1, height - 1);
        //Determine our required floorAmount
        // �ȉ��̌v�Z��[20,20] �ArequiredFloorPercent��50����(20*20*50)/100=200�}�X�ƂȂ�
        reqFloorAmount = (width * height * requiredFloorPercent) / 100;

        //Used for our while loop, when this reaches our reqFloorAmount we will stop tunneling
        int floorCount = 0;

        //Set our start position to not be a tile (0 = no tile, 1 = tile)
        map[floorX, floorY] = 0;
        //Increase our floor count
        floorCount++;

        while (floorCount < reqFloorAmount)
        {
            //Determine our next direction
            // �����_���Ői�ޕ��������߂�
            int randDir = rand.Next(4);

            switch (randDir)
            {
                case 0: //Up
                    //Ensure that the edges are still tiles
                    if ((floorY + 1) < map.GetUpperBound(1) - 1)
                    {
                        //Move the y up one
                        floorY++;

                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1)
                        {
                            //Change it to not a tile
                            map[floorX, floorY] = (int)DungeonMapType.Floor;
                            //Increase floor count
                            floorCount++;
                        }
                    }
                    break;
                case 1: //Down
                    //Ensure that the edges are still tiles
                    if ((floorY - 1) > 1)
                    {
                        //Move the y down one
                        floorY--;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1)
                        {
                            //Change it to not a tile
                            map[floorX, floorY] = (int)DungeonMapType.Floor;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 2: //Right
                    //Ensure that the edges are still tiles
                    if ((floorX + 1) < map.GetUpperBound(0) - 1)
                    {
                        //Move the x to the right
                        floorX++;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1)
                        {
                            //Change it to not a tile
                            map[floorX, floorY] = (int)DungeonMapType.Floor;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
                case 3: //Left
                    //Ensure that the edges are still tiles
                    if ((floorX - 1) > 1)
                    {
                        //Move the x to the left
                        floorX--;
                        //Check if that piece is currently still a tile
                        if (map[floorX, floorY] == 1)
                        {
                            //Change it to not a tile
                            map[floorX, floorY] = (int)DungeonMapType.Floor;
                            //Increase the floor count
                            floorCount++;
                        }
                    }
                    break;
            }
        }
        //Return the updated map
        return map;
    }

    /// <summary>
    /// Draws the map to the screen
    /// </summary>
    /// <param name="map">Map that we want to draw</param>
    public void RenderMap(int[,] map)
    {
        GroundeTilemap.ClearAllTiles(); //Clear the map (ensures we dont overlap)
        WallTilemap.ClearAllTiles(); //Clear the map (ensures we dont overlap)
        OuterTilemap.ClearAllTiles(); //Clear the map (ensures we dont overlap)

        for (int x = 0; x < width; x++) //Loop through the width of the map
        {
            for (int y = 0; y < height; y++) //Loop through the height of the map
            {
                if (map[x, y] == (int)DungeonMapType.Floor)
                {
                    GroundeTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[0]);
                }
                if (map[x, y] == (int)DungeonMapType.Wall)
                {
                    WallTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[1]);
                }

                if (map[x, y] == (int)DungeonMapType.StartPos)
                {
                    OuterTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[2]);
                }
                if (map[x, y] == (int)DungeonMapType.NextStagePos)
                {
                    OuterTilemap.SetTile(new Vector3Int(x, y, 0), exit);
                }
                if (map[x, y] == (int)DungeonMapType.Portion)
                {
                    OuterTilemap.SetTile(new Vector3Int(x, y, 0), potion);
                    // OuterTilemap.
                    GroundeTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[0]);
                }
                if (map[x, y] == (int)DungeonMapType.Treasure)
                {
                    OuterTilemap.SetTile(new Vector3Int(x, y, 0), treasure);
                    GroundeTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[0]);
                }
                if (map[x, y] == (int)DungeonMapType.Statu)
                {
                     GroundeTilemap.SetTile(new Vector3Int(x, y, 0), Tiles[0]);
                    if(DungeonHierarchyCounter.Instance.dungeonHierarchyCount % 5 == 0  || DungeonHierarchyCounter.Instance.dungeonHierarchyCount % 5 == 1)
                    {
                        OuterTilemap.SetTile(new Vector3Int(x, y, 0), statue); 
                    }
                   
                }
                
            }
        }
    }

    IEnumerator SetHighOrcSpawn()
    {
        yield return new WaitUntil(()=>GameTurnManager.playerActionCount > 10);
        this.GetComponent<EnemyGenerater>().EnemySpawn(StartPos, EnemyParameterBase.EnemyType.High);
    }
}
