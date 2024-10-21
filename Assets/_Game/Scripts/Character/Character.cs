using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Transform lowerBody;
    [SerializeField] protected Transform upperBody;
    [SerializeField] protected GameObject bulletPrefab;

    [SerializeField] protected Transform muzzle;
    [SerializeField] protected Transform barrel;


    [SerializeField, Min(1)] protected float cannonBallMass = 30;

    [SerializeField, Min(1)] protected float shotForce = 30;

    public virtual void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = Quaternion.LookRotation((muzzle.position - barrel.position)).normalized;
        Bullet b = bullet.GetComponent<Bullet>(); 
        b.Oner = this.transform;
        b.Rb.mass = cannonBallMass;
        b.Rb.AddForce(muzzle.forward * shotForce, ForceMode.Impulse);
    }
}
