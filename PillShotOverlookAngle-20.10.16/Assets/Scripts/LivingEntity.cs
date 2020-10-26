using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public static int testSV = 6;
    public float startingHealth;
    protected float health; //protected 为了不出现在引擎，但是能让派生类访问得到
    protected bool isDead;
    public event System.Action OnDeath; //这是一个事件，关键字 event

    protected virtual void Start() //virtual 指父类中的虚方法
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath(); //触发事件的意思
        }

        GameObject.Destroy(gameObject);
    }
}