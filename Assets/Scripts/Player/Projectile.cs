using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float fireBallSpeed;
    private Animator myAnimator;
    private BoxCollider2D MyBoxCollider2D;
    private float lifeTime;
    private bool isHit;
    private float direction;
    [SerializeField] float damgeGivenToEnemy;


    private void Start()
    {
        
    }
    private void Awake()
    {
        MyBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //https://discussions.unity.com/t/how-do-i-get-some-objects-to-ignore-collision-with-a-specific-object/102308/3
        /*
        GameObject DetectionZone = GameObject.FindGameObjectWithTag("DetectionZone");
        Physics2D.IgnoreCollision(DetectionZone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        */ //this code is usable but it only can find one gameObject making it obsoclet

        if (isHit) { return; }
        float FlameBallMovement = fireBallSpeed * Time.deltaTime * direction;
        transform.Translate(FlameBallMovement,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) 
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) // ignore collison whenever is tounch new colider
    {
        if (collision.tag == "Enemy")
        {
            isHit = true;
            collision.GetComponent<Health>().takeCriticalHit(false); // set false for crtial hit floating text
            collision.GetComponent<Health>().TakeDamage(damgeGivenToEnemy);
            MyBoxCollider2D.enabled = false;
            myAnimator.SetTrigger("Explosion"); // set trigger for explosion animmation
            collision.GetComponentInChildren<CircleCollider2D>().radius = 3;
            //myCamera.orthographicSize = 1f;
        }
        else if (collision.tag == "Player")
        {
            isHit = true;
            collision.GetComponent<Health>().TakeDamage(damgeGivenToEnemy);
            MyBoxCollider2D.enabled = false;
            myAnimator.SetTrigger("Explosion"); // set trigger for explosion animmation
        }

        else if (collision.tag == "DetectionZone" || collision.tag == "InvisibleWall" || collision.tag == "MeeleAttack" || collision.tag == "FlameBall") //ingore out the collsion for dectionZone
        {
            //print("Collsion dectect"); //https://forum.unity.com/threads/ignore-collisions-by-tag-solved.60387/
            if (collision.GetComponent<Collider2D>() != null)
            {
                Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>(), true);
            }
        } 
        else
        {
            isHit = true;   //set is hit to true to return and cancle the sciprt (Look at fix update method)
            MyBoxCollider2D.enabled = false;
            myAnimator.SetTrigger("Explosion"); // set trigger for explosion animmation
        }
    }
    public void setDirection(float _direction)
    {
        
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        isHit = false;
        MyBoxCollider2D.enabled = true;

        float localScale = transform.localScale.x;
        if (Mathf.Sign(localScale) != direction)
            transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    private void DeactiveFlameBall()
    {
        gameObject.SetActive(false);
    }
}
