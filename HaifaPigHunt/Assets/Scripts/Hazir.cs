using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hazir : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float SpeedOffMoving;
    [SerializeField] float RotationSpeed;
    Animator Anim;
    Rigidbody rb;
    bool StartFollow;
    bool DoHitBool;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        StartCoroutine(DoHit());
    }

    // Update is called once per frame
    void Update()
    {

        CheckForFollowing();


    }

    private void FixedUpdate()
    {
        if (StartFollow)
        {
            StartFollowFunc();
            CheckForAttack();
        }

    }

    void StartFollowFunc ()
    {
        Debug.LogError("Spped: " +(transform.position + (Vector3.Normalize(Player.transform.position - transform.position) * Time.deltaTime)) * SpeedOffMoving);
        Vector3 Dir = Vector3.Normalize(Player.transform.position - transform.position);
        //rb.velocity = new Vector3(Dir.x, rb.velocity.y, Dir.z) * SpeedOffMoving * Time.deltaTime;
        //rb.MovePosition(transform.position + ((Vector3.Normalize(Player.transform.position - transform.position) * SpeedOffMoving * Time.deltaTime)));
        Anim.SetBool("Follow", true);
    }

    void CheckForAttack()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < 7)
        {
            Anim.SetTrigger("Attack");
        }
    }

    void CheckForFollowing ()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < 40)
        {
            StartFollow = true;


                var lookPos = Player.transform.position - transform.position;
             lookPos.y = 0;
             var rotation = Quaternion.LookRotation(lookPos);
             transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime* RotationSpeed);
         
        }
        else
        {
            StartFollow = false;
            rb.velocity = Vector3.zero;
            rb.velocity = rb.velocity;
            Anim.SetBool("Follow", false);
        }
    }

    IEnumerator DoHit ()
    {
        int i = 1;

        while(i > 0)
        {
            if (DoHitBool)
            {
                Player.gameObject.GetComponent<Player>().GetHit();
                yield return new WaitForSeconds(0.5f);
            }
            i++;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DoHitBool = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoHitBool = false;
        }
    }

}
