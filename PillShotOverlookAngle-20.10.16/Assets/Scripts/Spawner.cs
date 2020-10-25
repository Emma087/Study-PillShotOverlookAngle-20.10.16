using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] Waves;
    public Enemy enemy;
    private Wave currentWave; //当前进行的波
    private int currentWaveNumber; //当前波的数字
    private int enemiesRemainingToSpawn; //生成点剩余的敌人数量
    private int enemiesRemainingAlive;
    private float nextSpawnTime; //下一次生成时间

    private void Start()
    {
        NextWave();
    }

    [Serializable] //可序列化，为了数组在引擎的 Inspector 窗口分别显示
    public class Wave //类里面写类，可能是一个包含类
        //一波攻击类中，注明了，一波包含的敌人数量，数量和数量间产生的间隔时间
    {
        public int enymyCount;
        public float timeBetweenSpawns;
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < Waves.Length)//这个判断是为了防止数组索引检索报错
        {
            currentWave = Waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enymyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }
}