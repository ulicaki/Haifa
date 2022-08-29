using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickWepone : MonoBehaviour
{
    [SerializeField] int WeponeIndex;
    [SerializeField] Vector3 ScaleBig;
    [SerializeField] Vector3 ScaleSmall;
    [SerializeField] Vector3 RotateSpeed;
    bool bigger = true;
    Vector3 NowScale;
    // Start is called before the first frame update
    void Start()
    {
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
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeWepone(WeponeIndex);
            gameObject.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateSpeed);
    }
}
