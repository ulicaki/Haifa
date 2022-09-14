using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Health;
    [SerializeField] GameObject DieEffect;
    [SerializeField] GameObject DieEffect2;
    [SerializeField] GameObject BloodEffect;
    [SerializeField] GameObject Damage5;
    [SerializeField] GameObject Damage10;
    [SerializeField] GameObject Damage30;

    [Header("Ragdoll")]
    [SerializeField] BoxCollider MainCollider;
    [SerializeField] Rigidbody HeadRig;
    [SerializeField] Rigidbody BodyRig;
    [SerializeField] Rigidbody Leg1_1Rig;
    [SerializeField] Rigidbody Leg1_2Rig;
    [SerializeField] Rigidbody Leg2_1Rig;
    [SerializeField] Rigidbody Leg2_2Rig;
    [SerializeField] Rigidbody Leg3_1Rig;
    [SerializeField] Rigidbody Leg3_2Rig;
    [SerializeField] Rigidbody Leg4_1Rig;
    [SerializeField] Rigidbody Leg4_2Rig;
    // Start is called before the first frame update
    public void GetHit (int damage,Vector3 Pos)
    {
        if(damage ==5)
            Instantiate(Damage5, Pos, Quaternion.identity);
        else if (damage == 10)
            Instantiate(Damage10, Pos, Quaternion.identity);
        else if (damage == 30)
            Instantiate(Damage30, Pos, Quaternion.identity);

        Health -= damage;
        Instantiate(BloodEffect, Pos, Quaternion.identity);
        if(Health <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        Instantiate(DieEffect, gameObject.transform.position, Quaternion.identity);
        Instantiate(DieEffect2, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
