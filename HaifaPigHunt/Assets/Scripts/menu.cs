using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    [SerializeField] GameObject Fader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumbitName ()
    {
        StartCoroutine(nextLevel());
    }

    IEnumerator nextLevel ()
    {
        Fader.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(1);
    }
}
