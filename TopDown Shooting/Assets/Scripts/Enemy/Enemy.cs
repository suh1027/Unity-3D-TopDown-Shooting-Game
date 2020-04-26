using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;
    LivingEntity targetEntity;
    //Material skinMaterial; MeshRenderer를 포함 시키지 않는 Zombie
    // -> Sound effect나 Animation으로 처리

    //Color originalColor;

    float timeBetweenAttacks = 1; // 공격 사이의 시간
    float attackDistanceThreshhold = 0.5f;
    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;
    float damage = 1;

    bool hasTarget;

    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
         //skinMaterial = GetComponent<Renderer>().material;
        //originalColor = skinMaterial.color;
        

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
                
    }

    private void OnTargetDeath() 
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                // 이들의 제곱형태로 거리를 받아 제곱근 연산을 사용안하는 방법이 있다.
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                // 목표와 자신의 위치의 차에 제곱한 수를 가져옴

                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshhold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
        
        //Vector3.Distance..
        //Distance 메소드는 제곱근 처리를 하기 때문에 처리비용이 높다.
            
        // pathFinder.SetDestination(target.position); 
        // 매프레임마다 새로운 경로를 요구하기때문에
        // 자원소모가 심한 방법 -> coroutine 으로 실행
    }
    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;
        
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius);



        float attackSpeed = 3; 
        float percent = 0;

        //skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {

            if(percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (Mathf.Pow(percent, 2) + percent) * 4; // 보간값을 사용??? *******
            //***** 이해 어려운..부분..
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null; 
            // while문 루프의 각 처리 사이에서 프레임을 스킵한다. 
            // 작업이 멈추고 update 메소드의 작업이 완전히 수행된후 다음프레임으로 넘어갔을때 
            // 밑의 코드나 다음번 루프때 실행 됨

        }

        //skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while(hasTarget)
        {
            if(currentState == State.Chasing) {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshhold/2);

                if (!dead)
                    pathFinder.SetDestination(target.position);
            }

            yield return new WaitForSeconds(refreshRate); // 0.25초마다 계속 반복하는 Coroutine 을 생성
        }
    }
}
