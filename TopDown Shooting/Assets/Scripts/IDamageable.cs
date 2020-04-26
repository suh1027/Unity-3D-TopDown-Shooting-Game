using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Idamageable 인터페이스를 생성 -> TakeHit 메소드를 생성해서 
// 발사체가 어느오브젝트에 붙어있는지와 상관없이 사용가능

public interface IDamageable
{
    void TakeHit(float damage, RaycastHit hit);

    void TakeDamage(float damage);
}