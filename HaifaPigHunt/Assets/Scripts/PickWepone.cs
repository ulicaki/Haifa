using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickWepone : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] public int Bullets;
    [SerializeField] int MinBullet;
    [SerializeField] int ManBullet;
    [SerializeField] int WeponeIndex;
    [SerializeField] Vector3 ScaleBig;
    [SerializeField] Vector3 ScaleSmall;
    [SerializeField] Vector3 RotateSpeed;
    [SerializeField] bool Rot;
    [SerializeField] bool DisPer;
    bool bigger = true;
    Vector3 NowScale;
    // Start is called before the first frame update
    void Start()
    {
        if(Bullets < 0)
        Bullets = Random.RandomRange(MinBullet, ManBullet);
        NowScale = ScaleBig;
        ScaleBigFunc();
    }

    void ScaleBigFunc ()
    {
        transform.DOScale(ScaleBig, 2).SetEase(Ease.InOutCubic).OnComplete(ScaleSmallFunc);
    }

    void ScaleSmallFunc ()
    {
        transform.DOScale(ScaleSmall, 2).SetEase(Ease.InOutCubic).OnComplete(ScaleBigFunc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && Bullets !=0)
        {
            if (other.gameObject.GetComponent<Player>().NowWepon != WeponeIndex)
            {
                other.GetComponent<Player>().TakeWepone(WeponeIndex, Bullets);
                if (!DisPer)
                    gameObject.SetActive(false);
                else
                    gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(Rot)
        transform.Rotate(RotateSpeed);
    }
}
