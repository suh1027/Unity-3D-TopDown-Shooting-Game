using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public ParticleSystem muzzleFlash;

    /*[SerializeField]*/ private float speed = 10;

    //오브젝트 풀링을 위한 ObjectManager
    //public ObjectManager objectManager;

    float nextShotTime;


    public void Shoot()
    {
        muzzleFlash.Play();
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.setSpeed(muzzleVelocity);
            
            // 오브젝트 풀링의 오브젝트 활성화
/*            GameObject newProjectile = objectManager.MakeObj("rifle_bullet");
            
            // 위치 방향 속도 설정
            newProjectile.transform.position = muzzle.position;
            newProjectile.transform.rotation = muzzle.rotation;
            setSpeed(muzzleVelocity);

            newProjectile.transform.Translate(Vector3.forward * Time.deltaTime * speed);*/ 


        }
    }

    /*private void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }*/// 오브젝트 풀링.... 체크하고 다시해보기
}
