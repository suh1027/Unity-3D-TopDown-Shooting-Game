using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{ 
    // 변수
    private Vector3 velocity; // Velocity
    private Vector3 prevPos; // 기존 위치값 저장

    // Flag
    private bool isMove = false;

    // 컴포넌트
    private Rigidbody myRigidBody;
    private Animator playerAnim;   // playerAnim 파라미터 Bool) IsRun / Trigger) Shoot Death

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    //RIgidbody는 FixedUpdate에서 사용 -> 짧게 반복적으로 실행되야하기 때문에?
    //프레임 저하가 발생해도 프레임에 시간의 가중치를 곱해 실행되어 이동속도를 유지시킬수 있음

    void FixedUpdate()
    {
        Move();
        MoveCheck();
    }
    
    public void setVelocity(Vector3 _velocity) // Player 로 부터 입력받은 값을 저장하는 함수
    {
        velocity = _velocity;
    }
    
    private void Move()
    {
        // Move
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
        
        // Anim
        playerAnim.SetBool("IsRun", isMove);     
    }

    // 움직임 체크 함수
    private void MoveCheck()
    {
        if(Vector3.Distance(prevPos, transform.position) >= 0.01f) // 이전 prevPos 와 transform.position을 비교하여 차이가 0.01f 이상이면 Move
            isMove = true;
        else
            isMove = false;

        prevPos = transform.position;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedpoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedpoint); // 재귀함수
    }    
}
