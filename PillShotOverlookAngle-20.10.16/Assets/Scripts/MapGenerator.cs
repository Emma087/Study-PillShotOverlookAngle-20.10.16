using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;
    [Range(0, 1)] public float outLinePercent; //创建一个浮动的线条在地板上面

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if (transform.Find(holderName))//教程写的是 FindChild 这里不知道如何修改
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePositon = new Vector3(-mapSize.x / 2 + .5f + x, 0, -mapSize.y / 2 + .5f + y);
                //两个 for循环嵌套，只不过是，把正方形的地板一行一个的画出来
                Transform newTile =
                    Instantiate(tilePrefab, tilePositon, Quaternion.Euler(Vector3.right * 90)) as Transform;
                //Quaternion.Euler 围绕 Z轴旋转的意思
                newTile.localScale = Vector3.one * (1 - outLinePercent); //Vector3.one 就是简写 Vector3(1, 1, 1)。
                newTile.parent = mapHolder;
            }
        }
        
    }
}