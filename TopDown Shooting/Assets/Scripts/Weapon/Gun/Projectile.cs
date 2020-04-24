using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
