using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());//这一句是启动协程的代码，不然协程不奏效
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
            pathfinder.SetDestination(targetPosition); //这句写在 Update里 有很多敌人时候，程序运行会吃力
            yield return new WaitForSeconds(refreshRate);
        }
    }
}