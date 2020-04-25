using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*[SerializeField]*/
    private float speed = 10;
    private float damage = 1;

    public LayerMask collisionMask; //어떤 오브젝트, 레이어와 충돌할지 결정하는 Mask

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    private void CheckCollisions(float _moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward); // (시작위치 ,방향)
        RaycastHit hit;
        // Raycast (ray, out hit, distance, mask, QueryTriggerInteraction) 
        // QueryTriggerInteraction=> triggerCollider들과 충돌할지 안할지 결정
        if (Physics.Raycast(ray,out hit, _moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        } 
    }

    private void OnHitObject(RaycastHit hit) 
    {
        //충돌순간을 가져와야함?
        //Debug.Log(hit.collider.gameObject.name);
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if(damageableObject != null) // Idamageable이 붙어있지 않은것은 제외하고
        {
            damageableObject.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}

