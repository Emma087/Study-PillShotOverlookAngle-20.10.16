using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Vector2 mapSize;
    [Range(0, 1)] public float outLinePercent; //创建一个浮动的线条在地板上面

    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;

    public int seed = 10;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        allTileCoords = new List<Coord>(); //所有的图块代码都等于一个新的坐标列表
        for (int x = 0; x < mapSize.x; x++) //这个 for循环嵌套增加障碍物，按照坐标
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        string holderName = "Generated Map";
        if (transform.Find(holderName)) //教程写的是 FindChild 这里不知道如何修改
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePositon = CoordToPosition(x, y);
                //两个 for循环嵌套，只不过是，把正方形的地板一行一个的画出来
                Transform newTile =
                    Instantiate(tilePrefab, tilePositon, Quaternion.Euler(Vector3.right * 90)) as Transform;
                //Quaternion.Euler 围绕 Z轴旋转的意思
                newTile.localScale = Vector3.one * (1 - outLinePercent); //Vector3.one 就是简写 Vector3(1, 1, 1)。
                newTile.parent = mapHolder;
            }
        }

        int obstacleCount = 10;
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle =
                Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * .5f, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
        }
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + .5f + x, 0, -mapSize.y / 2 + .5f + y);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord //协调
    {
        public int x;
        public int y;

        public Coord(int _x, int _y) //构造函数
        {
            x = _x;
            y = _y;
        }
    }
}