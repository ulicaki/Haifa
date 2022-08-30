using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGun : MonoBehaviour
{
    [SerializeField] GameObject PuffEffect;
    [SerializeField] float SecToDie;
    [SerializeField] GameObject SpecialPuff;
    [SerializeField] bool ThisSpecial;
    bool givedDamage = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartTimer ()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer ()
    {
        yield return new WaitForSeconds(SecToDie);
        Instantiate(PuffEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(ThisSpecial && !collision.gameObject.CompareTag("Player"))
        {
            Instantiate(SpecialPuff, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(!givedDamage)
            {
                givedDamage = true;
                if (!ThisSpecial)
                {
                    int rand = Random.RandomRange(0, 2);
                    if (rand == 0)
                        collision.transform.gameObject.GetComponent<Enemy>().GetHit(5, transform.position);
                    else
                        collision.transform.gameObject.GetComponent<Enemy>().GetHit(10, transform.position);
                }
                else
                    collision.transform.gameObject.GetComponent<Enemy>().GetHit(30, transform.position);
            }
        }
    }
}
