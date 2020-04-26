using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*[SerializeField]*/
    private float speed = 10;
    private float damage = 1;

    float lifeTime = 3;
    float skinWidth = .1f;

    public LayerMask collisionMask; //어떤 오브젝트, 레이어와 충돌할지 결정하는 Mask

    void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0) // 총알이 생성됬을때 어떤 충돌체 오브젝트와 이미 겹친(충돌한) 상태일때-> 뭐든 하나라도 충돌된 상태
        {
            OnHItObject(initialCollisions[0]);
        }
    }

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
        if (Physics.Raycast(ray,out hit, _moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
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

    private void OnHItObject(Collider c)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null) // Idamageable이 붙어있지 않은것은 제외하고
        {
            damageableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}

