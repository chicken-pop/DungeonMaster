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

    // DungeonMapのタイプを選定する
    private enum DungeonMapType
    {
        Floor = 0,
        Wall = 1,
        StartPos = 2,
        Portion=3,
        NextStagePos = 999,
    }
    private System.Random rand = null;

    private int reqFloorAmount = 0;

    [Tooltip("The Tilemap to draw onto")]
    //地面を描画するためのtilemap(Collision無)
    public Tilemap GroundeTilemap;
    //壁を描画するためのtilemap(Collision有)
    public Tilemap WallTilemap;
    //アイテムなどを描画するためのtilemap(Collision有)
    public Tilemap OuterTilemap;

    public Tile[] Tiles = new Tile[5];

    public RuleTile potion;

    public RuleTile exit;

    // mapは外からアクセスはできるが、このクラス以外でセットすることができなくする
    public static int[,] map
    {
        get;
        private set;
    }

    //Playerのスタート位置
    public static Vector2 StartPos = Vector2.zero;

    //Enemyのスタート位置
    public static List<Vector2> EnemyPos = new List<Vector2>();


    private void Awake()
    {
        // mapを作成する
        map = new int[width, height];
        // mapを埋める
        // GetUpperBound(0)は一次元配列の要素の最後の場所を返す
        // 例：[1,2,4,8,16,32]の場合、GetUpperBound(0)は5
        for (int x = 0; x < map.GetUpperBound(0) + 1; x++)
        {
            // GetUpperBound(1)は二次元配列の二次元目の最後の場所を返す
            for (int y = 0; y < map.GetUpperBound(1) + 1; y++)
            {
                map[x, y] = (int)DungeonMapType.Wall;
            }
        }
        // seedを決めます。Randomにしたい場合はTime.time。
        float seed = Time.time;
        map = RandomWalkCave(map, seed, 50);

        // スタート位置と決めます
        // mapの中の0を操作して、ランダムに座標を取り出します。
        // seedも同じように結果を固定できるようにします。
        if (rand == null)
        {
            rand = new System.Random(seed.GetHashCode());
        }

        if (reqFloorAmount == 0)
        {
            Debug.LogError("mapが生成されませんでした。reqFloorAmountが0です。");
        }
        var startPos = rand.Next(reqFloorAmount);

        Debug.Log($"startPos:{startPos}");
        var nextStagePos = rand.Next(reqFloorAmount);

        Debug.Log($"nextStagePos:{nextStagePos}");
        // もし結果が同じだった場合はもう一度nextStagePosをRandomで振り直す
        if (startPos == nextStagePos)
        {
            nextStagePos = rand.Next(reqFloorAmount);
        }

        // Portionの場所もランダムで決める
        var portionPos = rand.Next(reqFloorAmount);

        var enemyPos = rand.Next(reqFloorAmount); 

        // カウントを0からスタートさせたいので-1からカウントアップさせていく。
        var posCount = -1;
        // GetUpperBound(0)はその次元の最後の値の場所を返す
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // mapの座標が空いていればstartposとnextStagePosの場合にそこの座標を変更する
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
                }
            }
        }

        RenderMap(map);

    }
    private void Start()
    {
        this.GetComponent<EnemyGenerater>().EnemySpawn();
    }

    /// <summary>
    // ランダムに方向を決め、位置を移動してタイルを削除すること(0にする)でdigしていきます。
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
        // rand.Nextは引数までの整数を返す。
        int floorY = rand.Next(1, height - 1);
        //Determine our required floorAmount
        // 以下の計算は[20,20] 、requiredFloorPercentが50だと(20*20*50)/100=200マスとなる
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
            // ランダムで進む方向を決める
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
            }
        }

       
    }
}
