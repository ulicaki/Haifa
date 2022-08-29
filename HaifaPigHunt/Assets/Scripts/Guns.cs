using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Guns : MonoBehaviour
{
    [SerializeField] float smooth;
    [SerializeField] float multi;
    Quaternion startRot;
    Quaternion targ;
    [SerializeField] bool OtherRot;
    [SerializeField] Vector3 OtherRotVal;
    // Start is called before the first frame update

    private void Awake()
    {
        //startRot = transform.rotation;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * multi;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multi;

        Quaternion rotationX = Quaternion.AngleAxis(mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(-mouseX, Vector3.up);

        if (OtherRot)
            targ = rotationX * rotationY * Quaternion.Euler(OtherRotVal);
        else
             targ = rotationX * rotationY;


        transform.localRotation = Quaternion.Slerp(transform.localRotation, targ, smooth * Time.deltaTime);
    }


}
