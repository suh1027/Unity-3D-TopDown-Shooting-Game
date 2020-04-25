using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float startingHealth;
    protected float health; // 상속받을 player와 enemy가 사용가능하도록 protected로 선언
    protected bool dead = false; // false

    public event System.Action OnDeath; 
    // System.Action - 델리게이트 메소드 - 다른 메소드 위치를 가르키고 불러올 수 있는 타입,
    // C ++에서 함수 포인터의 역할과 유사

    protected virtual void Start()
    {
        health = startingHealth;
    }

    void IDamageable.TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;

        if(OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }

    
}
