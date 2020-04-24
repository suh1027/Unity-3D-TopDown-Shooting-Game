using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent pathFinder;
    Transform target;

    void Start()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        // pathFinder.SetDestination(target.position); 
        // 매프레임마다 새로운 경로를 요구하기때문에
        // 자원소모가 심한 방법 -> coroutine 으로 실행
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        while(target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
            pathFinder.SetDestination(target.position);

            yield return new WaitForSeconds(refreshRate); // 0.25초마다 계속 반복하는 Coroutine 을 생성
        }
    }
}
