using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    private NavMeshAgent pathfinder;

    private Transform target;

    // Start is called before the first frame update
    protected override void Start() // override 覆写父类中的虚方法
    {
        base.Start(); // 但是在这里用 base关键字启动父类原本的虚方法
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath()); //这一句是启动协程的代码，不然协程不奏效
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator UpdatePath() //协程，为了节省程序运算
    {
        float refreshRate = .25f; //refreshRate 刷新率
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!isDead)//这句防止程序报异常
            {
                pathfinder.SetDestination(targetPosition); //这句写在 Update里 有很多敌人时候，程序运行会吃力
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}