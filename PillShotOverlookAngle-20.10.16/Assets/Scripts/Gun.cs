using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle; //建立一个公共的枪口
    public Projectile projectile;
    public float msBetweenShots = 100f; //这是开枪的间隔，用的毫秒
    public float muzzleVelocity = 35; //枪口发射的速度
    private float nextShotTime; //等待下一次开枪的时间

    public void Shoot()
    {
        if (Time.time > nextShotTime)//控制开枪间隔时间
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}