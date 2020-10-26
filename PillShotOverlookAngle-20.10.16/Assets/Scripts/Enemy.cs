using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State //表示当前敌人状态的，枚举类型
    {
        Idle, //站立不动
        Chasing, //追踪目标
        Attacking //攻击中
    }

    private State currentState;

    private NavMeshAgent pathfinder;

    private Transform target;
    private float attackDistanceThreshold = .5f; //敌人对主角的攻击限制距离，1.5个unity距离单位
    private float timeBetweenAttacks = 1; //敌人攻击的间隔时间
    private float nextAttackTime; //敌人下一次允许攻击时间
    private float myCollisionRadius; //敌人的半径
    private float targetCollisionRadius;

    private Material skinMaterial;
    private Color originalColor;

    // Start is called before the first frame update
    protected override void Start() // override 覆写父类中的虚方法
    {
        base.Start(); // 但是在这里用 base关键字启动父类原本的虚方法
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material; //用组件的 渲染器 调出来 material
        originalColor = skinMaterial.color;
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        StartCoroutine(UpdatePath()); //这一句是启动协程的代码，不然协程不奏效
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float squareDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            //单纯的去比较两个 Vector3 坐标的距离的话，使用 Vector3.Distance 功能，但非常吃程序，所以换成比较两个变量的平方数，不吃程序，且获得结果一样
            if (squareDistanceToTarget <
                Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)
                ) //Mathf.Pow（要获得平方数的变量，几平方）
                //这句写成这样，也是没关系的 sqarDistanceToTarget < attackDistanceThreshold * attackDistanceThreshold
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack()); //StartCoroutine 启动协程的意思
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;
        Vector3 originalPosition = transform.position; //攻击的起始位置，等于当前自己的位置

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition =
            target.position - directionToTarget * myCollisionRadius;

        //Vector3 attackPosition = target.position; //攻击的目标的位置，是目标的当前位置
        float attackSpeed = 3; //这是攻击的速度
        float percent = 0; //声明一个百分比

        skinMaterial.color = Color.yellow;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; //这里使用的功能叫做对称函数，这个数学知识后面要查一下
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null; // yield return null表示暂缓一帧，在下一帧接着往下处理
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath() //协程，为了节省程序运算
    {
        float refreshRate = .25f; //refreshRate 刷新率
        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition =
                    target.position - directionToTarget *
                    (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!isDead) //这句防止程序报异常
                {
                    pathfinder.SetDestination(targetPosition); //这句写在 Update里 有很多敌人时候，程序运行会吃力
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}