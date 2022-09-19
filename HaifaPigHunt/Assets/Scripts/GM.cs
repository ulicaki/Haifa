using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GM : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text BoarsLeftText;
    int StartBoarCount;
    int BoarLeft;
    // Start is called before the first frame update
    void Start()
    {
        FirstSetUp();
    }

    void FirstSetUp()
    {
        GameObject[] Boarobjs = GameObject.FindGameObjectsWithTag("Enemy");
        StartBoarCount = Boarobjs.Length;
        BoarLeft = StartBoarCount;
        BoarsLeftText.text = StartBoarCount + "/"+ StartBoarCount;
    }

    public void MinusBoar ()
    {
        BoarLeft--;
        BoarsLeftText.text = BoarLeft + "/" + StartBoarCount;

        Sequence sec = DOTween.Sequence();
        sec.Append(BoarsLeftText.transform.DOScale(new Vector3(1.2f,1.2f,1.2f),0.2f));
        sec.Append(BoarsLeftText.transform.DOScale(new Vector3(1,1,1),0.2f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
