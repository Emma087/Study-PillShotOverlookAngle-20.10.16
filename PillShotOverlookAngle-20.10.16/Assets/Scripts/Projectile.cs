using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed; //设定一个默认的子弹速度
    public LayerMask CollisionMask;
    private float damage = 1;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; //这句是为了后续更改子弹速度比较方便
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance, CollisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        damageableObject.TakeHit(1, hit);
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}