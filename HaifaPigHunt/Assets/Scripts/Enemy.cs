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
    [SerializeField] CapsuleCollider MainCollider;
    [SerializeField] CapsuleCollider HeadRig;
    [SerializeField] CapsuleCollider BodyRig;
    [SerializeField] CapsuleCollider Leg1;
    [SerializeField] CapsuleCollider Leg2;
    [SerializeField] CapsuleCollider Leg3;
    [SerializeField] CapsuleCollider Leg4;
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
            GameObject.FindGameObjectWithTag("GM").GetComponent<GM>().MinusBoar();
            Die();
        }
    }

    void Die ()
    {
        MainCollider.enabled = false;
        HeadRig.enabled = true;
        BodyRig.enabled = true;
        Leg1.enabled = true;
        Leg2.enabled = true;
        Leg3.enabled = true;
        Leg4.enabled = true;

        Destroy(MainCollider.gameObject.GetComponent<Rigidbody>());
        HeadRig.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        BodyRig.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Leg1.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Leg2.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Leg3.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Leg4.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        Destroy(gameObject.GetComponent<Hazir>());
        Destroy(gameObject.GetComponent<Animator>());
        Destroy(gameObject.GetComponent<Enemy>());
       // Instantiate(DieEffect, gameObject.transform.position, Quaternion.identity);
       // Instantiate(DieEffect2, gameObject.transform.position, Quaternion.identity);
       //Destroy(gameObject);
    }

}
