using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Meele : MonoBehaviour
{
    [SerializeField] float damgeGivenToEnemy;
    [SerializeField] float knockBackAmout;
    [SerializeField] GameObject PlayerObject;
    private BoxCollider2D MyBoxCollider2D;
    private float direction;

    private void Awake()
    {
        MyBoxCollider2D = GetComponent<BoxCollider2D>();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        direction = PlayerObject.transform.localScale.x;

        if (collision.tag == "Enemy")
        {
            
            collision.GetComponent<Health>().TakeDamage(damgeGivenToEnemy); //Damge enemy health
            collision.GetComponent<SlimeMV>().enabled = false;
            collision.GetComponent<SlimeMV>().isBeingHit(true, direction, knockBackAmout);
            //collision.GetComponent<Rigidbody2D>().AddForce(Froce *Time.deltaTime, ForceMode2D.Impulse);
            // lý do vì sao lấy được GetCompoent<Health> vì được lấy từ đối tượng va vào colider của gameObject này.
            MyBoxCollider2D.enabled = false;
        }
        else if (collision.tag == "DetectionZone" || collision.tag == "InvisibleWall") //ingore out the collsion for dectionZone
        {
            //print("Collsion dectect"); //https://forum.unity.com/threads/ignore-collisions-by-tag-solved.60387/
            if (collision.GetComponent<CircleCollider2D>() != null)
                Physics2D.IgnoreCollision(collision.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), true);
        }
    }


}
