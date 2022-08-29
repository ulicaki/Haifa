using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [Header("Wepones")]
    [SerializeField] GameObject Gun1;
    [SerializeField] GameObject DuckGun;
    [SerializeField] Transform DuckGunStartPos;
    Vector3 DuckGunStartScale;
    Quaternion DuckGunStartRot;
    [SerializeField] float DuckTrowPower;
    [SerializeField] GameObject[] DuckGunPrefab;
    [SerializeField] AudioClip DuckTrowSound;
    WeponeActive ActiveWepone;

    [Header("UI")]
    [SerializeField] Image ScreenBlood;
    Color NowColorn;

    [Header("Other")]
    [SerializeField] AudioSource AS;
    [SerializeField] GameObject CameraCam;
    [SerializeField] Transform CameraFarLook;
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
        
        if(Input.GetMouseButtonDown(0))
        {
            if(ActiveWepone == WeponeActive.DuckWepone)
             DuckTrow();
        }

    }

    public void TakeWepone (int WeponIndex)
    {
        switch(WeponIndex)
        {
            case 1:
                Gun1.SetActive(true);
                DuckGun.SetActive(false);
                ActiveWepone = WeponeActive.Gun1;
                break;
            case 2: Gun1.SetActive(false);
                DuckGun.SetActive(true);
                ActiveWepone = WeponeActive.DuckWepone;
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
        DuckWepone
    }


}
