using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float FirePower;
    [SerializeField] Player PlayerObj;
    [SerializeField] CameraShake CamShake;
    [SerializeField] Camera cam;
    [SerializeField] GameObject RikoshetEffect;
    [SerializeField] GameObject muzzlePrefab;
    [SerializeField] GameObject muzzlePosition;
    [SerializeField] float ShakePower;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip ShootSound;
    [SerializeField] bool ExplosionBullet;
    [SerializeField] float ExplosionPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && PlayerObj.bullets > 0)
        {
            GetComponent<Animator>().Play("Shoot");
            CamShake.shakeDuration = 0.1f;

            CamShake.enabled = false;
            CamShake.enabled = true;
            CamShake.shakeAmount = ShakePower;
            FireWeapon();
        }
    }

    void FireWeapon ()
    {
        AS.PlayOneShot(ShootSound);
        RaycastHit hit;
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(RikoshetEffect, hit.point, Quaternion.identity);

            if(ExplosionBullet)
            {
                Collider[] hitColliders = Physics.OverlapSphere(hit.point, 5);
                foreach (var hitCollider in hitColliders)
                {
                    if(hitCollider.GetComponent<Rigidbody>())
                    {
                        hitCollider.GetComponent<Rigidbody>().AddExplosionForce(ExplosionPower, hit.point, 5);
                    }

                }
            }

            if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = hit.transform.position - transform.position;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * FirePower * 1000);
            }


            if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = hit.transform.position - transform.position;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * FirePower);
            }

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                int rand = Random.RandomRange(0, 2);
                if (rand == 0)
                    damage = 5;
                else
                    damage = 10;
                hit.transform.gameObject.GetComponent<Enemy>().GetHit(damage, hit.point);
            }
        }




        // --- Spawn muzzle flash ---
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation);

        // --- Shoot Projectile Object ---

        PlayerObj.Shoot();
        // --- Insert custom code here to shoot projectile or hitscan from weapon ---

    }
}
