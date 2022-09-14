using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [Header("Wepones")]
    [SerializeField] GameObject Gun1;
    [SerializeField] GameObject Gun2;
    [SerializeField] GameObject Gun3;
    [SerializeField] GameObject FlareGun;
    [SerializeField] GameObject DuckGun;
    [SerializeField] Transform DuckGunStartPos;
    Vector3 DuckGunStartScale;
    Quaternion DuckGunStartRot;
    [SerializeField] float DuckTrowPower;
    [SerializeField] GameObject[] DuckGunPrefab;
    [SerializeField] AudioClip DuckTrowSound;
    [SerializeField] GameObject DropedM4;
    [SerializeField] GameObject DropedFlareGun;
    [SerializeField] GameObject DropedGun2;
    [SerializeField] GameObject DropedGun3;
    [SerializeField] float DropWeponForce;
    [SerializeField] float DropWeponForceRot;
    WeponeActive ActiveWepone;
    public int bullets = 0;
    public int NowWepon;
    [SerializeField] Vector3 DropM4EndSize;
    [SerializeField] Vector3 DropFlareGunEndSize;
    [SerializeField] Vector3 DropGun2EndSize;
    [SerializeField] Vector3 DropGun3EndSize;

    [Header("UI")]
    [SerializeField] Image ScreenBlood;
    [SerializeField] GameObject BulletPanel;
    [SerializeField] Text BulletText;
    Color NowColorn;

    [Header("Other")]
    [SerializeField] AudioSource AS;
    [SerializeField] GameObject CameraCam;
    [SerializeField] Transform CameraFarLook;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject SpeedEffect;
    bool WeponeOn = false;
    // Start is called before the first frame update
    void Start()
    {

        DuckGunStartScale = DuckGun.transform.localScale;

    }

    public void GetHit()
    {
        GetComponent<CameraShake>().enabled = false;
        GetComponent<CameraShake>().enabled = true;
        Color NowColor = ScreenBlood.color;
        NowColor.a += 0.1f;
        ScreenBlood.color = NowColor;
        StartCoroutine(AfterGotHit());
    }

    IEnumerator AfterGotHit ()
    {
        yield return new WaitForSeconds(5);
        NowColorn = ScreenBlood.color;
        DOVirtual.Float(ScreenBlood.color.a, 0, 0.2f, v =>
        {
            NowColorn.a =v;
            ScreenBlood.color = NowColorn;
        });
    } 

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 40)
            SpeedEffect.SetActive(true);
        else
            SpeedEffect.SetActive(false);
   
        if(Input.GetMouseButtonDown(0))
        {
            if (ActiveWepone == WeponeActive.DuckWepone && bullets > 0)
            {
                DuckTrow();
                Shoot();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropWepone();
        }

    }


    void DropWepone()
    {
        switch (NowWepon)
        {
            case 1:
                Gun1.SetActive(false);
                GameObject Droped = (GameObject)Instantiate(DropedM4, Gun1.transform.position, Gun1.transform.rotation);
                Droped.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                Vector3 RandRot = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                Droped.GetComponent<Rigidbody>().AddTorque(RandRot * DropWeponForceRot);
                Droped.transform.DOScale(DropM4EndSize, 1.5f);
                Droped.transform.GetChild(1).GetComponent<PickWepone>().Bullets = bullets;
                bullets = 0;
                BulletText.text = "0";
                StartCoroutine(TimerForDrop(Droped));
                BulletPanel.SetActive(false);
                Gun1.SetActive(false);
                DuckGun.SetActive(false);
                NowWepon = 0;
                break;
            case 3:
                FlareGun.SetActive(false);
                GameObject Droped2 = (GameObject)Instantiate(DropedFlareGun, FlareGun.transform.position, FlareGun.transform.rotation);
                Droped2.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                Vector3 RandRot2 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                Droped2.GetComponent<Rigidbody>().AddTorque(RandRot2 * DropWeponForceRot);
                Droped2.transform.DOScale(DropFlareGunEndSize, 1.5f);
                Droped2.transform.GetChild(1).GetComponent<PickWepone>().Bullets = bullets;
                bullets = 0;
                BulletText.text = "0";
                StartCoroutine(TimerForDrop(Droped2));
                BulletPanel.SetActive(false);
                Gun1.SetActive(false);
                FlareGun.SetActive(false);
                DuckGun.SetActive(false);
                NowWepon = 0;
                break;
            case 4:
                Gun2.SetActive(false);
                GameObject Droped3 = (GameObject)Instantiate(DropedGun2, Gun2.transform.position, Gun2.transform.rotation);
                Droped3.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                Vector3 RandRot3 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                Droped3.GetComponent<Rigidbody>().AddTorque(RandRot3 * DropWeponForceRot);
                Droped3.transform.DOScale(DropGun2EndSize, 1.5f);
                Droped3.transform.GetChild(1).GetComponent<PickWepone>().Bullets = bullets;
                bullets = 0;
                BulletText.text = "0";
                StartCoroutine(TimerForDrop(Droped3));
                BulletPanel.SetActive(false);
                Gun1.SetActive(false);
                Gun2.SetActive(false);
                FlareGun.SetActive(false);
                DuckGun.SetActive(false);
                NowWepon = 0;
                break;
            case 5:
                Gun3.SetActive(false);
                GameObject Droped4 = (GameObject)Instantiate(DropedGun3, Gun3.transform.position, Gun3.transform.rotation);
                Droped4.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                Vector3 RandRot4 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                Droped4.GetComponent<Rigidbody>().AddTorque(RandRot4 * DropWeponForceRot);
                Droped4.transform.DOScale(DropGun3EndSize, 1.5f);
                Droped4.transform.GetChild(1).GetComponent<PickWepone>().Bullets = bullets;
                bullets = 0;
                BulletText.text = "0";
                StartCoroutine(TimerForDrop(Droped4));
                BulletPanel.SetActive(false);
                Gun1.SetActive(false);
                Gun2.SetActive(false);
                Gun3.SetActive(false);
                FlareGun.SetActive(false);
                DuckGun.SetActive(false);
                NowWepon = 0;
                break;
        }
    }

    IEnumerator TimerForDrop(GameObject gun)
    {
        yield return new WaitForSeconds(1.5f);

        gun.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Shoot ()
    {
        bullets--;
        BulletText.text = "" + bullets;
        if(bullets <= 0)
        {
           switch(NowWepon)
            {
                case 1:
                    Gun1.SetActive(false);
                    GameObject Droped = (GameObject) Instantiate(DropedM4, Gun1.transform.position, Gun1.transform.rotation);
                    Droped.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) *DropWeponForce + (Vector3.up*1000)) );
                    Vector3 RandRot = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                    Droped.GetComponent<Rigidbody>().AddTorque(RandRot* DropWeponForceRot);
                      Droped.transform.DOScale(DropM4EndSize, 1.5f);
                    StartCoroutine(TimerForDrop(Droped));
                    break;
                case 3:
                    FlareGun.SetActive(false);
                    GameObject Droped2 = (GameObject)Instantiate(DropedFlareGun, FlareGun.transform.position, FlareGun.transform.rotation);
                    Droped2.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                    Vector3 RandRot2 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                    Droped2.GetComponent<Rigidbody>().AddTorque(RandRot2 * DropWeponForceRot);
                    Droped2.transform.DOScale(DropFlareGunEndSize, 1.5f);
                    StartCoroutine(TimerForDrop(Droped2));
                    break;
                case 4:
                    Gun2.SetActive(false);
                    GameObject Droped3 = (GameObject)Instantiate(DropedGun2, Gun2.transform.position, Gun2.transform.rotation);
                    Droped3.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                    Vector3 RandRot3 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                    Droped3.GetComponent<Rigidbody>().AddTorque(RandRot3 * DropWeponForceRot);
                    Droped3.transform.DOScale(DropFlareGunEndSize, 1.5f);
                    StartCoroutine(TimerForDrop(Droped3));
                    break;
                case 5:
                    Gun3.SetActive(false);
                    GameObject Droped4 = (GameObject)Instantiate(DropedGun3, Gun3.transform.position, Gun3.transform.rotation);
                    Droped4.GetComponent<Rigidbody>().AddForce(((CameraFarLook.transform.position - CameraCam.transform.position) * DropWeponForce + (Vector3.up * 1000)));
                    Vector3 RandRot4 = new Vector3(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
                    Droped4.GetComponent<Rigidbody>().AddTorque(RandRot4 * DropWeponForceRot);
                    Droped4.transform.DOScale(DropGun3EndSize, 1.5f);
                    Droped4.transform.GetChild(1).GetComponent<PickWepone>().Bullets = bullets;
                    StartCoroutine(TimerForDrop(Droped4));
                    break;

            }
            BulletPanel.SetActive(false);
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            FlareGun.SetActive(false);
            DuckGun.SetActive(false);
            NowWepon = 0;
        }
    }

    public void TakeWepone (int WeponIndex,int Bullet)
    {
        Debug.LogError("Pick");

        if(NowWepon != 0)
        {
            DropWepone();
        }
        bullets = Bullet;
        NowWepon = WeponIndex;
        switch (WeponIndex)
        {
            case 1:
                WeponeOn = true;
                Gun1.SetActive(true);
                DuckGun.SetActive(false);
                Gun2.SetActive(false);
                FlareGun.SetActive(false);
                ActiveWepone = WeponeActive.Gun1;
                BulletPanel.SetActive(true);
                BulletText.text = "" + bullets;
                break;
            case 2:
                WeponeOn = true; 
                Gun1.SetActive(false);
                DuckGun.SetActive(true);
                Gun2.SetActive(false);
                FlareGun.SetActive(false);
                ActiveWepone = WeponeActive.DuckWepone;
                BulletPanel.SetActive(true);
                BulletText.text = "" + bullets;  
                break;
            case 3:
                WeponeOn = true;
                Gun1.SetActive(false);
                Gun2.SetActive(false);
                FlareGun.SetActive(true);
                DuckGun.SetActive(false);
                BulletPanel.SetActive(true);
                BulletText.text = "" + bullets;
                ActiveWepone = WeponeActive.none;
                break;
            case 4:
                WeponeOn = true;
                Gun1.SetActive(false);
                Gun2.SetActive(true);
                FlareGun.SetActive(false);
                DuckGun.SetActive(false);
                BulletPanel.SetActive(true);
                ActiveWepone = WeponeActive.none;
                BulletText.text = "" + bullets;
                break;
            case 5:
                WeponeOn = true;
                Gun1.SetActive(false);
                Gun2.SetActive(false);
                Gun3.SetActive(true);
                FlareGun.SetActive(false);
                DuckGun.SetActive(false);
                BulletPanel.SetActive(true);
                BulletText.text = "" + bullets;
                ActiveWepone = WeponeActive.none;
                break;
        }
    }

    void DuckTrow ()
    {
        AS.PlayOneShot(DuckTrowSound);
        DuckGun.GetComponent<DuckGun>().StartTimer();
        DuckGun.transform.SetParent(null);
        DuckGun.GetComponent<SphereCollider>().enabled = true;
        DuckGun.AddComponent<Rigidbody>();
        DuckGunStartRot = DuckGun.transform.rotation;

        /*RaycastHit hit;
        var ray = CameraCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit))
        {
            DuckGun.GetComponent<Rigidbody>().AddForce((hit.point - DuckGun.transform.position) * DuckTrowPower, ForceMode.Impulse);
        }
        else
        {
            
        }*/

        DuckGun.GetComponent<Rigidbody>().AddForce((CameraFarLook.position - DuckGun.transform.position) * DuckTrowPower, ForceMode.Impulse);
        int rand = Random.RandomRange(0, DuckGunPrefab.Length);
        DuckGun = (GameObject)Instantiate(DuckGunPrefab[rand], DuckGunStartPos.position, DuckGunStartRot);
        DuckGun.transform.SetParent(CameraCam.transform);
        DuckGun.transform.localScale = Vector3.zero;
        DuckGun.transform.rotation = DuckGunStartRot;
        DuckGun.transform.DOScale(DuckGunStartScale, 0.3f).SetEase(Ease.InOutBounce);
    }

    public enum WeponeActive
    {
        Gun1,
        DuckWepone,
        none
    }


}
