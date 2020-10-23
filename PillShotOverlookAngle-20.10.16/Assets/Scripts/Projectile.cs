using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;//设定一个默认的子弹速度

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;//这句是为了后续更改子弹速度比较方便
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}