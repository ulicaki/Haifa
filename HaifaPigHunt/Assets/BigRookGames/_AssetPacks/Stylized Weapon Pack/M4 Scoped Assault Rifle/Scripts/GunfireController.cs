﻿using UnityEngine;

namespace BigRookGames.Weapons
{
    public class GunfireController : MonoBehaviour
    {
        [SerializeField] int damage;
        [SerializeField] float FirePower;
        // --- Audio ---
        public AudioClip GunShotClip;
        public AudioSource source;
        public Vector2 audioPitch = new Vector2(.9f, 1.1f);

        // --- Muzzle ---
        public GameObject muzzlePrefab;
        public GameObject muzzlePosition;

        // --- Config ---
        public bool autoFire;
        public float shotDelay = .5f;
        public bool rotate = true;
        public float rotationSpeed = .25f;

        // --- Options ---
        public GameObject scope;
        public bool scopeActive = true;
        private bool lastScopeState;

        // --- Projectile ---
        [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
        public GameObject projectilePrefab;
        [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
            " the visible rocket attached to the rocket launcher")]
        public GameObject projectileToDisableOnFire;

        // --- Timing ---
        [SerializeField] private float timeLastFired;


        [SerializeField] CameraShake CamShake;
        [SerializeField] GameObject RikoshetEffect;
        [SerializeField] Camera cam;
        [SerializeField] Player PlayerObj;
        

        private void Start()
        {
            if(source != null) source.clip = GunShotClip;
            timeLastFired = 0;
            lastScopeState = scopeActive;
        }

        private void Update()
        {
            // --- If rotate is set to true, rotate the weapon in scene ---
            if (rotate)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y 
                                                                        + rotationSpeed, transform.localEulerAngles.z);
            }

            // --- Fires the weapon if the delay time period has passed since the last shot ---
            if (Input.GetMouseButtonDown(0) && PlayerObj.bullets > 0)
            {
                GetComponent<Animator>().Play("Shoot");
                CamShake.shakeDuration = 0.1f;
                CamShake.shakeAmount = 0.5f;
                CamShake.enabled = false;
                CamShake.enabled = true;
                FireWeapon();
            }

            // --- Toggle scope based on public variable value ---
            if(scope && lastScopeState != scopeActive)
            {
                lastScopeState = scopeActive;
                scope.SetActive(scopeActive);
            }
        }

        /// <summary>
        /// Creates an instance of the muzzle flash.
        /// Also creates an instance of the audioSource so that multiple shots are not overlapped on the same audio source.
        /// Insert projectile code in this function.
        /// </summary>
        public void FireWeapon()
        {
            RaycastHit hit;
            var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(RikoshetEffect, hit.point, Quaternion.identity);

                if(hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Vector3 dir = hit.transform.position- transform.position ;
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * FirePower);
                }    

                if(hit.transform.gameObject.CompareTag("Enemy"))
                {
                    int rand = Random.RandomRange(0, 2);
                    if (rand == 0)
                        damage = 5;
                    else
                        damage = 10;
                    hit.transform.gameObject.GetComponent<Enemy>().GetHit(damage, hit.point);
                }
            }




            // --- Keep track of when the weapon is being fired ---
            timeLastFired = Time.time;

            // --- Spawn muzzle flash ---
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation);

            // --- Shoot Projectile Object ---
            if (projectilePrefab != null)
            {
                GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation,transform);
            }

            // --- Disable any gameobjects, if needed ---
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke("ReEnableDisabledProjectile", 3);
            }

            // --- Handle Audio ---
            if (source != null)
            {
                // --- Sometimes the source is not attached to the weapon for easy instantiation on quick firing weapons like machineguns, 
                // so that each shot gets its own audio source, but sometimes it's fine to use just 1 source. We don't want to instantiate 
                // the parent gameobject or the program will get stuck in a loop, so we check to see if the source is a child object ---
                if(source.transform.IsChildOf(transform))
                {
                    source.Play();
                }
                else
                {
                    // --- Instantiate prefab for audio, delete after a few seconds ---
                    AudioSource newAS = Instantiate(source);
                    if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                    {
                        // --- Change pitch to give variation to repeated shots ---
                        newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                        newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                        // --- Play the gunshot sound ---
                        newAS.PlayOneShot(GunShotClip);

                        // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                        Destroy(newAS.gameObject, 4);
                    }
                }
            }

            PlayerObj.Shoot();
            // --- Insert custom code here to shoot projectile or hitscan from weapon ---

        }

        private void ReEnableDisabledProjectile()
        {
            projectileToDisableOnFire.SetActive(true);
        }
    }
}