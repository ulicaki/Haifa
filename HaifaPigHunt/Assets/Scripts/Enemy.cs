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
    // Start is called before the first frame update
public void GetHit (int damage,Vector3 Pos)
    {
        if(damage ==5)
            Instantiate(Damage5, Pos, Quaternion.identity);
        else if (damage == 10)
            Instantiate(Damage10, Pos, Quaternion.identity);
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
