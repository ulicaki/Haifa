using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGun : MonoBehaviour
{
    [SerializeField] GameObject PuffEffect;
    [SerializeField] float SecToDie;
    [SerializeField] GameObject SpecialPuff;
    [SerializeField] bool ThisSpecial;
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
        if(ThisSpecial)
        {
            Instantiate(SpecialPuff, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
