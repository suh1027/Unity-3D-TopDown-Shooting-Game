using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]

public class Player : LivingEntity
{
    [SerializeField] private float moveSpeed = 5;

    //컴포넌트
    private PlayerController controller;
    private Camera viewCamera;
    private GunController gunController;
    private Animator playerAnim;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        playerAnim = GetComponent<Animator>();
        viewCamera = Camera.main; // 메인카메라 불러옴
    }

     void Update()
    {
        // #1. 입력 정보

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.setVelocity(moveVelocity);

        // #2. 플레이어 방향 정보 설정 (Camera -> Plane 으로 Ray 발사 마우스포인터 위치 찾음)

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); // 화면상에서 위치를 반환해주는 메소드(ScreenPointToRay)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, point, Color.red); 
            // -> 바라보는쪽으로 플레이어를 rotate 시키기 위한 Ray 체크 Debug
            controller.LookAt(point);
        }

        // #3. 무기 정보
        if (Input.GetMouseButton(0))
        {
            playerAnim.SetTrigger("Shoot");
            gunController.Shoot();
        }
    }
}
